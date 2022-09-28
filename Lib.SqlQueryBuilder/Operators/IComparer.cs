namespace Lib.QueryBuilder.Operators;

public interface IComparer
{
    public IConnective All(string column, Comparer comparer, Query subQuery);
    public IConnective Any(string column, Comparer comparer, Query subQuery);
    public IConnective Between(string column, object? minimum, object? maximum);
    public IConnective Differs(string column, object? value);
    public IConnective Equals(string column, object? value);
    public IConnective Exists(Query subQuery);
    public IConnective Greater(string column, object? value);
    public IConnective GreaterEqual(string column, object? value);
    public IConnective In(string column, params object?[] values);
    public IConnective In(string column, Query subQuery);
    public IConnective IsNull(string column);
    public IConnective IsNotNull(string column);
    public IConnective Less(string column, object? value);
    public IConnective LessEqual(string column, object? value);
    public IConnective Like(string column, string value);
    public IConnective Nested(Action<Condition> condition);
    public IConnective NotBetween(string column, object? minimum, object? maximum);
    public IConnective NotIn(string column, params object?[] values);
    public IConnective NotIn(string column, Query subQuery);
    public IConnective NotLike(string column, string value);
}