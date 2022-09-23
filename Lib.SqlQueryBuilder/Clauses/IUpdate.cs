using Lib.QueryBuilder.Clauses;

namespace Lib.QueryBuilder.Clauses;

public interface IUpdate
{
    public ISet Update(string table);
    public ISet Update(string schema, string table);
}