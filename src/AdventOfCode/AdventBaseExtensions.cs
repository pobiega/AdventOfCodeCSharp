using AdventOfCodeSupport;

namespace AdventOfCode2023;

internal static class AdventBaseExtensions
{
    public static Task<bool?> TrySolveSubmitPart1(this AdventBase adventBase)
        => TrySolveSubmit(
            adventBase,
            x => x.Part1(),
            x => x.CheckPart1Async(),
            x => x.SubmitPart1Async());

    public static Task<bool?> TrySolveSubmitPart2(this AdventBase adventBase)
        => TrySolveSubmit(
            adventBase,
            x => x.Part2(),
            x => x.CheckPart2Async(),
            x => x.SubmitPart2Async());

    private static async Task<bool?> TrySolveSubmit(
        AdventBase day,
        Action<AdventBase> solver,
        Func<AdventBase, Task<bool?>> checker,
        Func<AdventBase, Task<bool?>> submitter)
    {
        try
        {
            solver(day);
            var result = await checker(day);

            if (result.HasValue && !result.Value)
            {
                if (InputHelper.YesNo("Do you want to submit this answer?"))
                {
                    return await submitter(day);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception caught during TrySolveSubmit: {ex}");
            throw;
        }

        return null;
    }
}
