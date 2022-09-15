using Lib.QueryBuilder.Operators;
using Lib.QueryBuilder.Utils;
using NUnit.Framework;

namespace Tests.Statements;

[TestFixture]
public class TestOn : BaseConfig
{
    [Test]
    public void WithConditions()
    {
        var conditions = new Condition[]
        {
            new(_columns[0], Comparer.Equals, 1, Connective.And),
            new(_columns[1], Comparer.Is, false)
        };
        string raw = _query.On("key1", "key2", Connective.And, conditions).ToSql();
        Assert.AreEqual($" ON key1 = key2 AND ({_columns[0]} = 1) AND ({_columns[1]} IS FALSE)", raw);
    }

    [Test]
    public void Simple()
    {
        string raw1 = _query.On("key1", "key2").ToSql();
        Assert.AreEqual(" ON key1 = key2", raw1);
    }
}