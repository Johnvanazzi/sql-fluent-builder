using Lib.QueryBuilder.Clauses;
using Lib.QueryBuilder.Operators;
using Lib.QueryBuilder.Utils;

namespace Lib.QueryBuilder;

public partial class Query
{
    public IJoin From(string schema, string table)
    {
        _sb.Append($" FROM [{schema}].[{table}]");

        return this;
    }

    public IJoin From(string table)
    {
        _sb.Append($" FROM [{table}]");

        return this;
    }

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
        if (columns.Length < 1)
            throw new ArgumentException("Array of values is empty");

        _sb.Append(" GROUP BY ").AppendJoin(", ", columns);

        return this;
    }

    public IQuery OrderBy(string[] columns)
    {
        if (columns.Length < 1)
            throw new ArgumentException("Array of columns is empty");

        _sb.Append(" ORDER BY ").AppendJoin(", ", columns);

        return this;
    }

    public IGroupBy Where(IConnective condition)
    {
        _sb.Append(" WHERE ").Append(condition.Sb);
        
        return this;
    }

    public IValues Values(object?[] values)
    {
        if (values.Length < 1)
            throw new ArgumentException("Array of values is empty");

        _sb.Append(" VALUES (").AppendJoin(", ", values.Select(Converter.ToSql)).Append(')');

        return this;
    }

    public IValues Values(object?[][] rows)
    {
        if (rows.Length < 1)
            throw new ArgumentException("Array of rows is empty");

        _sb.Append(" VALUES ");

        foreach (object?[] row in rows)
        {
            _sb.Append('(').AppendJoin(", ", row.Select(Converter.ToSql)).Append("), ");
        }

        _sb.Remove(_sb.Length - 2, 2);
        
        return this;
    }
    
    public string ToSql() => _sb.ToString();

    public IOrderBy Having(IConnective condition)
    {
        _sb.Append(" HAVING ").Append(condition.Sb);

        return this;
    }

    public IOn LeftJoin(string schema,string table) {
        _sb.Append($" LEFT JOIN [{schema}].[{table}]");
        
        return this;
    }
    
    public IOn LeftJoin(string table)
    {
        _sb.Append($" LEFT JOIN [{table}]");

        return this;
    }
    
    public IOn RightJoin(string schema,string table) {
        _sb.Append($" RIGHT JOIN [{schema}].[{table}]");
        
        return this;
    }
    
    public IOn RightJoin(string table)
    {
        _sb.Append($" RIGHT JOIN [{table}]");

        return this;
    }
    
    public IOn CrossJoin(string schema,string table) {
        _sb.Append($" CROSS JOIN [{schema}].[{table}]");
        
        return this;
    }
    
    public IOn CrossJoin(string table)
    {
        _sb.Append($" CROSS JOIN [{table}]");

        return this;
    }
    
    public IOn OuterJoin(string schema,string table) {
        _sb.Append($" OUTER JOIN [{schema}].[{table}]");
        
        return this;
    }
    
    public IOn OuterJoin(string table)
    {
        _sb.Append($" OUTER JOIN [{table}]");

        return this;
    }
    
    public IOn InnerJoin(string schema,string table) {
        _sb.Append($" INNER JOIN [{schema}].[{table}]");
        
        return this;
    }
    
    public IOn InnerJoin(string table)
    {
        _sb.Append($" INNER JOIN [{table}]");

        return this;
    }

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
}