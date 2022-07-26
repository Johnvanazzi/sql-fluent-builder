namespace Lib.QueryBuilder;

public static class Converter
{
    public static string ObjectToSql(object? obj)
    {
        return obj switch
        {
            int => $"{obj}",
            string => $"'{obj}'",
            byte => $"{obj}",
            null => "NULL",
            bool val => val ? "TRUE" : "FALSE",
            double => $"{obj}",
            long => $"{obj}",
            float => $"{obj}",
            Guid => $"'{obj}'",
            DateTime val => $"'{val:s}'",
            _ => throw new ArgumentOutOfRangeException(nameof(obj), obj, "Type not found for this argument.")
        };
    }
}