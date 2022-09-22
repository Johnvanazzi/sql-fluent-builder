using Lib.QueryBuilder.Operators;
using Lib.QueryBuilder.Utils;

namespace Lib.QueryBuilder.Clauses;

public interface IOn : IWhere
{
    public IJoin On(string leftKey, string rightKey);
    public IJoin On(string leftKey, string rightKey, Connective connective, IConnective condition);
}