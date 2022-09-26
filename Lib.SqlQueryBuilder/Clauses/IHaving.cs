using Lib.QueryBuilder.Operators;

namespace Lib.QueryBuilder.Clauses;

public interface IHaving : IOrderBy
{
    public IOrderBy Having(IConnective condition);
}