using System;
using System.Collections.Generic;
using Lib.QueryBuilder;
using Lib.QueryBuilder.Operators;
using NUnit.Framework;

namespace Tests;

public class QueryTest
{
    private string[] _columns;
    private string _table;
    private string _schema;
    private Query _query;
    
    [SetUp]
    public void Setup()
    {
        _columns = new[] {"col1", "col2", "col3"};
        _table = "table1";
        _schema = "schema1";
        _query = new Query();
    }

    [Test]
    public void TestClear()
    {
        Assert.NotNull(_query);

        _query.Select().From(_table);
        _query.Clear();
        
        Assert.AreEqual(";",_query.ToSql());
    }
    
    [Test]
    public void TestSelect()
    {
        string raw = _query.Select().ToSql();
        Assert.AreEqual($"SELECT *;", raw);
        
        _query.Clear();
        raw = _query.Select(_columns).ToSql();
        Assert.AreEqual($"SELECT {_columns[0]}, {_columns[1]}, {_columns[2]};", raw);
        
        _query.Clear();
        Assert.Catch<ArgumentException>(() => _query.Select(Array.Empty<string>()));
    }

    [Test]
    public void TestSelectFrom()
    {
        _query.Clear();
        string raw = _query.Select().From(_table).ToSql();
        Assert.AreEqual($"SELECT * FROM [{_table}];", raw);
        
        _query.Clear();
        raw = _query.Select().From(_schema, _table).ToSql();
        Assert.AreEqual($"SELECT * FROM [{_schema}].[{_table}];", raw);
        
        _query.Clear();
        raw = _query.Select(_columns).From(_table).ToSql();
        Assert.AreEqual($"SELECT {_columns[0]}, {_columns[1]}, {_columns[2]} FROM [{_table}];", raw);
        
        _query.Clear();
        raw = _query.Select(_columns).From(_schema, _table).ToSql();
        Assert.AreEqual($"SELECT {_columns[0]}, {_columns[1]}, {_columns[2]} FROM [{_schema}].[{_table}];", raw);
    }

    [Test]
    public void TestGroupBy()
    {
        _query.Clear();
        string raw = _query.GroupBy(_columns).ToSql();
        Assert.AreEqual($" GROUP BY {_columns[0]}, {_columns[1]}, {_columns[2]};", raw);
        
        _query.Clear();
        Assert.Catch<ArgumentException>(() =>_query.GroupBy(Array.Empty<string>()));
    }
    
    [Test]
    public void TestOrderBy()
    {
        _query.Clear();
        string raw = _query.OrderBy(_columns).ToSql();
        Assert.AreEqual($" ORDER BY {_columns[0]}, {_columns[1]}, {_columns[2]};", raw);
        
        _query.Clear();
        Assert.Catch<ArgumentException>(() => _query.OrderBy(Array.Empty<string>()));
    }
    
    [Test]
    public void TestSet()
    {
        _query.Clear();
        var values = new Dictionary<string, object?>();
        Assert.Catch<ArgumentException>(() => _query.Set(values));
        
        values.Add(_columns[0], 0);
        values.Add(_columns[1], "Test");
        values.Add(_columns[2], null);
        
        _query.Clear();
        string raw = _query.Set(values).ToSql();
        Assert.AreEqual($" SET {_columns[0]}=0, {_columns[1]}='Test', {_columns[2]}=NULL;", raw);
    }
    
    [Test]
    public void TestValues()
    {
        Assert.Catch<ArgumentException>(() => _query.Values(Array.Empty<object?>()));
        Assert.Catch<ArgumentException>(() => _query.Values(Array.Empty<object?[]>()));

        object?[] values = { 0, "Test", null };
        
        _query.Clear();
        string raw = _query.Values(values).ToSql();
        Assert.AreEqual(" VALUES (0, 'Test', NULL);", raw);

        object?[][] values2 =
        {
            new object?[]{ 0, "Test1", null },
            new object?[]{ 1, "Test2", 0.123 }
        }; 
        _query.Clear();
        raw = _query.Values(values2).ToSql();
        Assert.AreEqual(" VALUES (0, 'Test1', NULL), (1, 'Test2', 0.123);", raw);
    }

    [Test]
    public void TestInsert()
    {
        _query.Clear();
        string raw = _query.Insert(_schema, _table).ToSql();
        Assert.AreEqual($"INSERT INTO [{_schema}].[{_table}];", raw);
        
        _query.Clear();
        raw = _query.Insert(_table).ToSql();
        Assert.AreEqual($"INSERT INTO [{_table}];", raw);
        
        _query.Clear();
        raw = _query.Insert(_schema, _table, _columns).ToSql();
        Assert.AreEqual($"INSERT INTO [{_schema}].[{_table}] ({_columns[0]}, {_columns[1]}, {_columns[2]});", raw);
        
        _query.Clear();
        raw = _query.Insert(_table, _columns).ToSql();
        Assert.AreEqual($"INSERT INTO [{_table}] ({_columns[0]}, {_columns[1]}, {_columns[2]});", raw);

        Assert.Catch<ArgumentException>(() => _query.Insert(_schema, _table, Array.Empty<string>()));
        Assert.Catch<ArgumentException>(() => _query.Insert(_table, Array.Empty<string>()));
    }

    [Test]
    public void TestDelete()
    {
        _query.Clear();
        string raw = _query.Delete(_schema, _table).ToSql();
        Assert.AreEqual($"DELETE FROM [{_schema}].[{_table}];", raw);
        
        _query.Clear();
        raw = _query.Delete(_table).ToSql();
        Assert.AreEqual($"DELETE FROM [{_table}];", raw);
    }
    
    [Test]
    public void TestUpdate()
    {
        _query.Clear();
        string raw = _query.Update(_schema, _table).ToSql();
        Assert.AreEqual($"UPDATE [{_schema}].[{_table}];", raw);
        
        _query.Clear();
        raw = _query.Update(_table).ToSql();
        Assert.AreEqual($"UPDATE [{_table}];", raw);
    }

    [Test]
    public void TestWhere()
    {
        _query.Clear();
        var cond = new Condition(_columns[0], Comparer.Differs, "test");
        string raw = _query.Where(cond).ToSql();
        Assert.AreEqual($" WHERE ({_columns[0]} != 'test');", raw);

        var cond1 = new Condition[]
        {
            new(_columns[0], Comparer.Equals, 1, Connective.And),
            new(_columns[1], Comparer.Is, false)
        };
        _query.Clear();
        raw = _query.Where(cond1).ToSql();
        Assert.AreEqual($" WHERE (({_columns[0]} = 1) AND ({_columns[1]} IS FALSE));", raw);
        
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
        _query.Clear();
        raw = _query.Where(cond2).ToSql();
        Assert.AreEqual($" WHERE (({_columns[0]} >= '2022-01-01T00:00:00') AND (({_columns[1]} > 1.2) OR ({_columns[2]} >= 2)) OR ({_columns[2]} <= 5));", raw);
    }
}