using AdventOfCode._2023;

namespace AdventTests._2023;

public class Day10Tests
{
    private string _testInput = """
                                -L|F7
                                7S-7|
                                L|7||
                                -L-J|
                                L|-JF
                                """;

    [Fact]
    public void Part1()
    {
        var day = new Day10();

        day.SetTestInput(_testInput);
        day.Part1();
        day.Part1Answer.ShouldBe("4");
    }

    [Fact]
    public void Part2()
    {
        var day = new Day10();

        day.SetTestInput(_testInput);
        day.Part2();
        day.Part2Answer.ShouldBe("x");
    }
}