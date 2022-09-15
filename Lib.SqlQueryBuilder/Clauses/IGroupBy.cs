namespace Lib.QueryBuilder.Clauses;

public interface IGroupBy : IOrderBy
{
    public IHaving GroupBy(string[] columns);
}