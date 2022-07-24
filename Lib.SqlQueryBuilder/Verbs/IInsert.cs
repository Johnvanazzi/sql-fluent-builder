using Lib.QueryBuilder.Clauses;

namespace Lib.QueryBuilder.Verbs;

public interface IInsert
{
    public IValues Insert();
}