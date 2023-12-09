using AdventOfCodeSupport;

namespace AdventOfCode._2023;

public sealed class Day07 : AdventBase
{
    protected override object InternalPart1()
    {
        var hands = Parse();
        hands.Sort(_part1Comparer);

        var score = hands
            .Select((hand, i) => hand.Bid * (i + 1))
            .Sum();

        return score;
    }

    private List<CamelHand> Parse()
    {
        return Input.Lines.Select(ParseOne).ToList();
    }

    public static CamelHand ParseOne(string line)
    {
        var cards = line.Split(' ')[0].Select(CharToCard).ToArray();

        var bid = line.Split(' ')[1].ToInt32();

        if (cards.All(x => x == cards[0]))
        {
            return new CamelHand(cards, HandType.FiveOfAKind, bid);
        }

        var counts = cards.GroupBy(x => x).Select(g => g.Count()).ToArray();

        if (counts.Any(x => x == 4))
        {
            return new CamelHand(cards, HandType.FourOfAKind, bid);
        }

        if (counts.Any(x => x == 3) && counts.Any(x => x == 2))
        {
            return new CamelHand(cards, HandType.FullHouse, bid);
        }

        if (counts.Any(x => x == 3))
        {
            return new CamelHand(cards, HandType.ThreeOfAKind, bid);
        }

        if (counts.Count(x => x == 2) == 2)
        {
            return new CamelHand(cards, HandType.TwoPair, bid);
        }

        if (counts.Contains(2))
        {
            return new CamelHand(cards, HandType.OnePair, bid);
        }

        return new CamelHand(cards, HandType.HighCard, bid);
    }

    protected override object InternalPart2()
    {
        var hands = Parse();
        hands.Sort(_part2Comparer);

        var score = hands
            .Select((hand, i) => hand.Bid * (i + 1))
            .Sum();

        return score;
    }

    public static CamelCard CharToCard(char c)
    {
        return c switch
        {
            '2' => CamelCard.Two,
            '3' => CamelCard.Three,
            '4' => CamelCard.Four,
            '5' => CamelCard.Five,
            '6' => CamelCard.Six,
            '7' => CamelCard.Seven,
            '8' => CamelCard.Eight,
            '9' => CamelCard.Nine,
            'T' => CamelCard.T,
            'J' => CamelCard.J,
            'Q' => CamelCard.Q,
            'K' => CamelCard.K,
            'A' => CamelCard.A,
            _ => throw new ArgumentException($"Invalid character {c}", nameof(c))
        };
    }

    private static readonly HandComparer _part2Comparer = new(true);
    private static readonly HandComparer _part1Comparer = new();

    public static CamelHand MakeStrongestHand(CamelHand hand)
    {
        if (hand.Cards.All(x => x != CamelCard.J))
        {
            return hand;
        }

        var handStr = hand.ToString();

        var potentialHands = Enum.GetValues<CamelCard>()
            .Select(card => handStr.Replace("J", card.GetStrValue()))
            .Select(ParseOne)
            .OrderByDescending(x => x.Type)
            .ToList();

        var bestType = potentialHands[0].Type;

        potentialHands = potentialHands
            .Where(y => y.Type == bestType)
            .ToList();

        potentialHands.Sort(_part2Comparer);

        return potentialHands[0];
    }
}

public enum CamelCard
{
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    T,
    J,
    Q,
    K,
    A,
}

public enum HandType
{
    HighCard,
    OnePair,
    TwoPair,
    ThreeOfAKind,
    FullHouse,
    FourOfAKind,
    FiveOfAKind,
}

public record CamelHand(CamelCard[] Cards, HandType Type, int Bid)
{
    public override string ToString()
        => $"{string.Join("", this.Cards.Select(x => x.GetStrValue()))} {Bid}";
}

public class HandComparer : Comparer<CamelHand>
{
    private readonly bool _isPart2;

    public HandComparer(bool isPart2 = false)
    {
        _isPart2 = isPart2;
    }

    public override int Compare(CamelHand? x, CamelHand? y)
    {
        if (x is null || y is null)
        {
            throw new ArgumentNullException("x or y", "Can't compare nulls");
        }

        if (x.Type != y.Type)
        {
            if (!_isPart2)
            {
                return x.Type.CompareTo(y.Type);
            }

            var newX = Day07.MakeStrongestHand(x);
            var newY = Day07.MakeStrongestHand(y);

            if (newX.Type != newY.Type)
            {
                return newX.Type.CompareTo(newY.Type);
            }
        }

        foreach ((CamelCard first, CamelCard second) in x.Cards.Zip(y.Cards))
        {
            if (first == second)
            {
                continue;
            }

            if (_isPart2)
            {
                if (first == CamelCard.J)
                    return -1;
                if (second == CamelCard.J)
                    return 1;
            }

            return first.CompareTo(second);
        }

        return 0;
    }
}

public static class Day07Extensions
{
    public static string GetStrValue(this CamelCard card)
        => card switch
        {
            CamelCard.Two => "2",
            CamelCard.Three => "3",
            CamelCard.Four => "4",
            CamelCard.Five => "5",
            CamelCard.Six => "6",
            CamelCard.Seven => "7",
            CamelCard.Eight => "8",
            CamelCard.Nine => "9",
            _ => card.ToString()
        };
}