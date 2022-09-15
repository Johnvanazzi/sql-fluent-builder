using System;
using Lib.QueryBuilder.Operators;
using Lib.QueryBuilder.Utils;
using NUnit.Framework;

namespace Tests.Statements;

[TestFixture]
public class TestHaving : BaseConfig
{
    [Test]
    public void WithSimpleCondition()
    {
        var condition = new Condition(_columns[0], Comparer.Differs, "test");
        string raw = _query.Having(condition).ToSql();
        Assert.AreEqual($" HAVING ({_columns[0]} != 'test')", raw);
    }
    
    [Test]
    public void WithConditionArray()
    {
        var conditions = new Condition[]
        {
            new(_columns[0], Comparer.Equals, 1, Connective.And),
            new(_columns[1], Comparer.Is, false)
        };
        string raw = _query.Having(conditions).ToSql();
        Assert.AreEqual($" HAVING (({_columns[0]} = 1) AND ({_columns[1]} IS FALSE))", raw);
    }

    [Test]
    public void WithNestedConditionArray()
    {
        var conditions = new Condition[]
        {
            new(_columns[0], Comparer.GreaterEqual, new DateTime(2022, 01, 01), Connective.And),
            new(new Condition[]
            {
                new (_columns[1], Comparer.Greater, 1.2, Connective.Or),
                new (_columns[2], Comparer.GreaterEqual, 2)
            }, Connective.Or),
            new (_columns[2], Comparer.LessEqual, 5)
        };
        string raw = _query.Having(conditions).ToSql();
        Assert.AreEqual($" HAVING (({_columns[0]} >= '2022-01-01T00:00:00') AND (({_columns[1]} > 1.2) OR ({_columns[2]} >= 2)) OR ({_columns[2]} <= 5))", raw);
    }
}