using System.Text;
using Lib.QueryBuilder.Operators;

namespace Lib.QueryBuilder.Clauses;

public class SqlClauses : IFrom, ISet, IPostWhere, IValues, IWhere
{
    protected readonly StringBuilder Sb = new();
    
    public IWhere From(string schema, string table)
    {
        Sb.Append($" FROM [{schema}].[{table}]");

        return this;
    }

    public IWhere From(string table)
    {
        Sb.Append($" FROM [{table}]");

        return this;
    }

    public IValues Set(Dictionary<string, object?> columnsValues)
    {
        if (columnsValues.Count < 1)
            throw new ArgumentException("No values or columns were provided");

        Sb.Append(" SET ")
          .AppendJoin(", ", columnsValues.Select(pair => $"{pair.Key}={Converter.ObjectToSql(pair.Value)}"));

        return this;
    }

    public IGroupBy GroupBy(string[] columns)
    {
        if (columns.Length < 1)
            throw new ArgumentException("Array of values is empty");

        Sb.Append(" GROUP BY ").AppendJoin(", ", columns);

        return this;
    }

    public IOrderBy OrderBy(string[] columns)
    {
        if (columns.Length < 1)
            throw new ArgumentException("Array of columns is empty");

        Sb.Append(" ORDER BY ").AppendJoin(", ", columns);

        return this;
    }

    public IPostWhere Where(Condition condition)
    {
        Sb.Append($" WHERE ({condition.Column} {Converter.ComparerToSql(condition.Comparer)} {Converter.ObjectToSql(condition.Value)})");

        return this;
    }

    public IPostWhere Where(Condition[] conditions)
    {
        Sb.Append(" WHERE (");        
        NestedConditions(conditions);
        Sb.Append(')');

        return this;
    }

    public IPostWhere Where(string column, Comparer comparer, object? value)
        => Where(new Condition(column, comparer, value));

    public IValues Values(object?[] values)
    {
        if (values.Length < 1)
            throw new ArgumentException("Array of values is empty");

        Sb.Append(" VALUES (").AppendJoin(", ", values.Select(Converter.ObjectToSql)).Append(')');

        return this;
    }

    public IValues Values(object?[][] rows)
    {
        if (rows.Length < 1)
            throw new ArgumentException("Array of rows is empty");

        Sb.Append(" VALUES ");

        foreach (object?[] row in rows)
        {
            Sb.Append('(').AppendJoin(", ", row.Select(Converter.ObjectToSql)).Append("), ");
        }

        Sb.Remove(Sb.Length - 2, 2);
        
        return this;
    }
    
    public string ToSql() => Sb.Append(';').ToString();
    
    private void NestedConditions(Condition[] conditions)
    {
        foreach (Condition cond in conditions)
        {
            if (cond.SubConditions != null)
            {
                Sb.Append('(');
                NestedConditions(cond.SubConditions);
                Sb.Append(')');
            }
            
            if (cond.Column != null)
                Sb.Append($"({cond.Column} {Converter.ComparerToSql(cond.Comparer)} {Converter.ObjectToSql(cond.Value)})");

            if (cond.Connective != null)
                Sb.Append($" {Converter.LogicalToSql(cond.Connective)} ");
        }
    }
}