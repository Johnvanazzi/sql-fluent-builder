using Lib.QueryBuilder.Clauses;
using Lib.QueryBuilder.Verbs;

namespace Lib.QueryBuilder;

public class Query : SqlVerbs
{
    private string _sql = "";
}

public class Test
{
    public void TestMethod()
    {
        new Query().Select().From("").Where();
        new Query().Update().Set(new string[] {}) ;
    }
}
