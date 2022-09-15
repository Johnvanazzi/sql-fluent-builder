using System;
using System.Collections.Generic;
using Lib.QueryBuilder;
using Lib.QueryBuilder.Operators;
using Lib.QueryBuilder.Utils;
using NUnit.Framework;

namespace Tests;

public class QueryTest
{
    private string[] _columns;
    private string _table;
    private string _schema;
    private Query _query;
    
    [OneTimeSetUp]
    public void Setup()
    {
        _columns = new[] {"col1", "col2", "col3"};
        _table = "table1";
        _schema = "schema1";
        _query = new Query();
    }

    [TearDown]
    public void ClearQuery() => _query.Clear();
    
    [Test]
    public void TestClear()
    {
        _query.Select().From(_table);
        _query.Clear();

        Assert.NotNull(_query);
        Assert.AreEqual(";",_query.ToSql());
    }

    [Test]
    public void TestSelect_NoColumnSpecified()
    {
        string raw = _query.Select().ToSql();
        Assert.AreEqual("SELECT *;", raw);
    }
    
    [Test]
    public void TestSelect_WithColumnSpecified()
    {
        string raw = _query.Select(_columns).ToSql();
        Assert.AreEqual($"SELECT {_columns[0]}, {_columns[1]}, {_columns[2]};", raw);
    }
    
    [Test]
    public void TestSelect_WithEmptyArray()
    {
        Assert.Catch<ArgumentException>(() => _query.Select(Array.Empty<string>()));
    }

    [Test]
    public void TestFrom_WithTable()
    {
        string raw = _query.From(_table).ToSql();
        Assert.AreEqual($" FROM [{_table}];", raw);
    }

    [Test]
    public void TestFrom_WithSchemaAndTable()
    {
        string raw = _query.Select().From(_schema, _table).ToSql();
        Assert.AreEqual($"SELECT * FROM [{_schema}].[{_table}];", raw);
    }

    [Test]
    public void TestGroupBy_WithColumns()
    {
        string raw = _query.GroupBy(_columns).ToSql();
        Assert.AreEqual($" GROUP BY {_columns[0]}, {_columns[1]}, {_columns[2]};", raw);
    }

    [Test]
    public void TestGroupBy_WithEmptyArray()
    {
        Assert.Catch<ArgumentException>(() =>_query.GroupBy(Array.Empty<string>()));
    }

    [Test]
    public void TestOrderBy_WithColumns()
    {
        string raw = _query.OrderBy(_columns).ToSql();
        Assert.AreEqual($" ORDER BY {_columns[0]}, {_columns[1]}, {_columns[2]};", raw);
    }
    
    [Test]
    public void TestOrderBy_WithEmptyArray()
    {
        Assert.Catch<ArgumentException>(() => _query.OrderBy(Array.Empty<string>()));
    }

    [Test]
    public void TestSet_WithEmptyDictionary()
    {
        var values = new Dictionary<string, object?>();
        Assert.Catch<ArgumentException>(() => _query.Set(values));
    }
    
    [Test]
    public void TestSet_WithCustomDictionary()
    {
        var values = new Dictionary<string, object?>
        {
            { _columns[0], 0 },
            { _columns[1], "Test" },
            { _columns[2], null }
        };
        string raw = _query.Set(values).ToSql();
        Assert.AreEqual($" SET {_columns[0]}=0, {_columns[1]}='Test', {_columns[2]}=NULL;", raw);
    }

    [Test]
    public void TestValues_WithEmptyArray()
    {
        Assert.Catch<ArgumentException>(() => _query.Values(Array.Empty<object?>()));
        Assert.Catch<ArgumentException>(() => _query.Values(Array.Empty<object?[]>()));
    }
    
    [Test]
    public void TestValues_WithSimpleArray()
    {
        object?[] values = { 0, "Test", null };
        string raw = _query.Values(values).ToSql();
        Assert.AreEqual(" VALUES (0, 'Test', NULL);", raw);
    }
    
    [Test]
    public void TestValues_WithComposedArray()
    {
        object?[][] values2 =
        {
            new object?[]{ 0, "Test1", null },
            new object?[]{ 1, "Test2", 0.123 }
        }; 
        string raw = _query.Values(values2).ToSql();
        Assert.AreEqual(" VALUES (0, 'Test1', NULL), (1, 'Test2', 0.123);", raw);
    }

    [Test]
    public void TestInsert_WithSchemaAndTable()
    {
        string raw = _query.Insert(_schema, _table).ToSql();
        Assert.AreEqual($"INSERT INTO [{_schema}].[{_table}];", raw);
    }
    
    [Test]
    public void TestInsert_WithTable()
    {
        string raw = _query.Insert(_table).ToSql();
        Assert.AreEqual($"INSERT INTO [{_table}];", raw);
    }
    
