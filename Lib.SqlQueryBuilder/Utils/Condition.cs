using System.Text;
using Lib.QueryBuilder.Operators;

namespace Lib.QueryBuilder.Utils;

public class Condition : IComparer, IConnective
{
    public StringBuilder Sb { get; init; }

    public Condition()
    {
        Sb = new StringBuilder();
    }

    public IConnective Differs(string column, object? value) => AppendCondition(column, Comparer.Differs, value);
    public IConnective Equals(string column, object? value) => AppendCondition(column, Comparer.Equals, value);

    public IConnective Exists(Query subQuery)
    {
        Sb.Append($"EXISTS ({subQuery.ToSql()})");

        return this;
    }

    public IConnective Greater(string column, object? value) => AppendCondition(column, Comparer.Greater, value);
    public IConnective GreaterEqual(string column, object? value) => AppendCondition(column, Comparer.GreaterEqual, value);
    public IConnective In(string column, params object?[] values) => AppendCondition(column, Comparer.In, values);

    public IConnective In(string column, Query subQuery) => AppendCondition(column, Comparer.In, subQuery);
    public IConnective Is(string column, object? value) => AppendCondition(column, Comparer.Is, value);
    public IConnective IsNot(string column, object? value) => AppendCondition(column, Comparer.IsNot, value);
    public IConnective Less(string column, object? value) => AppendCondition(column, Comparer.Less, value);
    public IConnective LessEqual(string column, object? value) => AppendCondition(column, Comparer.LessEqual, value);
    
    public IConnective Nested(Action<Condition> condition)
    {
        Sb.Append('(');
        condition(this);
        Sb.Append(')');

        return this;
    }

    public IConnective NotBetween(string column, object? minimum, object? maximum) =>
        AppendBetween(column, Comparer.NotBetween, minimum, maximum);

    public IConnective NotIn(string column, params object?[] value) => AppendCondition(column, Comparer.NotIn, value);
    public IConnective NotIn(string column, Query subQuery) => AppendCondition(column, Comparer.NotIn, subQuery);

    public IConnective All(string column, Comparer comparer, Query subQuery)
    {
        Sb.Append($"{column} {comparer.ToSql()} ALL ({subQuery.ToSql()})");
        
        return this;
    }
    
    public IConnective Any(string column, Comparer comparer, Query subQuery)
    {
        Sb.Append($"{column} {comparer.ToSql()} {Comparer.Any.ToSql()} ({subQuery.ToSql()})");
        
        return this;
    }

    public IConnective Between(string column, object? minimum, object? maximum) =>
        AppendBetween(column, Comparer.Between, minimum, maximum);

    public IConnective NotLike(string column, string value) => AppendCondition(column, Comparer.NotLike, value);
    public IConnective Like(string column, string value) => AppendCondition(column, Comparer.Like, value);

    public IComparer And() => AppendConnective("AND");
    public IComparer Or() => AppendConnective("OR");

    private IConnective AppendBetween(string column, Comparer comparer , object? minimum, object? maximum)
    {
        Sb.Append($"({column} {comparer.ToSql()} {minimum.ToSql()} AND {maximum.ToSql()})");

        return this;
    }
    
    private IConnective AppendCondition(string column, Comparer comparer, object? value)
    {
        Sb.Append($"({column} {comparer.ToSql()} {value.ToSql()})");
        return this;
    }

    private IComparer AppendConnective(string connective)
    {
        Sb.Append($" {connective} ");

        return this;
    }
}