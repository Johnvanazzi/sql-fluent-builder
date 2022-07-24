using Lib.QueryBuilder.Clauses;

namespace Lib.QueryBuilder.Verbs;

public interface IDelete
{
    public IWhere Delete(string table);
    public IWhere Delete(string schema, string table);
}