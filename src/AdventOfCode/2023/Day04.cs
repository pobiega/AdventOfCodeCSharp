using AdventOfCodeSupport;

namespace AdventOfCode._2023;

public sealed class Day04 : AdventBase
{
    private ScratchCard[] _cards = null!;

    protected override void InternalOnLoad()
    {
        base.InternalOnLoad();
        _cards = Input.Lines.Select(ParseCard).ToArray();
    }

    private ScratchCard ParseCard(string arg)
    {
        var gameAndScore = arg.Split(':', StringSplitOptions.TrimEntries);

        var cardId = int.Parse(gameAndScore[0]
            .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)[1]);

        var scores = gameAndScore[1].Split('|', StringSplitOptions.TrimEntries);

        var winningNumbers = scores[0]
            .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)
            .ToArray();
        var numbers = scores[1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse).ToArray();

        return new ScratchCard(cardId, winningNumbers, numbers);
    }

    // internal static List<ScratchCard> ParseCard(string input) =>
    //     String("Card ")
    //         .Then(Num)
    //         .Before(String(": "))
    //         .Bind<char, IEnumerable<ScratchCard>>(cardId => (
    //             Num
    //                 .Separated(String(" "))
    //                 .Before(String(" | "))
    //                 .Map(x => new ScratchCard(cardId, x.ToArray(), null!))
    //         )
    //         .Separated(String("\n"))
    //         .ParseOrThrow(input)
    //         .ToList());


    protected override object InternalPart1()
    {
        long points = 0;
        foreach (ScratchCard card in _cards)
        {
            long cardPoints = 0;
            foreach (int number in card.Numbers)
            {
                if (!card.WinningNumbers.Contains(number))
                {
                    continue;
                }

                if (cardPoints == 0)
                {
                    cardPoints = 1;
                }
                else
                {
                    cardPoints *= 2;
                }
            }

            points += cardPoints;
        }

        return points;
    }

    protected override object InternalPart2()
    {
        var cardQueue = new Queue<ScratchCard>(_cards);
        var allCards = new List<ScratchCard>(_cards);

        while (cardQueue.Count > 0)
        {
            var card = cardQueue.Dequeue();

            var wins = card.Numbers.Count(number => card.WinningNumbers.Contains(number));

            for (int i = 0; i < wins; i++)
            {
                var newCard = _cards.First(x => x.CardId == card.CardId + i + 1);
                allCards.Add(newCard);
                cardQueue.Enqueue(newCard);
            }
        }

        return allCards.Count;
    }
}

internal record ScratchCard(int CardId, int[] WinningNumbers, int[] Numbers);