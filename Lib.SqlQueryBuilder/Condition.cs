using Lib.QueryBuilder.Operators;

namespace Lib.QueryBuilder;

public class Condition
{
    public string? Column { get; }
    public Comparer? Comparer { get; }
    public object? Value { get; }
    public Connective? Connective { get; }
    public Condition[]? SubConditions { get; }

    public Condition(string column, Comparer comparer, object? value)
    {
        Column = column;
        Comparer = comparer;
        Value = value;
    }

    public Condition(string column, Comparer comparer, object? value, Connective connective)
    {
        Column = column;
        Comparer = comparer;
        Value = value;
        Connective = connective;
    }
    
    public Condition(Condition[]? subConditions, Connective? connective = null)
    {
        SubConditions = subConditions;
        Connective = connective;
    }
}