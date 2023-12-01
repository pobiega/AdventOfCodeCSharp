using AdventOfCodeSupport;

namespace AdventOfCode;

internal static class AdventBaseExtensions
{
    public static Task<bool?> TrySolveSubmitPart1(this AdventBase adventBase)
        => TrySolveSubmit(
            adventBase,
            x => x.Part1(),
            x => x.Part1Answer,
            x => x.SubmitPart1Async());

    public static Task<bool?> TrySolveSubmitPart2(this AdventBase adventBase)
        => TrySolveSubmit(
            adventBase,
            x => x.Part2(),
            x => x.Part2Answer,
            x => x.SubmitPart2Async());

    private static async Task<bool?> TrySolveSubmit(
        AdventBase day,
        Action<AdventBase> solver,
        Func<AdventBase, string> answer,
        Func<AdventBase, Task<bool?>> submitter)
    {
        try
        {
            solver(day);

            if (answer(day) != string.Empty)

                if (InputHelper.YesNo("Do you want to submit this answer?"))
                {
                    return await submitter(day);
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