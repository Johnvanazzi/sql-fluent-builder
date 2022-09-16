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
        var cond = new Condition(_columns[0], Comparer.Differs, "test");
        string raw = _query.Where(cond).ToSql();
        Assert.AreEqual($" WHERE ({_columns[0]} != 'test')", raw);
    }
    
    [Test]
    public void WithConditionArray()
    {
        var cond = new Condition[]
        {
            new(_columns[0], Comparer.Equals, 1, Connective.And),
            new(_columns[1], Comparer.Is, false)
        };
        string raw = _query.Where(cond).ToSql();
        Assert.AreEqual($" WHERE (({_columns[0]} = 1) AND ({_columns[1]} IS FALSE))", raw);
    }
    
    [Test]
    public void WithNestedConditionArray()
    {
        var cond = new Condition[]
        {
            new(_columns[0], Comparer.GreaterEqual, new DateTime(2022, 01, 01), Connective.And),
            new(new Condition[]
            {
                new (_columns[1], Comparer.Greater, 1.2, Connective.Or),
                new (_columns[2], Comparer.GreaterEqual, 2)
            }, Connective.Or),
            new (_columns[2], Comparer.LessEqual, 5)
        };
        string raw = _query.Where(cond).ToSql();
        Assert.AreEqual($" WHERE (({_columns[0]} >= '2022-01-01T00:00:00') AND (({_columns[1]} > 1.2) OR ({_columns[2]} >= 2)) OR ({_columns[2]} <= 5))", raw);
    }
    
    [Test]
    public void WhereExists()
    {
        var subQuery = new Query();
        subQuery.Select().From(_table);
        string raw = _query.WhereExists(subQuery).ToSql();
        
        Assert.AreEqual($" WHERE EXISTS (SELECT * FROM [{_table}])", raw);
    }
    
    [Test]
    public void WhereAny()
    {
        var subQuery = new Query();
        subQuery.Select().From(_table);
        string raw = _query.WhereAny(_columns[0], Comparer.Differs, subQuery).ToSql();
        
        Assert.AreEqual($" WHERE {_columns[0]} != ANY (SELECT * FROM [{_table}])", raw);
    }
    
    [Test]
    public void WhereAll()
    {
        var subQuery = new Query();
        subQuery.Select().From(_table);
        string raw = _query.WhereAll(_columns[0], Comparer.Equals, subQuery).ToSql();
        
        Assert.AreEqual($" WHERE {_columns[0]} = ALL (SELECT * FROM [{_table}])", raw);
    }
}