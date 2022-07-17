using Lib.QueryBuilder.Clauses;
using Lib.QueryBuilder.Verbs;

namespace Lib.QueryBuilder;

public class Query : ICommand
{
    private string _sql = "";

    public IFrom From(string schema, string table)
    {
        throw new NotImplementedException();
    }

    public IFrom From(string table)
    {
        throw new NotImplementedException();
    }

    public ISelect Select()
    {
        throw new NotImplementedException();
    }

    public IGroupBy GroupBy()
    {
        throw new NotImplementedException();
    }

    public IOrderBy OrderBy()
    {
        throw new NotImplementedException();
    }

    public IWhere Where()
    {
        throw new NotImplementedException();
    }

    public IDelete Delete()
    {
        throw new NotImplementedException();
    }

    public ISet Set()
    {
        throw new NotImplementedException();
    }

    public ISet Set(string[] columns)
    {
        throw new NotImplementedException();
    }

    public IUpdate Update()
    {
        throw new NotImplementedException();
    }

    public IValues Values()
    {
        throw new NotImplementedException();
    }

    public IInsert Insert()
    {
        throw new NotImplementedException();
    }
}

public class Test
{
    public void TestMethod()
    {
        new Query();
    }
}
