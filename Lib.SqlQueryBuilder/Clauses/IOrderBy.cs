namespace Lib.QueryBuilder.Clauses;

public interface IOrderBy : IQuery
{
    public IOrderBy OrderBy();
}