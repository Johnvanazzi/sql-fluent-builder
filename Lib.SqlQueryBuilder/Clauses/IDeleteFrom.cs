namespace Lib.QueryBuilder.Clauses;

public interface IDeleteFrom
{
    public IWhere DeleteFrom(string table);
    public IWhere DeleteFrom(string schema, string table);
}