using Lib.QueryBuilder.Clauses;

namespace Lib.QueryBuilder.Verbs;

public interface IDelete : IWhere
{
    public IDelete Delete();
}