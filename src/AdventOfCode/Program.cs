using AdventOfCode;

using AdventOfCodeSupport;

var solutions = new AdventSolutions();

var today = solutions.GetMostRecentDay();

await today.DownloadInputAsync();

today.Part1().Part2();

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