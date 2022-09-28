namespace Lib.QueryBuilder.Clauses;

public interface ISelect : IQuery
{
    public IFrom Select();
    public IFrom Select(params string[] columns);
}