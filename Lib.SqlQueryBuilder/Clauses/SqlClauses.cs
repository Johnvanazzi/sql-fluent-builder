namespace Lib.QueryBuilder.Clauses;

public class SqlClauses : IFrom, ISet, IGroupBy, IOrderBy, IValues, IWhere
{
    public IWhere From(string schema, string table)
    {
        throw new NotImplementedException();
    }

    public IWhere From(string table)
    {
        throw new NotImplementedException();
    }

    public IValues Set()
    {
        throw new NotImplementedException();
    }

    public IValues Set(string[] columns)
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

    public IPostWhere Where()
    {
        throw new NotImplementedException();
    }

    public IValues Values()
    {
        throw new NotImplementedException();
    }
}