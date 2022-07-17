namespace Lib.QueryBuilder.Clauses;

public interface IWhere : IGroupBy, IOrderBy
{
    public IWhere Where();
}