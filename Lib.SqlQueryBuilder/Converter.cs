using Lib.QueryBuilder.Operators;

namespace Lib.QueryBuilder;

public static class Converter
{
    public static string ObjectToSql(object? obj) => obj switch
    {
        null => "NULL",
        int => $"{obj}",
        long => $"{obj}",
        char => $"'{obj}'",
        string => $"'{obj}'",
        byte => $"{obj}",
        bool val => val ? "TRUE" : "FALSE",
        decimal => $"{obj}",
        double => $"{obj}",
        float => $"{obj}",
        Guid => $"'{obj}'",
        DateTime val => $"'{val:s}'",
        _ => throw new ArgumentOutOfRangeException(nameof(obj), obj, $"Could not convert value to SQL string because the Type '{obj.GetType()}' provided is not supported")
    };

    public static string LogicalOperatorToSql(Logical logical) => logical switch
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