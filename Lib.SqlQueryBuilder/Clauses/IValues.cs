namespace Lib.QueryBuilder.Clauses;

public interface IValues : IQuery
{
    public IValues Values(object?[] values);
}