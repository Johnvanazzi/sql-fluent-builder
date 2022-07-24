using Lib.QueryBuilder.Clauses;

namespace Lib.QueryBuilder.Verbs;

public class SqlVerbs : SqlClauses, ISelect, IUpdate, IInsert, IDelete
{
    public IFrom Select()
    {
        throw new NotImplementedException();
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