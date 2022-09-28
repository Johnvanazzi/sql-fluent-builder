using Lib.QueryBuilder.Clauses;
using Lib.QueryBuilder.Operators;
using Lib.QueryBuilder.Utils;
using Lib.QueryBuilder.Validations;

namespace Lib.QueryBuilder;

public partial class Query : IFrom, ISet, IValues, IHaving, IJoin, IOn, IUnion
{
    /// <summary>
    /// Appends as 'SET' clause to the query builder.
    /// </summary>
    /// <param name="columnsValues">A dictionary containing the columns names and the new values to be updated.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Throws exceptions when passed dictionary is empty.</exception>
    public IValues Set(Dictionary<string, object?> columnsValues)
    {
        if (columnsValues.Count < 1)
            throw new ArgumentException("No values or columns were provided");

        _sb.Append(" SET ")
            .AppendJoin(", ", columnsValues.Select(pair => $"{pair.Key}={pair.Value.ToSql()}"));

        return this;
    }

    /// <summary>
    /// Appends a 'GROUP BY' clause to the query builder.
    /// </summary>
    /// <param name="columns">String array containing the columns names.</param>
    /// <returns></returns>
    public IHaving GroupBy(string[] columns)
    {
        ArrayValidations.ItsNotEmpty(columns, nameof(columns));

        _sb.Append(" GROUP BY ").AppendJoin(", ", columns);

        return this;
    }

    /// <summary>
    /// Appends a 'ORDER BY' clause to the query builder.
    /// </summary>
    /// <param name="columns">String array containing the columns names.</param>
    /// <returns></returns>
    public IQuery OrderBy(string[] columns)
    {
        ArrayValidations.ItsNotEmpty(columns, nameof(columns));

        _sb.Append(" ORDER BY ").AppendJoin(", ", columns);

        return this;
    }

    /// <summary>
    /// Appends a 'VALUES' clause to the query builder.
    /// </summary>
    /// <param name="values">Object array containing the values to be inserted on table.</param>
    /// <returns></returns>
    public IValues Values(object?[] values)
    {
        ArrayValidations.ItsNotEmpty(values, nameof(values));

        _sb.Append(" VALUES (")
            .AppendJoin(", ", values.Select(Converter.ToSql))
            .Append(')');

        return this;
    }

    /// <summary>
    /// Appends a 'VALUES' clause to the query builder.
    /// </summary>
    /// <param name="rows">Multi-index object array containing the new rows to be inserted on table.</param>
    /// <returns></returns>
    public IValues Values(object?[][] rows)
    {
        ArrayValidations.ItsNotEmpty(rows, nameof(rows));

        _sb.Append(" VALUES ");

        foreach (object?[] row in rows)
        {
            _sb.Append('(')
                .AppendJoin(", ", row.Select(Converter.ToSql))
                .Append("), ");
        }

        _sb.Remove(_sb.Length - 2, 2);

        return this;
    }

    /// <summary>
    /// Appends the 'WHERE' clause to the query builder.
    /// </summary>
    /// <param name="condition">the Condition object containing all filters to be applied on the clause.</param>
    /// <returns></returns>
    public IGroupBy Where(IConnective condition) => AppendClause("WHERE", condition);

    /// <summary>
    /// Appends the 'HAVING' clause to the query builder.
    /// </summary>
    /// <param name="condition">the Condition object containing all filters to be applied on the clause.</param>
    /// <returns></returns>
    public IOrderBy Having(IConnective condition) => AppendClause("HAVING", condition);

    /// <summary>
    /// Appends the 'FROM' clause to the query builder.
    /// </summary>
    /// <param name="schema">The database schema where the table is.</param>
    /// <param name="table">The name of the table.</param>
    /// <returns></returns>
    public IJoin From(string schema, string table) => AppendClause("FROM", schema, table);

    /// <summary>
    /// Appends the 'FROM' clause to the query builder.
    /// </summary>
    /// <param name="table">The name of the table.</param>
    /// <returns></returns>
    public IJoin From(string table) => AppendClause("FROM", table);

    /// <summary>
    /// Appends the 'LEFT JOIN' clause to the query builder.
    /// </summary>
    /// <param name="schema">The database schema where the table is.</param>
    /// <param name="table">The name of the table.</param>
    /// <returns></returns>
    public IOn LeftJoin(string schema, string table) => AppendClause("LEFT JOIN", schema, table);

