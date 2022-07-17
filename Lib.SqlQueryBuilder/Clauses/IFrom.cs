namespace Lib.QueryBuilder.Clauses;

public interface IFrom : IWhere
{
    public IFrom From(string schema, string table);
    public IFrom From(string table);
}