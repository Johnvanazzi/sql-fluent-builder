using Lib.QueryBuilder.Operators;
using Lib.QueryBuilder.Utils;
using NUnit.Framework;

namespace Tests.Statements;

[TestFixture]
public class TestJoin : BaseConfig
{
    [Test]
    public void TestJoinOn()
    {
        string raw1 = _query.InnerJoin(_schema, _table).On("key1", "key2").ToSql();
        
        var conditions = new Condition[]
        {
            new(_columns[0], Comparer.Equals, 1, Connective.And),
            new(_columns[1], Comparer.Is, false)
        };
        
        _query.Clear();
        string raw2 = _query.InnerJoin(_schema, _table).On("key1", "key2", Connective.And, conditions).ToSql();

        Assert.AreEqual($" INNER JOIN [{_schema}].[{_table}] ON key1 = key2;", raw1);
        Assert.AreEqual($" INNER JOIN [{_schema}].[{_table}] ON key1 = key2 AND ({_columns[0]} = 1) AND ({_columns[1]} IS FALSE);", raw2);
    }
}