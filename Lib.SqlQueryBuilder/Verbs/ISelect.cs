using Lib.QueryBuilder.Clauses;

namespace Lib.QueryBuilder.Verbs;

public interface ISelect : IFrom
{
    public ISelect Select();
}