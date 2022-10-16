using System.Text;
using SqlFluentBuilder.Clauses;
using SqlFluentBuilder.Operators;
using SqlFluentBuilder.Utils;
using SqlFluentBuilder.Validations;

namespace SqlFluentBuilder;

public partial class Query : IFrom, ISet, IValues, IHaving, IJoin, IOn
{
    /// <summary>
    /// Appends as 'SET' clause to the query builder.
    /// </summary>
    /// <param name="columnsValues">A dictionary containing the columns names and the new values to be updated.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Throws exceptions when passed dictionary is empty.</exception>
    public IWhere Set(Dictionary<string, object?> columnsValues)
    {
        if (columnsValues.Count < 1)
            throw new ArgumentException("No values or columns were provided");

        _sb.Append(" SET ")
            .AppendJoin(", ", columnsValues.Select(pair => $"[{pair.Key}] = {pair.Value.ToSql()}"));

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

        _sb.Append(" GROUP BY ");
        AppendColumnArray(columns);

        return this;
    }

    /// <summary>
    /// Appends a 'ORDER BY' clause to the query builder.
    /// </summary>
    /// <param name="columns">String array containing the columns names.</param>
    /// <returns></returns>
    public IUnion OrderBy(string[] columns)
    {
        ArrayValidations.ItsNotEmpty(columns, nameof(columns));

        _sb.Append(" ORDER BY ");
        AppendColumnArray(columns);

        return this;
    }

    /// <summary>
    /// Appends a 'VALUES' clause to the query builder.
    /// </summary>
    /// <param name="values">Object array containing the values to be inserted on table.</param>
    /// <returns></returns>
    public IQuery Values(object?[] values)
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
    public IQuery Values(object?[][] rows)
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
    /// Appends the 'WHERE' clause to the query builder.
    /// </summary>
    /// <param name="condition">A lambda function that will apply the filter conditions.</param>
    /// <returns></returns>
    public IGroupBy Where(Func<Condition, IConnective> condition) => AppendClause("WHERE", condition);

    /// <summary>
    /// Appends the 'HAVING' clause to the query builder.
    /// </summary>
    /// <param name="condition">the Condition object containing all filters to be applied on the clause.</param>
    /// <returns></returns>
    public IOrderBy Having(IConnective condition) => AppendClause("HAVING", condition);

    /// <summary>
    /// Appends the 'HAVING' clause to the query builder.
    /// </summary>
    /// <param name="condition">A lambda function that will apply the filter conditions</param>
    /// <returns></returns>
    public IOrderBy Having(Func<Condition, IConnective> condition) => AppendClause("HAVING", condition);

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
    /// <param name="leftTable">The name of the table joining by the 'left'.</param>
    /// <param name="leftKey">The key from the left table of the join clause.</param>
    /// <param name="rightTable">The name of the table joining by the 'right'.</param>
    /// <param name="rightKey">The key from the right table of the join clause.</param>
    /// <returns></returns>
    public IJoin On(string leftTable, string leftKey, string rightTable, string rightKey)
    {
        _sb.Append($" ON [{leftTable}].[{leftKey}] = [{rightTable}].[{rightKey}]");

        return this;
    }

    /// <summary>
    /// Appends the 'ON' clause to the query builder.
    /// </summary>
    /// <param name="leftTable">The name of the table joining by the 'left'.</param>
    /// <param name="leftKey">The key from the left table of the join clause.</param>
    /// <param name="rightTable">The name of the table joining by the 'right'.</param>
    /// <param name="rightKey">The key from the right table of the join clause.</param>
    /// <param name="connective">A logical connective to append further logical constraints.</param>
    /// <param name="condition">Condition object with the additional constraints to the join clause.</param>
    /// <returns></returns>
    public IJoin On(string leftTable, string leftKey, string rightTable, string rightKey, Connective connective,
        IConnective condition)
    {
        _sb.Append($" ON [{leftTable}].[{leftKey}] = [{rightTable}].[{rightKey}] {connective.ToSql()} ")
            .Append(condition.Sb);

        return this;
    }

    /// <summary>
    /// Appends the 'UNION' clause to the query builder.
    /// </summary>
    /// <returns></returns>
    public ISelect Union() => AppendClause(" UNION ");

    /// <summary>
    /// Appends the 'UNION ALL' clause to the query builder.
    /// </summary>
    /// <returns></returns>
    public ISelect UnionAll() => AppendClause(" UNION ALL ");

    /// <summary>
    /// Appends the 'INTO' clause to the query builder. This is intended to be used after the 'SELECT' statement.
    /// </summary>
    /// <param name="newTable">The name of the table of destiny.</param>
    /// <returns></returns>
    public IFrom Into(string newTable) => AppendClause("INTO", newTable);

    /// <summary>
    /// Appends the 'INTO' clause to the query builder. This is intended to be used after the 'SELECT' statement.
    /// </summary>
    /// <param name="schema">The name of the schema.</param>
    /// <param name="newTable">The name of the table of destiny.</param>
    /// <returns></returns>
    public IFrom Into(string schema, string newTable) => AppendClause("INTO", schema, newTable);

    /// <summary>
    /// Appends the 'INTO' clause to the query builder. This is intended to be used after the 'SELECT' statement.
    /// </summary>
    /// <param name="newTable">The name of the table of destiny.</param>
    /// <param name="externalDb">The name of the external database.</param>
    /// <returns></returns>
    public IFrom IntoIn(string newTable, string externalDb) => AppendClause($" INTO [{newTable}] IN '{externalDb}'");

    /// <summary>
    /// Appends the 'INTO' clause to the query builder. This is intended to be used after the 'SELECT' statement.
    /// </summary>
    /// <param name="schema">The name of the schema.</param>
    /// <param name="newTable">The name of the table of destiny.</param>
    /// <param name="externalDb">The name of the external database.</param>
    /// <returns></returns>
    public IFrom IntoIn(string schema, string newTable, string externalDb) =>
        AppendClause($" INTO [{schema}].[{newTable}] IN '{externalDb}'");

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

    private Query AppendClause(string clause, Func<Condition, IConnective> function)
    {
        StringBuilder sb = function.Invoke(new Condition()).Sb;
        _sb.Append($" {clause} ").Append(sb);

        return this;
    }

    private Query AppendClause(string clause)
    {
        _sb.Append(clause);

        return this;
    }
}