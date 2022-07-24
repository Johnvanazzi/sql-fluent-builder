using System.Text;

namespace Lib.QueryBuilder.Clauses;

public class SqlClauses : IFrom, ISet, IGroupBy, IOrderBy, IValues, IWhere
{
    protected readonly StringBuilder Sb = new();
    
    public IWhere From(string schema, string table)
    {
        Sb.Append($" FROM {schema}.{table}");

        return this;
    }

    public IWhere From(string table)
    {
        Sb.Append($" FROM {table}");

        return this;
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