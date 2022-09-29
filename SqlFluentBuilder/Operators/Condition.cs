using System.Text;
using SqlFluentBuilder.Utils;

namespace SqlFluentBuilder.Operators;

public class Condition : IComparer, IConnective
{
    public StringBuilder Sb { get; }

    IComparer IConnective.And
    {
        get
        {
            Sb.Append(" AND ");
            return this;
        }
    }

    IComparer IConnective.Or
    {
        get
        {
            Sb.Append(" OR ");
            return this;
        }
    }

    public Condition()
    {
        Sb = new StringBuilder();
    }

    public string ToSql() => Sb.ToString();

    /// <summary>
    /// Appends the SQL difference comparer to the builder.
    /// </summary>
    /// <param name="column">The column that is being compared.</param>
    /// <param name="value">The value the column will be compared against.</param>
    /// <returns></returns>
    public IConnective Differs(string column, object? value) => AppendCondition(column, Comparer.Differs, value);

    /// <summary>
    /// Appends the SQL equality comparer to the builder.
    /// </summary>
    /// <param name="column">The column that is being compared.</param>
    /// <param name="value">The value the column will be compared against.</param>
    /// <returns></returns>
    public IConnective Equals(string column, object? value) => AppendCondition(column, Comparer.Equals, value);

    /// <summary>
    /// Appends the SQL existence comparer to the builder.
    /// </summary>
    /// <param name="subQuery">A Query object containing the sub-query to be used on comparison.</param>
    /// <returns></returns>
    public IConnective Exists(Query subQuery)
    {
        Sb.Append($"EXISTS ({subQuery.ToSql()})");

        return this;
    }

    /// <summary>
    /// Appends the SQL 'greater than' comparer to the builder.
    /// </summary>
    /// <param name="column">The column that is being compared.</param>
    /// <param name="value">The value the column will be compared against.</param>
    /// <returns></returns>
    public IConnective Greater(string column, object? value) => AppendCondition(column, Comparer.Greater, value);

    /// <summary>
    /// Appends the SQL 'greater or equal than' comparer to the builder.
    /// </summary>
    /// <param name="column">The column that is being compared.</param>
    /// <param name="value">The value the column will be compared against.</param>
    /// <returns></returns>
    public IConnective GreaterEqual(string column, object? value) =>
        AppendCondition(column, Comparer.GreaterEqual, value);

    /// <summary>
    /// Appends the SQL 'IN' operator to the builder.
    /// </summary>
    /// <param name="column">The column that is being compared.</param>
    /// <param name="values">The object array containing data to be compared against.</param>
    /// <returns></returns>
    public IConnective In(string column, params object?[] values) => AppendCondition(column, Comparer.In, values);

    /// <summary>
    /// Appends the SQL 'NOT IN' operator to the builder.
    /// </summary>
    /// <param name="column">The column that is being compared.</param>
    /// <param name="subQuery">The sub-query builder containing data to be compared against.</param>
    /// <returns></returns>
    public IConnective In(string column, Query subQuery)
    {
        Sb.Append($"({column} IN ({subQuery.ToSql()}))");
        return this;
    }

    /// <summary>
    /// Appends the SQL 'IS NULL' comparer to the builder.
    /// </summary>
    /// <param name="column">The column that is being compared.</param>
    /// <returns></returns>
    public IConnective IsNull(string column) => AppendCondition(column, Comparer.Is, null);

    /// <summary>
    /// Appends the SQL 'IS NOT NULL' comparer to the builder.
    /// </summary>
    /// <param name="column">The column that is being compared.</param>
    /// <returns></returns>
    public IConnective IsNotNull(string column) => AppendCondition(column, Comparer.IsNot, null);


    /// <summary>
    /// Appends the SQL 'less than' comparer to the builder.
    /// </summary>
    /// <param name="column">The column that is being compared.</param>
    /// <param name="value">The value the column will be compared against.</param>
    /// <returns></returns>
    public IConnective Less(string column, object? value) => AppendCondition(column, Comparer.Less, value);

