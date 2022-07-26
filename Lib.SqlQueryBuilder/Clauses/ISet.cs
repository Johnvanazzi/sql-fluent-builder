namespace Lib.QueryBuilder.Clauses;

public interface ISet : IQuery
{
    public IValues Set(string[] columns);
}