namespace Lib.QueryBuilder.Clauses;

public interface IHaving : IQuery
{
    public IOrderBy Having(Condition[] conditions);
    public IOrderBy Having(Condition condition);
}