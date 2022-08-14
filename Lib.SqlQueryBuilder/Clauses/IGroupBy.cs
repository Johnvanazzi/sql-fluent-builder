namespace Lib.QueryBuilder.Clauses;

public interface IGroupBy : IQuery
{
    public IHaving GroupBy(string[] columns);
}