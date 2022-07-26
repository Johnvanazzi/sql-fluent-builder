namespace Lib.QueryBuilder.Clauses;

public interface IWhere : IQuery
{
    public IPostWhere Where();
}