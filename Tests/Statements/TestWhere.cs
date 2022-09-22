using System;
using Lib.QueryBuilder;
using Lib.QueryBuilder.Operators;
using Lib.QueryBuilder.Utils;
using NUnit.Framework;

namespace Tests.Statements;

[TestFixture]
public class TestWhere : BaseConfig
{
    [Test]
    public void WithSimpleCondition()
    {
        var cond = new Condition().Differs(_columns[0], "test");
        
        string raw = _query.Where(cond).ToSql();
        
        Assert.AreEqual($" WHERE ({_columns[0]} != 'test')", raw);
    }

    [Test]
    public void WithConditionArray()
    {
        var cond = new Condition()
            .Equals(_columns[0], 1).And()
            .Is(_columns[1], false);

        string raw = _query.Where(cond).ToSql();
        
        Assert.AreEqual($" WHERE ({_columns[0]} = 1) AND ({_columns[1]} IS FALSE)", raw);
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

        string raw = _query.Where(conditions).ToSql();
        Assert.AreEqual(
            $" WHERE ({_columns[0]} >= '2022-01-01T00:00:00') AND (({_columns[1]} > 1.2) OR ({_columns[2]} >= 2)) OR ({_columns[2]} <= 5)",
            raw);
    }

    [Test]
    public void WhereExists()
    {
        var subQuery = new Query();
        subQuery.Select().From(_table);
        IConnective conditions = new Condition().Exists(subQuery);
        
        string raw = _query.Where(conditions).ToSql();

        Assert.AreEqual($" WHERE EXISTS (SELECT * FROM [{_table}])", raw);
    }

    [Test]
    public void WhereAny()
    {
        var subQuery = new Query();
        subQuery.Select().From(_table);
        IConnective conditions = new Condition().Any(_columns[0], Comparer.Differs, subQuery);
        
        string raw = _query.Where(conditions).ToSql();

        Assert.AreEqual($" WHERE {_columns[0]} != ANY (SELECT * FROM [{_table}])", raw);
    }

    [Test]
    public void WhereAll()
    {
        var subQuery = new Query();
        subQuery.Select().From(_table);
        IConnective conditions = new Condition().All(_columns[0], Comparer.Equals, subQuery);
        
        string raw = _query.Where(conditions).ToSql();

        Assert.AreEqual($" WHERE {_columns[0]} = ALL (SELECT * FROM [{_table}])", raw);
    }
}