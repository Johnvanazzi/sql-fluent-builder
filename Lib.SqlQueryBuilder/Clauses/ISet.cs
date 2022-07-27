namespace Lib.QueryBuilder.Clauses;

public interface ISet : IQuery
{
    public IValues Set(Dictionary<string, object?> columnsValues);
}