using Lib.QueryBuilder.Clauses;

namespace Lib.QueryBuilder.Verbs;

public class SqlVerbs : SqlClauses, ISelect, IUpdate, IInsert, IDelete
{
    public IFrom Select()
    {
        Sb.Append("SELECT *");

        return this;
    }

    public IFrom Select(params string[] columns)
    {
        if (columns.Length < 1)
            throw new ArgumentException("Array of columns must not be empty");

        Sb.Append("SELECT ");

        foreach (string col in columns)
        {
            Sb.Append($"{col}, ");
        }

        Sb.Remove(Sb.Length - 3, 2);

        return this;
    }

    public ISet Update(string table)
    {
        Sb.Append($"UPDATE [{table}]");

        return this;
    }

    public ISet Update(string schema, string table)
    {
        Sb.Append($"UPDATE [{schema}].[{table}]");

        return this;
    }
    
    public IWhere Delete(string table)
    {
        Sb.Append($"DELETE FROM [{table}]");

        return this;
    }
    
    public IWhere Delete(string schema, string table)
    {
        Sb.Append($"DELETE FROM [{schema}].[{table}]");

        return this;
    }

    public IValues Insert(string schema, string table, string[] columns)
    {
        Sb.Append($"INSERT INTO [{schema}].[{table}] ( ");


        foreach (string col in columns)
        {
            Sb.Append($"{col}, ");
        }

        Sb.Remove(Sb.Length - 3, 2);
        Sb.Append(')');
        
        return this;
    }

    public IValues Insert(string schema, string table)
    {
        Sb.Append($"INSERT INTO [{schema}].[{table}]");

        return this;
    }

    public IValues Insert(string table, string[] columns)
    {
        Sb.Append($"INSERT INTO [{table}] (");
        
        foreach (string col in columns)
        {
            Sb.Append($"{col}, ");
        }

        Sb.Remove(Sb.Length - 3, 2);
        Sb.Append(')');

        return this;
    }

    public IValues Insert(string table)
    {
        Sb.Append($"INSERT INTO [{table}]");

        return this;
    }
}