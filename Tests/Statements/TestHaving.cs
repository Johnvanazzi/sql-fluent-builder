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
        var condition = new Condition().Differs(_columns[0], "test");
        string raw = _query.Having(condition).ToSql();

        Assert.AreEqual($" HAVING ({_columns[0]} != 'test')", raw);
    }

    [Test]
    public void WithConditionArray()
    {
        var conditions = new Condition()
            .Equals(_columns[0], 1).And()
            .Is(_columns[1], false);

        string raw = _query.Having(conditions).ToSql();

        Assert.AreEqual($" HAVING ({_columns[0]} = 1) AND ({_columns[1]} IS FALSE)", raw);
    }

    [Test]
    public void WithNestedConditionArray()
    {
        var conditions = new Condition()
            .GreaterEqual(_columns[0], new DateTime(2022, 01, 01)).And()
            .Nested(c => c
                .Greater(_columns[1], 1.2).Or()
                .GreaterEqual(_columns[2], 2)
            ).Or()
            .LessEqual(_columns[2], 5);
       
        string raw = _query.Having(conditions).ToSql();
        
        Assert.AreEqual($" HAVING ({_columns[0]} >= '2022-01-01T00:00:00') AND (({_columns[1]} > 1.2) OR ({_columns[2]} >= 2)) OR ({_columns[2]} <= 5)", raw);
    }
}