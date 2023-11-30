using AdventOfCode2023;
using AdventOfCodeSupport;

var solutions = new AdventSolutions();

var today = solutions.GetMostRecentDay();

var done = await today.TrySolveSubmitPart1();

if (!done.HasValue)
{
    Console.WriteLine("PAOC: Failed to solve/submit part 1");
    return;
}

if (!done.Value)
{
    Console.WriteLine("PAOC: Part 1 answer was wrong.");
    return;
}

done = await today.TrySolveSubmitPart2();

if (!done.HasValue)
{
    Console.WriteLine("PAOC: Failed to solve/submit part 2");
    return;
}

if (!done.Value)
{
    Console.WriteLine("PAOC: Part 2 answer was wrong.");
    return;
}
