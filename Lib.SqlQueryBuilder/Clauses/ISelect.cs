namespace Lib.QueryBuilder.Clauses;

public interface ISelect
{
    public IFrom Select();
    public IFrom Select(params string[] columns);
}