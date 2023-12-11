using AdventOfCode._2023;

namespace AdventTests._2023;

public class Day07Tests
{
    private string _testInput = """
                                32T3K 765
                                T55J5 684
                                KK677 28
                                KTJJT 220
                                QQQJA 483
                                """;

    [Fact]
    public void Part1()
    {
        var day = new Day07();

        day.SetTestInput(_testInput);
        day.Part1();
        day.Part1Answer.ShouldBe("6440");
    }

    [Fact]
    public void Part2()
    {
        var day = new Day07();

        day.SetTestInput(_testInput);
        day.Part2();
        day.Part2Answer.ShouldBe("5905");
    }

    [Theory]
    [InlineData("JJQQQ 1", HandType.FiveOfAKind)]
    [InlineData("QJJQ2 500", HandType.FourOfAKind)]
    [InlineData("J39Q5 1", HandType.OnePair)]
    public void CanUpgrade(string source, HandType expected)
    {
        var hand = Day07.ParseOne(source);
        var upgraded = Day07.MakeStrongestHand(hand);
        upgraded.Type.ShouldBe(expected);
    }
}