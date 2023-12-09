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

    [Fact]
    public void CanUpgrade()
    {
        var hand = Day07.ParseOne("QJJQ2 500");
        var upgraded = Day07.MakeStrongestHand(hand);

        upgraded.Type.ShouldBe(HandType.FourOfAKind);

        var fiveHand = Day07.ParseOne("JJQQQ 1");
        var upgFiveHand = Day07.MakeStrongestHand(fiveHand);
        upgFiveHand.Type.ShouldBe(HandType.FiveOfAKind);
    }

    [Fact]
    public void Idk()
    {
        var hand1 = Day07.ParseOne("JJQQQ 1");
        var hand2 = Day07.ParseOne("QQQQQ 2");
        var hand3 = Day07.ParseOne("JJJJJ 3");
        var hand4 = Day07.ParseOne("JJJJ2 3");

        var hands = new List<CamelHand> { hand2, hand1, hand3, hand4 };

        hands.Sort(new HandComparer(true));

        hands[0].ToString().ShouldBe("JJQQQ 1");
        hands[1].ToString().ShouldBe("QQQQQ 2");
    }
}