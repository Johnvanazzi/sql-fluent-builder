using System.Text;
using Lib.QueryBuilder.Verbs;

namespace Lib.QueryBuilder;

public class Query : SqlVerbs
{
    public string ToSql() => Sb.ToString();
}

public class Test
{
    public void TestMethod()
    {
        new Query().Select().From("").Where();
        new Query().Update().Set(new string[] {}) ;
    }
}
