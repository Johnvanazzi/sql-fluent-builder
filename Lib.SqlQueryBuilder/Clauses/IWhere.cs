using Lib.QueryBuilder.Operators;
using Lib.QueryBuilder.Utils;

namespace Lib.QueryBuilder.Clauses;

public interface IWhere : IQuery
{
    public IGroupBy Where(Condition condition);
    public IGroupBy Where(Condition[] conditions);
    public IGroupBy Where(string column, Comparer comparer, object? value);
    public IGroupBy WhereExists(Query subQuery);
    public IGroupBy WhereAll(string column, Comparer comparer, Query subQuery);
    public IGroupBy WhereAny(string column, Comparer comparer, Query subQuery);
}