    /// <summary>
    /// Appends the 'LEFT JOIN' clause to the query builder.
    /// </summary>
    /// <param name="table">The name of the table.</param>
    /// <returns></returns>
    public IOn LeftJoin(string table) => AppendClause("LEFT JOIN", table);

    /// <summary>
    /// Appends the 'RIGHT JOIN' clause to the query builder.
    /// </summary>
    /// <param name="schema">The database schema where the table is.</param>
    /// <param name="table">The name of the table.</param>
    /// <returns></returns>
    public IOn RightJoin(string schema, string table) => AppendClause("RIGHT JOIN", schema, table);

    /// <summary>
    /// Appends the 'RIGHT JOIN' clause to the query builder.
    /// </summary>
    /// <param name="table">The name of the table.</param>
    /// <returns></returns>
    public IOn RightJoin(string table) => AppendClause("RIGHT JOIN", table);

    /// <summary>
    /// Appends the 'CROSS JOIN' clause to the query builder.
    /// </summary>
    /// <param name="schema">The database schema where the table is.</param>
    /// <param name="table">The name of the table.</param>
    /// <returns></returns>
    public IOn CrossJoin(string schema, string table) => AppendClause("CROSS JOIN", schema, table);

    /// <summary>
    /// Appends the 'CROSS JOIN' clause to the query builder.
    /// </summary>
    /// <param name="table">The name of the table.</param>
    /// <returns></returns>
    public IOn CrossJoin(string table) => AppendClause("CROSS JOIN", table);

    /// <summary>
    /// Appends the 'OUTER JOIN' clause to the query builder.
    /// </summary>
    /// <param name="schema">The database schema where the table is.</param>
    /// <param name="table">The name of the table.</param>
    /// <returns></returns>
    public IOn OuterJoin(string schema, string table) => AppendClause("OUTER JOIN", schema, table);

    /// <summary>
    /// Appends the 'OUTER JOIN' clause to the query builder.
    /// </summary>
    /// <param name="table">The name of the table.</param>
    /// <returns></returns>
    public IOn OuterJoin(string table) => AppendClause("OUTER JOIN", table);

    /// <summary>
    /// Appends the 'INNER JOIN' clause to the query builder.
    /// </summary>
    /// <param name="schema">The database schema where the table is.</param>
    /// <param name="table">The name of the table.</param>
    /// <returns></returns>
    public IOn InnerJoin(string schema, string table) => AppendClause("INNER JOIN", schema, table);

    /// <summary>
    /// Appends the 'INNER JOIN' clause to the query builder.
    /// </summary>
    /// <param name="table">The name of the table.</param>
    /// <returns></returns>
    public IOn InnerJoin(string table) => AppendClause("INNER JOIN", table);

    /// <summary>
    /// Appends the 'ON' clause to the query builder.
    /// </summary>
    /// <param name="leftKey">The key from the left table of the join clause.</param>
    /// <param name="rightKey">The key from the right table of the join clause.</param>
    /// <returns></returns>
    public IJoin On(string leftKey, string rightKey)
    {
        _sb.Append($" ON {leftKey} = {rightKey}");

        return this;
    }

    /// <summary>
    /// Appends the 'ON' clause to the query builder.
    /// </summary>
    /// <param name="leftKey">The key from the left table of the join clause.</param>
    /// <param name="rightKey">The key from the right table of the join clause.</param>
    /// <param name="connective">A logical connective to append further logical constraints.</param>
    /// <param name="condition">Condition object with the additional constraints to the join clause.</param>
    /// <returns></returns>
    public IJoin On(string leftKey, string rightKey, Connective connective, IConnective condition)
    {
        _sb.Append($" ON {leftKey} = {rightKey} {connective.ToSql()} ").Append(condition.Sb);

        return this;
    }

    private Query AppendClause(string clause, string table)
    {
        _sb.Append($" {clause} [{table}]");

        return this;
    }

    private Query AppendClause(string clause, string schema, string table) =>
        AppendClause(clause, $"{schema}].[{table}");

    private Query AppendClause(string clause, IConnective condition)
    {
        _sb.Append($" {clause} ").Append(condition.Sb);

        return this;
    }

    private Query AppendClause(string clause)
    {
        _sb.Append(clause);
        
        return this;
    }

    public ISelect Union() => AppendClause(" UNION ");

    public ISelect UnionAll() => AppendClause(" UNION ALL ");
}