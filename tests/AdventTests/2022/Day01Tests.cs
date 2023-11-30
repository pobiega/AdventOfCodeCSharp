using AdventOfCode2023._2022;
using Shouldly;

public class Day01Tests
{
    private string _testInput = """
                           1000
                           2000
                           3000

                           4000

                           5000
                           6000

                           7000
                           8000
                           9000

                           10000
                           """;


    [Fact]
    public void Part1()
    {
        var day = new Day01();

        day.SetTestInput(_testInput);
        day.Part1();
        day.Part1Answer.ShouldBe("24000");
    }

    [Fact]
    public void Part2()
    {
        var day = new Day01();

        day.SetTestInput(_testInput);
        day.Part2();
        day.Part2Answer.ShouldBe("45000");
    }
}
