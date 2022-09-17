using System.Text;
using Lib.QueryBuilder.Clauses;
using Lib.QueryBuilder.Verbs;

namespace Lib.QueryBuilder;

public partial class Query : IFrom, ISet, IValues, IHaving, IJoin, IOn
{
    protected readonly StringBuilder Sb = new();
    public void Clear() => Sb.Clear();
}