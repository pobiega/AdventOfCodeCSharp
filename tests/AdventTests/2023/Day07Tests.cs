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


    [Fact]
    public void Idk()
    {
        var hand1 = Day07.ParseOne("JJQQQ 2");
        var hand2 = Day07.ParseOne("QQQQQ 2");
        var hand3 = Day07.ParseOne("JJJJJ 2");
        var hand4 = Day07.ParseOne("JJJJ2 2");

        var hands = new List<CamelHand> { hand2, hand1, hand3, hand4 };

        hands.Sort(new HandComparerPart2());

        hands[0].ToString().ShouldBe("JJJJ2 2");
        hands[1].ToString().ShouldBe("JJJJJ 2");
        hands[2].ToString().ShouldBe("JJQQQ 2");
        hands[3].ToString().ShouldBe("QQQQQ 2");
    }
}