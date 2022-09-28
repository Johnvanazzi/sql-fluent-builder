using Lib.QueryBuilder.Operators;

namespace Lib.QueryBuilder.Clauses;

public interface IWhere : IUnion, IQuery
{
    public IGroupBy Where(IConnective condition);
}