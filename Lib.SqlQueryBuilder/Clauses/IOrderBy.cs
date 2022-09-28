namespace Lib.QueryBuilder.Clauses;

public interface IOrderBy : IUnion, IQuery
{
    public IQuery OrderBy(string[] columns);
}