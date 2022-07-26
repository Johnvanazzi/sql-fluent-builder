namespace Lib.QueryBuilder.Clauses;

public interface ISet
{
    public IValues Set(string[] columns);
}