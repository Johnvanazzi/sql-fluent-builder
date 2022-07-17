using Lib.QueryBuilder.Clauses;

namespace Lib.QueryBuilder.Verbs;

public interface IInsert : IValues
{
    public IInsert Insert();
}