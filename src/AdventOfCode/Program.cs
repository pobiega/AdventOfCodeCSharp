using AdventOfCode;

using AdventOfCodeSupport;

var solutions = new AdventSolutions();

var today = solutions.GetMostRecentDay();

await today.DownloadInputAsync();

await TrySolve(1, x => x.TrySolveSubmitPart1());
await TrySolve(2, x => x.TrySolveSubmitPart2());

return;

async Task TrySolve(int number, Func<AdventBase, Task<bool?>> func)
{
    bool? b = await func(today);

    if (!b.HasValue)
    {
        Console.WriteLine($"PAOC: Failed to solve/submit part {number}");
        return;
    }

    if (!b.Value)
    {
        Console.WriteLine($"PAOC: Part {number} answer was wrong.");
        return;
    }
}