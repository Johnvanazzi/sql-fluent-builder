namespace Lib.QueryBuilder.Clauses;

public interface IFrom : IQuery
{
    public IJoin From(string schema, string table);
    public IJoin From(string table);
}