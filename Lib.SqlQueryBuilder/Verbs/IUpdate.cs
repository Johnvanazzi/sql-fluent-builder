using Lib.QueryBuilder.Clauses;

namespace Lib.QueryBuilder.Verbs;

public interface IUpdate
{
    public ISet Update(string table);
    public ISet Update(string schema, string table);
}