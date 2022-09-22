using System.Text;
using Lib.QueryBuilder.Operators;

namespace Lib.QueryBuilder.Utils;

public static class Converter
{
    public static string ToSql(this object? obj) => obj switch
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
        Array => ArrayToSqlString(obj),
        Query => obj.ToSql(),
        _ => throw new ArgumentOutOfRangeException(nameof(obj), obj,
            $"Could not convert value to SQL string because the Type '{obj.GetType()}' provided is not supported")
    };

    public static string ToSql(this Connective connective) => connective switch
    {
        Connective.And => "AND",
        Connective.Or => "OR",
        _ => throw new ArgumentOutOfRangeException(nameof(connective), connective, "Logical Operator does not exist;")
    };
    
    public static string ToSql(this Comparer comparer) => comparer switch
    {
        Comparer.All => "ALL",
        Comparer.Any => "ANY",
        Comparer.Between => "BETWEEN",
        Comparer.Differs => "!=",
        Comparer.Equals => "=",
        Comparer.GreaterEqual => ">=",
        Comparer.Greater => ">",
        Comparer.Is => "IS",
        Comparer.IsNot => "IS NOT",
        Comparer.Less => "<",
        Comparer.LessEqual => "<=",
        Comparer.Like => "LIKE",
        Comparer.NotLike => "NOT LIKE",
        Comparer.NotBetween => "NOT BETWEEN",
        _ => throw new ArgumentOutOfRangeException(nameof(comparer), comparer, "Comparer Operator does not exist;")
    };

    private static string ArrayToSqlString(object? array)
    {
        var sb = new StringBuilder();
        sb.Append('(').AppendJoin(", ", array as Array).Append(')');
        
        return sb.ToString();
    }
}