﻿using AdventOfCodeSupport;

namespace AdventOfCode._2023;

public sealed class Day02 : AdventBase
{
    private Game[] _games = null!;

    protected override void InternalOnLoad()
    {
        base.InternalOnLoad();

        _games = Input.Lines.Select(Game.Parse).ToArray();
    }

    protected override object InternalPart1()
    {
        const int maxReds = 12;
        const int maxGreens = 13;
        const int maxBlues = 14;

        return _games
            .Where(x => x.Sets.All(y => y is { Red: <= maxReds, Green: <= maxGreens, Blue: <= maxBlues }))
            .Select(x => x.Id)
            .Sum();
    }

    protected override object InternalPart2()
    {
        return _games
            .Select(GetMinimumSet)
            .Select(x => x.Red * x.Blue * x.Green)
            .Sum();
    }

    internal Set GetMinimumSet(Game game)
    {
        var red = 0;
        var green = 0;
        var blue = 0;

        foreach (Set set in game.Sets)
        {
            red = Math.Max(set.Red, red);
            green = Math.Max(set.Green, green);
            blue = Math.Max(set.Blue, blue);
        }

        return new Set(red, green, blue);
    }

    public record Game(int Id, Set[] Sets)
    {
        public static Game Parse(string line)
        {
            var parts = line.Split(':', StringSplitOptions.TrimEntries);

            var setParts = parts[1].Split(';', StringSplitOptions.TrimEntries);
            var sets = setParts.Select(Set.Parse).ToArray();
            var id = parts[0].Split(' ')[1].ToInt32();
            return new Game(id, sets);
        }
    }

    public record Set(int Red, int Green, int Blue)
    {
        public static Set Parse(string data)
        {
            var red = 0;
            var green = 0;
            var blue = 0;

            var parts = data.Split(',', StringSplitOptions.TrimEntries);

            foreach (string part in parts)
            {
                var partData = part.Split(' ');
                var number = partData[0].ToInt32();
                var color = partData[1];

                switch (color)
                {
                    case "blue":
                        blue += number;
                        break;
                    case "red":
                        red += number;
                        break;
                    case "green":
                        green += number;
                        break;
                }
            }

            return new Set(red, green, blue);
        }
    }
}