using AdventOfCodeSupport;

namespace AdventOfCode2023._2022;

public sealed class Day01 : AdventBase
{
    protected override object InternalPart1()
        => Input.Blocks
            .Select(x => x.Lines
                .Select(int.Parse)
                .Sum())
            .Max();

    protected override object InternalPart2()
        => Input.Blocks
            .Select(x => x.Lines
                .Select(int.Parse)
                .Sum())
            .OrderDescending()
            .Take(3)
            .Sum();
}
