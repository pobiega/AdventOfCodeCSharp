using AdventOfCode._2023;

namespace AdventTests._2023;

public class Day08Tests
{
    [Fact]
    public void Part1()
    {
        var day = new Day08();

        day.SetTestInput("""
                         RL

                         AAA = (BBB, CCC)
                         BBB = (DDD, EEE)
                         CCC = (ZZZ, GGG)
                         DDD = (DDD, DDD)
                         EEE = (EEE, EEE)
                         GGG = (GGG, GGG)
                         ZZZ = (ZZZ, ZZZ)
                         """);
        day.Part1();
        day.Part1Answer.ShouldBe("2");
    }
    
    [Fact]
    public void Part1_2()
    {
        var day = new Day08();

        day.SetTestInput("""
                         LLR
                         
                         AAA = (BBB, BBB)
                         BBB = (AAA, ZZZ)
                         ZZZ = (ZZZ, ZZZ)
                         """);
        day.Part1();
        day.Part1Answer.ShouldBe("6");
    }

    [Fact]
    public void Part2()
    {
        var day = new Day08();

        day.SetTestInput("""
                         RL

                         AAA = (BBB, CCC)
                         BBB = (DDD, EEE)
                         CCC = (ZZZ, GGG)
                         DDD = (DDD, DDD)
                         EEE = (EEE, EEE)
                         GGG = (GGG, GGG)
                         ZZZ = (ZZZ, ZZZ)
                         """);
        day.Part2();
        day.Part2Answer.ShouldBe("x");
    }
}