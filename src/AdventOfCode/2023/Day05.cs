using AdventOfCodeSupport;

namespace AdventOfCode._2023;

public sealed class Day05 : AdventBase
{
    protected override object InternalPart1()
    {
        var parser = new Day5Parser(Input.Blocks);
        var seeds = parser.ParseSeeds();
        var seedToSoil = parser.ParseMap();
        var soilToFert = parser.ParseMap();
        var fertToWater = parser.ParseMap();
        var waterToLight = parser.ParseMap();
        var lightToTemp = parser.ParseMap();
        var tempToHum = parser.ParseMap();
        var humToLoc = parser.ParseMap();

        var location = seeds
            .Select(x => seedToSoil.Get(x))
            .Select(x => soilToFert.Get(x))
            .Select(x => fertToWater.Get(x))
            .Select(x => waterToLight.Get(x))
            .Select(x => lightToTemp.Get(x))
            .Select(x => tempToHum.Get(x))
            .Select(x => humToLoc.Get(x))
            .Min();

        return location;
    }

    protected override object InternalPart2()
    {
        var parser = new Day5Parser(Input.Blocks);
        var seeds = parser.ParseSeedRange();

        var seedToSoil = parser.ParseMap();
        var soilToFert = parser.ParseMap();
        var fertToWater = parser.ParseMap();
        var waterToLight = parser.ParseMap();
        var lightToTemp = parser.ParseMap();
        var tempToHum = parser.ParseMap();
        var humToLoc = parser.ParseMap();

        var maps = new[] { seedToSoil, soilToFert, fertToWater, waterToLight, lightToTemp, tempToHum, humToLoc };

        var soil = seedToSoil.GetRanges(seeds);
        var fert = soilToFert.GetRanges(soil);
        var water = fertToWater.GetRanges(fert);
        var light = waterToLight.GetRanges(water);
        var temp = lightToTemp.GetRanges(light);
        var hum = tempToHum.GetRanges(temp);
        var loc = humToLoc.GetRanges(hum);

        return loc
                   .MinBy(x => x.From)!
                   .From
               - 1;
    }
}

public class Day5Parser
{
    private Queue<InputBlock> _inputBlocks;

    public Day5Parser(InputBlock[] inputBlocks)
    {
        _inputBlocks = inputBlocks.ToQueue();
    }

    public List<long> ParseSeeds()
    {
        var block = _inputBlocks.Dequeue();

        var line = block.Text;
        var split = line.Split(": ")[1].Split(' ').Select(long.Parse);

        return split.ToList();
    }

    public RangeMap ParseMap()
    {
        var block = _inputBlocks.Dequeue();
        var lines = block.Lines.Skip(1);
        return RangeMap.Create(lines);
    }

    public List<LongRange> ParseSeedRange()
    {
        var block = _inputBlocks.Dequeue();
        var line = block.Text;

        return line
            .Split(": ")[1]
            .Split(' ')
            .Select(long.Parse)
            .Chunk(2)
            .Select(x => new LongRange(x[0], x[0] - 1))
            .ToList();
    }
}

public class RangeMap
{
    public MappedRange[] MappedRanges { get; }

    private RangeMap(MappedRange[] mappedRanges)
    {
        MappedRanges = mappedRanges;
    }

    public static RangeMap Create(IEnumerable<string> lines)
    {
        var x = lines
            .Select(x => x.Split(' ').Select(long.Parse).ToArray())
            .Select(line => new MappedRange(line[0], line[1], line[2]))
            .OrderBy(x => x.From)
            .ToArray();

        return new RangeMap(x);
    }

    public long Get(long key)
    {
        foreach (MappedRange range in MappedRanges)
        {
            var rangeValue = range.Get(key);
            if (rangeValue.HasValue)
            {
                return rangeValue.Value;
            }
        }

        return key;
    }

    public List<LongRange> GetRanges(List<LongRange> ranges)
    {
        var newRanges = new List<LongRange>(ranges.Count * 2);

        foreach (LongRange range in ranges)
        {
            var (from, to) = range;

            bool addEnding = true;
            foreach (MappedRange mappedRange in MappedRanges)
            {
                if (from > mappedRange.To)
                {
                    continue;
                }

                if (to <= mappedRange.From)
                {
                    newRanges.Add(range);
                    addEnding = false;
                    break;
                }

                // we have an overlap

                if (from < mappedRange.From)
                {
                    newRanges.Add(new LongRange(from, mappedRange.From));
                    from = mappedRange.From;
                }

                var startOffset = mappedRange.Adjustment + from;
                if (to <= mappedRange.To)
                {
                    newRanges.Add(new LongRange(startOffset, mappedRange.Adjustment + to));
                    addEnding = false;
                    break;
                }

                newRanges.Add(new LongRange(startOffset, mappedRange.Adjustment + mappedRange.To));
                from = mappedRange.To;
            }

            if (addEnding && from != to)
            {
                newRanges.Add(new LongRange(from, to));
            }
        }

        return newRanges;
    }
}

public class MappedRange
{
    public MappedRange(long destinationStart, long sourceStart, long length)
    {
        Adjustment = destinationStart - sourceStart;
        From = sourceStart;
        To = sourceStart + length - 1;
    }

    public long To { get; }

    public long From { get; }

    public long Adjustment { get; }

    public long? Get(long key)
    {
        if (key >= From && key <= To)
        {
            return key + Adjustment;
        }

        return null;
    }
}

public record LongRange(long From, long To);