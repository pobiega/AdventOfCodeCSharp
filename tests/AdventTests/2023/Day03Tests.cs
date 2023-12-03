using AdventOfCode._2023;

namespace AdventTests._2023;

public class Day03Tests
{
    private string _testInput = """
                                467..114..
                                ...*......
                                ..35..633.
                                ......#...
                                617*......
                                .....+.58.
                                ..592.....
                                ......755.
                                ...$.*....
                                .664.598..
                                """;

    private string _edgeInput = """
                                ...100
                                ....*.
                                ....10
                                ..*100
                                """;

    [Fact]
    public void Part1()
    {
        var day = new Day03();

        day.SetTestInput(_testInput);
        day.Part1();
        day.Part1Answer.ShouldBe("4361");
    }
    
    [Fact]
    public void Part1_Edges()
    {
        var day = new Day03();

        day.SetTestInput(_edgeInput);
        day.Part1();
        day.Part1Answer.ShouldBe("210");
    }

    [Fact]
    public void Part2()
    {
        var day = new Day03();

        day.SetTestInput(_testInput);
        day.Part2();
        day.Part2Answer.ShouldBe("467835");
    }
}