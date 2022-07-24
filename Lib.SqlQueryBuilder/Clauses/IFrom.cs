namespace Lib.QueryBuilder.Clauses;

public interface IFrom
{
    public IWhere From(string schema, string table);
    public IWhere From(string table);
}