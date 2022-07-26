using Lib.QueryBuilder.Verbs;

namespace Lib.QueryBuilder;

public class Query : SqlVerbs
{
    public void Clear() => Sb.Clear();
}