using Lib.QueryBuilder.Operators;

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

    public static string LogicalOperatorToSql(Logical logical)
    {
        return logical switch
        {
            Logical.And => "AND",
            Logical.Differs => "!=",
            Logical.Equals => "=",
            Logical.Is => "IS",
            Logical.IsNot => "IS NOT",
            Logical.Or => "OR",
            _ => throw new ArgumentOutOfRangeException(nameof(logical), logical, "Logical Operator does not exist;")
        };
    }
}