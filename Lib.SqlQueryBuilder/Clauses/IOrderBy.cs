namespace Lib.QueryBuilder.Clauses;

public interface IOrderBy : IUnion
{
    public IUnion OrderBy(params string[] columns);
}