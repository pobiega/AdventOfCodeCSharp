using AdventOfCodeSupport;

namespace AdventOfCode._2023;

public sealed class Day03 : AdventBase
{
    protected override void InternalOnLoad()
    {
        base.InternalOnLoad();
        _height = Input.Lines.Length - 1;
        _width = Input.Lines[0].Length - 1;
    }

    protected override object InternalPart1()
    {
        var validNumbers = Input.Lines
            .SelectMany(FindAllNumbers)
            .Where(x => HasSymbolNeighbour(x, IsSymbol))
            .ToArray();

        return validNumbers
            .Select(x => x.Value)
            .Sum();
    }

    private bool HasSymbolNeighbour(NumericLocation arg, Func<char, bool> symbolFunc)
    {
        var coordsToCheck = GetAllAdjacentLocations(arg.X, arg.Y, arg.Length);

        return coordsToCheck
            .Select(location => Input.Lines[location.y][location.x])
            .Any(symbolFunc);
    }

    private static readonly (int x, int y)[] AdjacentOffsets =
    {
        (-1, -1), (0, -1), (1, -1), (-1, 0), (1, 0), (-1, 1), (0, 1), (1, 1)
    };

    private int _height;
    private int _width;

    private IEnumerable<(int x, int y)> GetAllAdjacentLocations(int x, int y, int length)
    {
        var locations = new List<(int x, int y)>();

        for (int i = x; i < x + length; i++)
        {
            foreach (var offset in AdjacentOffsets)
            {
                var newX = i + offset.x;
                newX = Math.Clamp(newX, 0, _width);
                var newY = y + offset.y;
                newY = Math.Clamp(newY, 0, _height);

                locations.Add((newX, newY));
            }
        }

        return locations.Distinct();
    }

    private bool IsSymbol(char c) => !char.IsDigit(c) && c != '.';

    private IEnumerable<NumericLocation> FindAllNumbers(string str, int lineNumber)
    {
        var buffer = new List<char>();

        var x = 0;
        foreach (var c in str)
        {
            if (char.IsDigit(c))
            {
                buffer.Add(c);
                continue;
            }

            if (buffer.Count > 0)
            {
                var num = new string(buffer.ToArray()).ToInt32();
                yield return new NumericLocation(num, x, lineNumber);

                x += buffer.Count + 1;
                buffer.Clear();
                continue;
            }

            x++;
        }

        if (buffer.Count > 0)
        {
            var num = new string(buffer.ToArray()).ToInt32();
            yield return new NumericLocation(num, x, lineNumber);

            buffer.Clear();
        }
    }

    protected override object InternalPart2()
    {
        var numbers = Input.Lines
            .SelectMany(FindAllNumbers)
            .ToArray();

        var gears = Input.Lines
            .SelectMany((str, lineNumber) => FindAllGears(str, lineNumber, numbers));

        return gears
            .Sum();
    }

    private IEnumerable<int> FindAllGears(string str, int lineNumber, NumericLocation[] numbers)
    {
        var line = Input.Lines[lineNumber];
        for (int x = 0; x < line.Length; x++)
        {
            var c = line[x];

            if (c == '*')
            {
                var neighbours = GetNearbyNumber(x, lineNumber, numbers);
                if (neighbours.Length == 2)
                {
                    yield return neighbours[0].Value * neighbours[1].Value;
                }
            }
        }
    }

    private NumericLocation[] GetNearbyNumber(int x, int y, NumericLocation[] numbers)
    {
        var locations = new List<NumericLocation>();

        foreach (var offset in GetAllAdjacentLocations(x, y, 1))
        {
            var newX = offset.x;
            var newY = offset.y;

            var nums = numbers.FirstOrDefault(n => newX >= n.X && newX <= (n.X + n.Length - 1) && n.Y == newY);

            if (nums != default)
            {
                locations.Add(nums);
            }
        }

        return locations.Distinct().ToArray();
    }
}

internal readonly record struct NumericLocation(int Value, int X, int Y)
{
    public int Length => Value.ToString().Length;
}