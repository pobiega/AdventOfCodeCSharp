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

        var cardId = gameAndScore[0]
            .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)[1]
            .ToInt32();

        var scores = gameAndScore[1].Split('|', StringSplitOptions.TrimEntries);

        var winningNumbers = scores[0]
            .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)
            .ToArray();

        var numbers = scores[1]
            .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse).ToArray();

        var numberOfWins = winningNumbers.Intersect(numbers).Count();

        return new ScratchCard(cardId, numberOfWins);
    }

    protected override object InternalPart1()
    {
        long points = 0;
        foreach (ScratchCard card in _cards)
        {
            long cardPoints = 0;

            for (int i = 0; i < card.NumberOfWins; i++)
            {
                cardPoints = cardPoints switch
                {
                    0 => 1,
                    _ => cardPoints * 2
                };
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

            for (int i = 0; i < card.NumberOfWins; i++)
            {
                var newCard = _cards.First(x => x.CardId == card.CardId + i + 1);
                allCards.Add(newCard);
                cardQueue.Enqueue(newCard);
            }
        }

        return allCards.Count;
    }
}

internal record ScratchCard(int CardId, int NumberOfWins);