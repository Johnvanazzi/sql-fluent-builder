using System;
using Lib.QueryBuilder;
using NUnit.Framework;

namespace Tests;

[TestFixture]
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
        Assert.Catch<ArgumentException>(() =>_query.OrderBy(Array.Empty<string>()));
    }
}