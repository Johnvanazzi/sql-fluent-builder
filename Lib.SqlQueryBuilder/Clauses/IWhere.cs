using Lib.QueryBuilder.Operators;

namespace Lib.QueryBuilder.Clauses;

public interface IWhere : IQuery
{
    public IGroupBy Where(IConnective condition);
}