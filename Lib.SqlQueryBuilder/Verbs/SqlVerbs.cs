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
        Sb.Append("SELECT ");

        foreach (string col in columns)
        {
            Sb.Append($"{col}, ");
        }

        Sb.Remove(Sb.Length - 3, 2);

        return this;
    }

    public ISet Update()
    {
        throw new NotImplementedException();
    }

    public IValues Insert()
    {
        throw new NotImplementedException();
    }

    public IWhere Delete()
    {
        throw new NotImplementedException();
    }
}