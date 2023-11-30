namespace AdventOfCode2023;

internal static class InputHelper
{
    public static bool YesNo(string prompt = "Confirm? (y/n): ")
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();

            bool? result = input?.ToLowerInvariant() switch
            {
                "yes" or "y" or "true" => true,
                "no" or "n" or "false" => false,
                _ => null
            };

            if (result.HasValue)
                return result.Value;
        }
    }
}
