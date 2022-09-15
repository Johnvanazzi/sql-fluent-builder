using System.Text;
using Lib.QueryBuilder.Operators;
using Lib.QueryBuilder.Utils;

namespace Lib.QueryBuilder.Clauses;

public class SqlClauses : IFrom, ISet, IPostWhere, IValues, IHaving, IJoin, IOn
{
    protected readonly StringBuilder Sb = new();
    
    public IJoin From(string schema, string table)
    {
        Sb.Append($" FROM [{schema}].[{table}]");

        return this;
    }

    public IJoin From(string table)
    {
        Sb.Append($" FROM [{table}]");

        return this;
    }

    public IValues Set(Dictionary<string, object?> columnsValues)
    {
        if (columnsValues.Count < 1)
            throw new ArgumentException("No values or columns were provided");

        Sb.Append(" SET ")
          .AppendJoin(", ", columnsValues.Select(pair => $"{pair.Key}={pair.Value.ToSql()}"));

        return this;
    }

    public IHaving GroupBy(string[] columns)
    {
        if (columns.Length < 1)
            throw new ArgumentException("Array of values is empty");

        Sb.Append(" GROUP BY ").AppendJoin(", ", columns);

        return this;
    }

    public IQuery OrderBy(string[] columns)
    {
        if (columns.Length < 1)
            throw new ArgumentException("Array of columns is empty");

        Sb.Append(" ORDER BY ").AppendJoin(", ", columns);

        return this;
    }

    public IPostWhere Where(Condition condition)
    {
        Sb.Append($" WHERE ({condition.Column} {condition.Comparer!.Value.ToSql()} {condition.Value.ToSql()})");

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

        Sb.Append(" VALUES (").AppendJoin(", ", values.Select(Converter.ToSql)).Append(')');

        return this;
    }

    public IValues Values(object?[][] rows)
    {
        if (rows.Length < 1)
            throw new ArgumentException("Array of rows is empty");

        Sb.Append(" VALUES ");

        foreach (object?[] row in rows)
        {
            Sb.Append('(').AppendJoin(", ", row.Select(Converter.ToSql)).Append("), ");
        }

        Sb.Remove(Sb.Length - 2, 2);
        
        return this;
    }
    
    public string ToSql() => Sb.ToString();

    public IOrderBy Having(Condition[] conditions)
    {
        Sb.Append(" HAVING (");        
        NestedConditions(conditions);
        Sb.Append(')');

        return this;
    }

    public IOrderBy Having(Condition condition)
    {
        Sb.Append($" HAVING ({condition.Column} {condition.Comparer!.Value.ToSql()} {condition.Value.ToSql()})");

        return this;
    }

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
                Sb.Append($"({cond.Column} {cond.Comparer!.Value.ToSql()} {cond.Value.ToSql()})");

            if (cond.Connective != null)
                Sb.Append($" {cond.Connective.Value.ToSql()} ");
        }
    }
    
    public IOn LeftJoin(string schema,string table) {
        Sb.Append($" LEFT JOIN [{schema}].[{table}]");
        
        return this;
    }
    
    public IOn LeftJoin(string table)
    {
        Sb.Append($" LEFT JOIN [{table}]");

        return this;
    }
    
    public IOn RightJoin(string schema,string table) {
        Sb.Append($" RIGHT JOIN [{schema}].[{table}]");
        
        return this;
    }
    
    public IOn RightJoin(string table)
    {
        Sb.Append($" RIGHT JOIN [{table}]");

        return this;
    }
    
    public IOn CrossJoin(string schema,string table) {
        Sb.Append($" CROSS JOIN [{schema}].[{table}]");
        
        return this;
    }
    
    public IOn CrossJoin(string table)
    {
        Sb.Append($" CROSS JOIN [{table}]");

        return this;
    }
    
    public IOn OuterJoin(string schema,string table) {
        Sb.Append($" OUTER JOIN [{schema}].[{table}]");
        
        return this;
    }
    
    public IOn OuterJoin(string table)
    {
        Sb.Append($" OUTER JOIN [{table}]");

        return this;
    }
    
    public IOn InnerJoin(string schema,string table) {
        Sb.Append($" INNER JOIN [{schema}].[{table}]");
        
        return this;
    }
    
    public IOn InnerJoin(string table)
    {
        Sb.Append($" INNER JOIN [{table}]");

        return this;
    }

    public IJoin On(string leftKey, string rightKey)
    {
        Sb.Append($" ON {leftKey} = {rightKey}");

        return this;
    }

    public IJoin On(string leftKey, string rightKey, Connective connective, Condition[] additionalConditions)
    {
        Sb.Append($" ON {leftKey} = {rightKey} {connective.ToSql()} ");
        NestedConditions(additionalConditions);
        
        return this;
    }
}