    [Test]
    public void TestInsert_WithSchemaAndTableAndColumns()
    {
        string raw = _query.Insert(_schema, _table, _columns).ToSql();
        Assert.AreEqual($"INSERT INTO [{_schema}].[{_table}] ({_columns[0]}, {_columns[1]}, {_columns[2]});", raw);
    }
    
    [Test]
    public void TestInsert_WithTableAndColumns()
    {
        string raw = _query.Insert(_table, _columns).ToSql();
        Assert.AreEqual($"INSERT INTO [{_table}] ({_columns[0]}, {_columns[1]}, {_columns[2]});", raw);
    }
    
    [Test]
    public void TestInsert_WithEmptyColumnArray()
    {
        Assert.Catch<ArgumentException>(() => _query.Insert(_schema, _table, Array.Empty<string>()));
        Assert.Catch<ArgumentException>(() => _query.Insert(_table, Array.Empty<string>()));
    }

    [Test]
    public void TestDelete_WithSchemaAndTable()
    {
        string raw = _query.Delete(_schema, _table).ToSql();
        Assert.AreEqual($"DELETE FROM [{_schema}].[{_table}];", raw);
    }
    
    [Test]
    public void TestDelete_WithTable()
    {
        string raw = _query.Delete(_table).ToSql();
        Assert.AreEqual($"DELETE FROM [{_table}];", raw);
    }

    [Test]
    public void TestUpdate_WithSchemaAndTable()
    {
        string raw = _query.Update(_schema, _table).ToSql();
        Assert.AreEqual($"UPDATE [{_schema}].[{_table}];", raw);
    }
    
    [Test]
    public void TestUpdate_WithTable()
    {
        string raw = _query.Update(_table).ToSql();
        Assert.AreEqual($"UPDATE [{_table}];", raw);
    }

    [Test]
    public void TestWhere_WithSimpleCondition()
    {
        var cond = new Condition(_columns[0], Comparer.Differs, "test");
        string raw = _query.Where(cond).ToSql();
        Assert.AreEqual($" WHERE ({_columns[0]} != 'test');", raw);
    }
    
    [Test]
    public void TestWhere_WithConditionArray()
    {
        var cond = new Condition[]
        {
            new(_columns[0], Comparer.Equals, 1, Connective.And),
            new(_columns[1], Comparer.Is, false)
        };
        string raw = _query.Where(cond).ToSql();
        Assert.AreEqual($" WHERE (({_columns[0]} = 1) AND ({_columns[1]} IS FALSE));", raw);
    }
    
    [Test]
    public void TestWhere_WithNestedConditionArray()
    {
        var cond = new Condition[]
        {
            new(_columns[0], Comparer.GreaterEqualThan, new DateTime(2022, 01, 01), Connective.And),
            new(new Condition[]
            {
                new (_columns[1], Comparer.GreaterThan, 1.2, Connective.Or),
                new (_columns[2], Comparer.GreaterEqualThan, 2)
            }, Connective.Or),
            new (_columns[2], Comparer.LessEqualThan, 5)
        };
        string raw = _query.Where(cond).ToSql();
        Assert.AreEqual($" WHERE (({_columns[0]} >= '2022-01-01T00:00:00') AND (({_columns[1]} > 1.2) OR ({_columns[2]} >= 2)) OR ({_columns[2]} <= 5));", raw);
    }

    [Test]
    public void TestHaving_WithSimpleCondition()
    {
        var condition = new Condition(_columns[0], Comparer.Differs, "test");
        string raw = _query.Having(condition).ToSql();
        Assert.AreEqual($" HAVING ({_columns[0]} != 'test');", raw);
    }
    
    [Test]
    public void TestHaving_WithConditionArray()
    {
        var conditions = new Condition[]
        {
            new(_columns[0], Comparer.Equals, 1, Connective.And),
            new(_columns[1], Comparer.Is, false)
        };
        string raw = _query.Having(conditions).ToSql();
        Assert.AreEqual($" HAVING (({_columns[0]} = 1) AND ({_columns[1]} IS FALSE));", raw);
    }

    [Test]
    public void TestHaving_WithNestedConditionArray()
    {
        var cond2 = new Condition[]
        {
            new(_columns[0], Comparer.GreaterEqualThan, new DateTime(2022, 01, 01), Connective.And),
            new(new Condition[]
            {
                new (_columns[1], Comparer.GreaterThan, 1.2, Connective.Or),
                new (_columns[2], Comparer.GreaterEqualThan, 2)
            }, Connective.Or),
            new (_columns[2], Comparer.LessEqualThan, 5)
        };
        string raw = _query.Having(cond2).ToSql();
        Assert.AreEqual($" HAVING (({_columns[0]} >= '2022-01-01T00:00:00') AND (({_columns[1]} > 1.2) OR ({_columns[2]} >= 2)) OR ({_columns[2]} <= 5));", raw);
    }

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