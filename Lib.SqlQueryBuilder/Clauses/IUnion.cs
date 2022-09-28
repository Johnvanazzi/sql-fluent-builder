namespace Lib.QueryBuilder.Clauses;

public interface IUnion
{
    public ISelect Union();
    public ISelect UnionAll();
}