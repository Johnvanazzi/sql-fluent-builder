using Lib.QueryBuilder.Operators;
using Lib.QueryBuilder.Utils;

namespace Lib.QueryBuilder.Clauses;

public interface IWhere : IQuery
{
    public IGroupBy Where(Condition condition);
    public IGroupBy Where(Condition[] conditions);
    public IGroupBy Where(string column, Comparer comparer, object? value);
}