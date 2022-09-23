using Lib.QueryBuilder.Clauses;

namespace Lib.QueryBuilder.Clauses;

public interface IDelete
{
    public IWhere Delete(string table);
    public IWhere Delete(string schema, string table);
}