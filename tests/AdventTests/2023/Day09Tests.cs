using AdventOfCode._2023;

namespace AdventTests._2023;

public class Day09Tests
{
    private string _testInput = """
                                0 3 6 9 12 15
                                1 3 6 10 15 21
                                10 13 16 21 30 45
                                """;

    [Fact]
    public void Part1()
    {
        var day = new Day09();

        day.SetTestInput(_testInput);
        day.Part1();
        day.Part1Answer.ShouldBe("114");
    }

    [Fact]
    public void Part2()
    {
        var day = new Day09();

        day.SetTestInput(_testInput);
        day.Part2();
        day.Part2Answer.ShouldBe("2");
    }
}