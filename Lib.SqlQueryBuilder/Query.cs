using System.Text;
using Lib.QueryBuilder.Verbs;

namespace Lib.QueryBuilder;

public class Query : SqlVerbs
{
    public string ToSql() => Sb.ToString();
}