    /// <summary>
    /// Appends the SQL 'less or equal than' comparer to the builder.
    /// </summary>
    /// <param name="column">The column that is being compared.</param>
    /// <param name="value">The value the column will be compared against.</param>
    /// <returns></returns>
    public IConnective LessEqual(string column, object? value) => AppendCondition(column, Comparer.LessEqual, value);

    /// <summary>
    /// Appends a nested condition to the builder.
    /// </summary>
    /// <param name="condition">An action representing all inner conditions.</param>
    /// <returns></returns>
    public IConnective Nested(Action<Condition> condition)
    {
        Sb.Append('(');
        condition(this);
        Sb.Append(')');

        return this;
    }

    /// <summary>
    /// Appends the SQL 'not between' comparer to the builder.
    /// </summary>
    /// <param name="column">The column that is being compared.</param>
    /// <param name="minimum">The minimum value of the interval.</param>
    /// <param name="maximum">The maximum value of the interval.</param>
    /// <returns></returns>
    public IConnective NotBetween(string column, object? minimum, object? maximum) =>
        AppendBetween(column, Comparer.NotBetween, minimum, maximum);

    /// <summary>
    /// Appends the SQL 'NOT IN' operator to the builder.
    /// </summary>
    /// <param name="column">The column that is being compared.</param>
    /// <param name="values">The object array containing data to be compared against.</param>
    /// <returns></returns>
    public IConnective NotIn(string column, params object?[] values) => AppendCondition(column, Comparer.NotIn, values);

    /// <summary>
    /// Appends the SQL 'NOT IN' operator to the builder.
    /// </summary>
    /// <param name="column">The column that is being compared.</param>
    /// <param name="subQuery">The sub-query containing data to be compared against.</param>
    /// <returns></returns>
    public IConnective NotIn(string column, Query subQuery)
    {
        Sb.Append($"({column} NOT IN ({subQuery.ToSql()}))");

        return this;
    }

    /// <summary>
    /// Appends the SQL 'ALL' operator to the builder.
    /// </summary>
    /// <param name="column">The column that is being compared.</param>
    /// <param name="comparer">A Comparer Enum value used to match data in the 'ALL' operator</param>
    /// <param name="subQuery">The sub-query containing data to be compared against.</param>
    /// <returns></returns>
    public IConnective All(string column, Comparer comparer, Query subQuery)
    {
        Sb.Append($"({column} {comparer.ToSql()} ALL ({subQuery.ToSql()}))");

        return this;
    }

    /// <summary>
    /// Appends the SQL 'ANY' operator to the builder.
    /// </summary>
    /// <param name="column">The column that is being compared.</param>
    /// <param name="comparer">A Comparer Enum value used to match data in the 'ANY' operator</param>
    /// <param name="subQuery">The sub-query containing data to be compared against.</param>
    /// <returns></returns>
    public IConnective Any(string column, Comparer comparer, Query subQuery)
    {
        Sb.Append($"({column} {comparer.ToSql()} ANY ({subQuery.ToSql()}))");

        return this;
    }

    /// <summary>
    /// Appends the SQL 'BETWEEN' comparer to the builder.
    /// </summary>
    /// <param name="column">The column that is being compared.</param>
    /// <param name="minimum">The minimum value of the interval.</param>
    /// <param name="maximum">The maximum value of the interval.</param>
    /// <returns></returns>
    public IConnective Between(string column, object? minimum, object? maximum) =>
        AppendBetween(column, Comparer.Between, minimum, maximum);

    /// <summary>
    /// Appends the SQL 'NOT BETWEEN' comparer to the builder.
    /// </summary>
    /// <param name="column">The column that is being compared.</param>
    /// <param name="value">The value the column will be compared against.</param>
    /// <returns></returns>
    public IConnective NotLike(string column, string value) => AppendCondition(column, Comparer.NotLike, value);

    /// <summary>
    /// Appends the SQL 'LIKE' comparer to the builder.
    /// </summary>
    /// <param name="column">The column that is being compared.</param>
    /// <param name="value">The value the column will be compared against.</param>
    /// <returns></returns>
    public IConnective Like(string column, string value) => AppendCondition(column, Comparer.Like, value);

    private IConnective AppendBetween(string column, Comparer comparer, object? minimum, object? maximum)
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