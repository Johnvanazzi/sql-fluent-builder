namespace Lib.QueryBuilder.Clauses;

public interface IGroupBy : IQuery
{
    public IGroupBy GroupBy(string[] columns);
}