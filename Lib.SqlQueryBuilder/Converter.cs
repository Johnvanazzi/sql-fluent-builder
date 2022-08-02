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

    public static string LogicalToSql(Connective? connective) => connective switch
    {
        Connective.And => "AND",
        Connective.Or => "OR",
        _ => throw new ArgumentOutOfRangeException(nameof(connective), connective, "Logical Operator does not exist;")
    };
    
    public static string ComparerToSql(Comparer? comparer) => comparer switch
    {
        Comparer.Differs => "!=",
        Comparer.Equals => "=",
        Comparer.GreaterEqualThan => ">=",
        Comparer.GreaterThan => ">",
        Comparer.Is => "IS",
        Comparer.IsNot => "IS NOT",
        Comparer.LessThan => "<",
        Comparer.LessEqualThan => "<=",
        _ => throw new ArgumentOutOfRangeException(nameof(comparer), comparer, "Comparer Operator does not exist;")
    };
}