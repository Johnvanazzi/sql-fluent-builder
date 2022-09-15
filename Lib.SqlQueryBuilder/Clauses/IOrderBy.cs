namespace Lib.QueryBuilder.Clauses;

public interface IOrderBy : IQuery
{
    public IQuery OrderBy(string[] columns);
}