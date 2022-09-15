using Lib.QueryBuilder.Operators;
using Lib.QueryBuilder.Utils;

namespace Lib.QueryBuilder.Clauses;

public interface IWhere : IQuery
{
    public IPostWhere Where(Condition condition);
    public IPostWhere Where(Condition[] conditions);
    public IPostWhere Where(string column, Comparer comparer, object? value);
}