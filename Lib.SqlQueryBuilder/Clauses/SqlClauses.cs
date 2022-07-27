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
        Sb.Append(" SET ");
        
        foreach (KeyValuePair<string, object?> pair in columnsValues)
        {
            Sb.Append($"{pair.Key}={Converter.ObjectToSql(pair.Value)}, ");
        }
        
        Sb.Remove(Sb.Length - 2, 2);
        
        return this;
    }

    public IGroupBy GroupBy()
    {
        throw new NotImplementedException();
    }

    public IOrderBy OrderBy()
    {
        throw new NotImplementedException();
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

    public string ToSql() => Sb.Append(';').ToString();
}