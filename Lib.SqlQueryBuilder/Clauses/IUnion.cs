namespace Lib.QueryBuilder.Clauses;

public interface IUnion : IQuery
{
    public ISelect Union();
    public ISelect UnionAll();
}