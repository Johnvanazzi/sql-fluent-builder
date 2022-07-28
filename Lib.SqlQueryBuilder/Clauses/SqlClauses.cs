using System.Text;

namespace Lib.QueryBuilder.Clauses;

public class SqlClauses : IFrom, ISet, IGroupBy, IOrderBy, IValues, IWhere
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
        
        Sb.Append(" SET ");
        
        foreach (KeyValuePair<string, object?> pair in columnsValues)
        {
            Sb.Append($"{pair.Key}={Converter.ObjectToSql(pair.Value)}, ");
        }
        
        Sb.Remove(Sb.Length - 2, 2);
        
        return this;
    }

    public IGroupBy GroupBy(string[] columns)
    {
        if (columns.Length < 1)
            throw new ArgumentException("Array of values is empty");
        
        Sb.Append(" GROUP BY ");

        foreach (string col in columns)
        {
            Sb.Append($"{col}, ");
        }

        Sb.Remove(Sb.Length - 2, 2);

        return this;
    }

    public IOrderBy OrderBy(string[] columns)
    {
        if (columns.Length < 1)
            throw new ArgumentException("Array of columns is empty");
        
        Sb.Append(" ORDER BY ");

        foreach (string col in columns)
        {
            Sb.Append($"{col}, ");
        }

        Sb.Remove(Sb.Length - 2, 2);

        return this;
    }

    public IPostWhere Where()
    {
        throw new NotImplementedException();
    }

    public IValues Values(object?[] values)
    {
        if (values.Length < 1)
            throw new ArgumentException("Array of values is empty");

        Sb.Append(" VALUES (");

        foreach (object? value in values)
        {
            Sb.Append($"{Converter.ObjectToSql(value)}, ");
        }

        Sb.Remove(Sb.Length - 2, 2).Append(')');

        return this;
    }

    public IValues Values(object?[][] rows)
    {
        if (rows.Length < 1)
            throw new ArgumentException("Array of rows is empty");

        Sb.Append(" VALUES ");

        foreach (object?[] row in rows)
        {
            Sb.Append("(");
            foreach (object? col in row)
            {
                Sb.Append($"{Converter.ObjectToSql(col)}, ");
            }
            
            Sb.Remove(Sb.Length - 2, 2).Append("), ");
        }

        Sb.Remove(Sb.Length - 2, 2);
        
        return this;
    }
    
    public string ToSql() => Sb.Append(';').ToString();
}