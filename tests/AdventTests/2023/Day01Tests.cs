using AdventOfCode._2023;

namespace AdventTests._2023;

public class Day01Tests
{
    private string _testInput = """
                                1abc2
                                pqr3stu8vwx
                                a1b2c3d4e5f
                                treb7uchet
                                """;

    private string _part2TestInput = """
                                     two1nine
                                     eightwothree
                                     abcone2threexyz
                                     xtwone3four
                                     4nineeightseven2
                                     zoneight234
                                     7pqrstsixteen
                                     """;

    [Fact]
    public void Part1()
    {
        var day = new Day01();

        day.SetTestInput(_testInput);
        day.Part1();
        day.Part1Answer.ShouldBe("142");
    }

    [Fact]
    public void Part2()
    {
        var day = new Day01();

        day.SetTestInput(_part2TestInput);
        day.Part2();
        day.Part2Answer.ShouldBe("281");
    }
}