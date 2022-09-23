using System.Text;
using Lib.QueryBuilder.Clauses;

namespace Lib.QueryBuilder;

public partial class Query : IFrom, ISet, IValues, IHaving, IJoin, IOn
{
    private readonly StringBuilder _sb;

    public Query()
    {
        _sb = new StringBuilder();
    }

    public string ToSql() => _sb.ToString();
    public void Clear() => _sb.Clear();
}