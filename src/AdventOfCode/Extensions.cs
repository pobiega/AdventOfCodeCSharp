using System.Globalization;

namespace AdventOfCode;

public static class Extensions
{
    public static int ToInt32(this string value) => Int32.Parse(value, CultureInfo.InvariantCulture);
    public static int ToInt32(this ReadOnlySpan<char> value) => Int32.Parse(value, provider: CultureInfo.InvariantCulture);
    public static long ToInt64(this string value) => Int64.Parse(value, CultureInfo.InvariantCulture);
    public static long ToInt64(this ReadOnlySpan<char> value) => Int64.Parse(value, provider: CultureInfo.InvariantCulture);
    public static uint ToUInt32(this string value) => UInt32.Parse(value, CultureInfo.InvariantCulture);
    public static uint ToUInt32(this ReadOnlySpan<char> value) => UInt32.Parse(value, provider: CultureInfo.InvariantCulture);
    public static ulong ToUInt64(this string value) => UInt64.Parse(value, CultureInfo.InvariantCulture);
    public static ulong ToUInt64(this ReadOnlySpan<char> value) => UInt64.Parse(value, provider: CultureInfo.InvariantCulture);

    public static int[] ToInt32(this string[] values) => [.. values.Select(value => value.ToInt32())];
    public static long[] ToInt64(this string[] values) => [.. values.Select(value => value.ToInt64())];
    public static uint[] ToUInt32(this string[] values) => [.. values.Select(value => value.ToUInt32())];
    public static ulong[] ToUInt64(this string[] values) => [.. values.Select(value => value.ToUInt64())];
    
    public static Queue<T> ToQueue<T>(this IEnumerable<T> enumerable)
    {
        var queue = new Queue<T>();
        foreach (var item in enumerable)
        {
            queue.Enqueue(item);
        }

        return queue;
    }
}