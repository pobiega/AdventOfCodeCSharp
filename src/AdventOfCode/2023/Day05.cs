using AdventOfCodeSupport;

namespace AdventOfCode._2023;

public sealed class Day05 : AdventBase
{
    protected override void InternalOnLoad()
    {
        base.InternalOnLoad();
    }

    protected override object InternalPart1()
    {
        var (seeds, maps) = Parse();

        return seeds.Min(seed => maps.Aggregate(seed, MapForward));
    }

    private long MapForward(long value, List<(long Dst, long Src, long Len)> map)
    {
        foreach (var range in map)
        {
            if (range.Src <= value && value < range.Src + range.Len)
            {
                return range.Dst - range.Src + value;
            }
        }

        return value;
    }

    private long MapReverse(long value, List<(long Dst, long Src, long Len)> map)
    {
        foreach (var range in map)
        {
            if (range.Dst <= value && value < range.Dst + range.Len)
            {
                return range.Src - range.Dst + value;
            }
        }

        return value;
    }

    private (List<long> seeds, List<List<(long Dst, long Src, long Len)>> maps) Parse()
    {
        var seeds = Input.Lines.First().Split(' ').Skip(1).Select(x => x.ToInt64()).ToList();
        var maps = new List<List<(long, long, long)>>();

        foreach (var line in Input.Lines.Skip(1))
        {
            if (line.Length is 0)
            {
                continue;
            }

            if (line.EndsWith(':'))
            {
                maps.Add([]);
            }
            else
            {
                var values = line.Split(' ').ToInt64();
                maps[^1].Add((values[0], values[1], values[2]));
            }
        }

        return (seeds, maps);
    }

    protected override object InternalPart2()
    {
        var (seeds, maps) = Parse();

        return maps
            .SelectMany((map, i) => map.Select(range => maps.Take(i + 1).Reverse().Aggregate(range.Dst, MapReverse)))
            .Where(seed => seeds.Chunk(2).Any(pair => pair[0] <= seed && seed < pair[0] + pair[1]))
            .Min(seed => maps.Aggregate(seed, MapForward));
    }
}