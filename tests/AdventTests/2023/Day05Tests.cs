using System.Numerics;

using AdventOfCodeSupport;

namespace AdventTests._2023;

using AdventOfCode._2023;

public class Day05Tests
{
    private string _testInput = """
                                seeds: 79 14 55 13

                                seed-to-soil map:
                                50 98 2
                                52 50 48

                                soil-to-fertilizer map:
                                0 15 37
                                37 52 2
                                39 0 15

                                fertilizer-to-water map:
                                49 53 8
                                0 11 42
                                42 0 7
                                57 7 4

                                water-to-light map:
                                88 18 7
                                18 25 70

                                light-to-temperature map:
                                45 77 23
                                81 45 19
                                68 64 13

                                temperature-to-humidity map:
                                0 69 1
                                1 0 69

                                humidity-to-location map:
                                60 56 37
                                56 93 4
                                """;

    [Fact]
    public void Parsing()
    {
        var input = new InputBlock(_testInput);
        var parser = new Day5Parser(input.Blocks);

        var seeds = parser.ParseSeeds();
        var seedToSoil = parser.ParseMap();

        var actual = seedToSoil.Get(53);
        actual.ShouldBe(55);
    }

    [Fact]
    public void Part1()
    {
        var day = new Day05();

        day.SetTestInput(_testInput);
        day.Part1();
        day.Part1Answer.ShouldBe("35");
    }

    [Fact]
    public void SoilGetRange()
    {
        // 79 to 92
        // 55 to 67
        var ranges = new List<LongRange> { new(79, 79 + 14 - 1), new(55, 55 + 13 - 1), };

        var block1 = new InputBlock("""
                                    50 98 2
                                    52 50 48
                                    """);
        // 98 99 -> 50 51
        // 50 to 97 -> 52 to 99
        var seedToSoil = RangeMap.Create(block1.Lines);

        var actual = seedToSoil.GetRanges(ranges);
        actual.Count.ShouldBe(2);
        actual[0].ShouldBeEquivalentTo(new LongRange(81, 94));
        actual[1].ShouldBeEquivalentTo(new LongRange(57, 69));
    }

    [Fact]
    public void ManualGetRange()
    {
        var ranges = new List<LongRange>
        {
            new(10, 20), // before any range
            new(30, 40), // overlap on start
            new(50, 60), // overlap on end
            new(70, 80), // after any range
        };

        var block1 = new InputBlock("""
                                    95 35 21
                                    """);
        var rangeMap = RangeMap.Create(block1.Lines);

        var actual = rangeMap.GetRanges(ranges);

        actual[0].ShouldBeEquivalentTo(ranges[0]);
        actual[1].ShouldBeEquivalentTo(new LongRange(30, 35));
        actual[2].ShouldBeEquivalentTo(new LongRange(95, 100));
        actual[3].ShouldBeEquivalentTo(new LongRange(110, 115));
        actual[4].ShouldBeEquivalentTo(new LongRange(55, 60));
        actual.Last().ShouldBeEquivalentTo(ranges[3]);
    }

    [Fact]
    public void Part2()
    {
        var day = new Day05();

        day.SetTestInput(_testInput);
        day.Part2();
        day.Part2Answer.ShouldBe("46");
    }
}