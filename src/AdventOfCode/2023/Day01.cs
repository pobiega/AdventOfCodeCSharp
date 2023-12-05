using AdventOfCodeSupport;

namespace AdventOfCode._2023;

public sealed class Day01 : AdventBase
{
    protected override object InternalPart1()
        => Input.Lines.Select(CombineFirstAndLastDigit).Sum();

    private int CombineFirstAndLastDigit(string source)
    {
        var first = source.First(char.IsDigit);
        var last = source.Last(char.IsDigit);

        return (first - '0') * 10 + (last - '0');
    }

    protected override object InternalPart2()
        => Input.Lines.Select(CombineFirstAndLastDigitP2).Sum();

    private readonly Dictionary<string, int> _numberLookup = new()
    {
        ["one"] = 1,
        ["two"] = 2,
        ["three"] = 3,
        ["four"] = 4,
        ["five"] = 5,
        ["six"] = 6,
        ["seven"] = 7,
        ["eight"] = 8,
        ["nine"] = 9,
        ["1"] = 1,
        ["2"] = 2,
        ["3"] = 3,
        ["4"] = 4,
        ["5"] = 5,
        ["6"] = 6,
        ["7"] = 7,
        ["8"] = 8,
        ["9"] = 9,
    };

    private int CombineFirstAndLastDigitP2(string source)
    {
        var bestIndex = source.Length + 1;
        var bestValue = "";

        foreach (KeyValuePair<string, int> pair in _numberLookup)
        {
            var index = source.IndexOf(pair.Key, StringComparison.InvariantCulture);

            if (index != -1 && index < bestIndex)
            {
                bestIndex = index;
                bestValue = pair.Value.ToString();
            }
        }

        var firstDigit = bestValue;
        bestIndex = int.MinValue;
        bestValue = "";

        foreach (KeyValuePair<string, int> pair in _numberLookup)
        {
            var index = source.LastIndexOf(pair.Key, StringComparison.InvariantCulture);

            if (index != -1 && index > bestIndex)
            {
                bestIndex = index;
                bestValue = pair.Value.ToString();
            }
        }

        var secondDigit = bestValue;


        return $"{firstDigit}{secondDigit}".ToInt32();
    }
}