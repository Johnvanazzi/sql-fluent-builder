using Lib.QueryBuilder.Clauses;
using Lib.QueryBuilder.Operators;
using Lib.QueryBuilder.Utils;
using Lib.QueryBuilder.Validations;

namespace Lib.QueryBuilder;

public partial class Query
{
    public IValues Set(Dictionary<string, object?> columnsValues)
    {
        if (columnsValues.Count < 1)
            throw new ArgumentException("No values or columns were provided");

        _sb.Append(" SET ")
            .AppendJoin(", ", columnsValues.Select(pair => $"{pair.Key}={pair.Value.ToSql()}"));

        return this;
    }

    public IHaving GroupBy(string[] columns)
    {
        ArrayValidations.ItsNotEmpty(columns, nameof(columns));

        _sb.Append(" GROUP BY ").AppendJoin(", ", columns);

        return this;
    }

    public IQuery OrderBy(string[] columns)
    {
        ArrayValidations.ItsNotEmpty(columns, nameof(columns));

        _sb.Append(" ORDER BY ").AppendJoin(", ", columns);

        return this;
    }

    public IValues Values(object?[] values)
    {
        ArrayValidations.ItsNotEmpty(values, nameof(values));

        _sb.Append(" VALUES (").AppendJoin(", ", values.Select(Converter.ToSql)).Append(')');

        return this;
    }

    public IValues Values(object?[][] rows)
    {
        ArrayValidations.ItsNotEmpty(rows, nameof(rows));

        _sb.Append(" VALUES ");

        foreach (object?[] row in rows)
        {
            _sb.Append('(').AppendJoin(", ", row.Select(Converter.ToSql)).Append("), ");
        }

        _sb.Remove(_sb.Length - 2, 2);

        return this;
    }

    public IGroupBy Where(IConnective condition) => AppendClause("WHERE", condition);
    public IOrderBy Having(IConnective condition) => AppendClause("HAVING", condition);
    public IJoin From(string schema, string table) => AppendClause("FROM", schema, table);
    public IJoin From(string table) => AppendClause("FROM", table);
    public IOn LeftJoin(string schema, string table) => AppendClause("LEFT JOIN", schema, table);
    public IOn LeftJoin(string table) => AppendClause("LEFT JOIN", table);
    public IOn RightJoin(string schema, string table) => AppendClause("RIGHT JOIN", schema, table);
    public IOn RightJoin(string table) => AppendClause("RIGHT JOIN", table);
    public IOn CrossJoin(string schema, string table) => AppendClause("CROSS JOIN", schema, table);
    public IOn CrossJoin(string table) => AppendClause("CROSS JOIN", table);
    public IOn OuterJoin(string schema, string table) => AppendClause("OUTER JOIN", schema, table);
    public IOn OuterJoin(string table) => AppendClause("OUTER JOIN", table);
    public IOn InnerJoin(string schema, string table) => AppendClause("INNER JOIN", schema, table);
    public IOn InnerJoin(string table) => AppendClause("INNER JOIN", table);

    public IJoin On(string leftKey, string rightKey)
    {
        _sb.Append($" ON {leftKey} = {rightKey}");

        return this;
    }

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

    private Query AppendClause(string clause, string schema, string table)
    {
        _sb.Append($" {clause} [{schema}].[{table}]");

        return this;
    }

    private Query AppendClause(string clause, IConnective condition)
    {
        _sb.Append($" {clause} ").Append(condition.Sb);

        return this;
    }
}