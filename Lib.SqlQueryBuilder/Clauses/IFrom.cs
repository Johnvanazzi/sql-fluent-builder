namespace Lib.QueryBuilder.Clauses;

public interface IFrom : IInto, IQuery
{
    public IJoin From(string schema, string table);
    public IJoin From(string table);
}