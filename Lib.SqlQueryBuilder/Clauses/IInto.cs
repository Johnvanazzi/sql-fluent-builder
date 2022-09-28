namespace Lib.QueryBuilder.Clauses;

public interface IInto
{
    public IFrom Into(string newTable);
    public IFrom Into(string newTable, string externalDb);
}