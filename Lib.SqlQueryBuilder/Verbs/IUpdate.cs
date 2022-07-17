using Lib.QueryBuilder.Clauses;

namespace Lib.QueryBuilder.Verbs;

public interface IUpdate : ISet
{
    public IUpdate Update();
}