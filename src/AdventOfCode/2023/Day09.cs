using AdventOfCodeSupport;

using SuperLinq;

namespace AdventOfCode._2023;

public sealed class Day09 : AdventBase
{
    protected override object InternalPart1()
    {
        var values = Input.Lines.Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToInt32().ToList());

        var total = 0;

        foreach (var vals in values)
        {
            var newValues = new List<List<int>>();

            var diff = vals;

            do
            {
                newValues.Add(diff);
                diff = CalculateDiff(diff);
            } while (diff.Any(x => x != 0));

            // traverse the right side

            newValues.Reverse();
            // newValues[0].Add(0);

            var lastValue = 0;
            foreach (List<int> value in newValues)
            {
                var lastInValue = value.Last();

                lastValue = lastInValue + lastValue;
                value.Add(lastValue);
            }

            total += newValues.Last().Last();
        }

        return total;
    }

    private List<int> CalculateDiff(List<int> vals)
    {
        return vals
            .Window(2)
            .Select(window => window[1] - window[0])
            .ToList();
    }

    protected override object InternalPart2()
    {
        var values = Input.Lines.Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToInt32().ToList());

        var total = 0;

        foreach (var vals in values)
        {
            var newValues = new List<List<int>>();

            var diff = vals;

            do
            {
                newValues.Add(diff);
                diff = CalculateDiff(diff);
            } while (diff.Any(x => x != 0));

            // traverse the left side

            newValues.Reverse();

            var lastValue = 0;
            foreach (List<int> value in newValues)
            {
                var lastInValue = value.First();

                lastValue = lastInValue - lastValue;
                value.Insert(0, lastValue);
            }

            total += newValues.Last().First();
        }

        return total;
    }
}