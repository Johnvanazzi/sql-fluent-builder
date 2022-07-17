namespace Lib.QueryBuilder.Clauses;

public interface ISet : IWhere
{
    public ISet Set();
    public ISet Set(string[] columns);
}