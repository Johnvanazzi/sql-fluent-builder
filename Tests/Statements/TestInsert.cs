using System;
using NUnit.Framework;

namespace Tests.Statements;

[TestFixture]
public class TestInsert : BaseConfig
{
    [Test]
    public void WithSchemaAndTable()
    {
        string raw = _query.Insert(_schema, _table).ToSql();
        Assert.AreEqual($"INSERT INTO [{_schema}].[{_table}]", raw);
    }
    
    [Test]
    public void WithTable()
    {
        string raw = _query.Insert(_table).ToSql();
        Assert.AreEqual($"INSERT INTO [{_table}]", raw);
    }
    
    [Test]
    public void WithSchemaAndTableAndColumns()
    {
        string raw = _query.Insert(_schema, _table, _columns).ToSql();
        Assert.AreEqual($"INSERT INTO [{_schema}].[{_table}] ({_columns[0]}, {_columns[1]}, {_columns[2]})", raw);
    }
    
    [Test]
    public void WithTableAndColumns()
    {
        string raw = _query.Insert(_table, _columns).ToSql();
        Assert.AreEqual($"INSERT INTO [{_table}] ({_columns[0]}, {_columns[1]}, {_columns[2]})", raw);
    }
    
    [Test]
    public void WithEmptyColumnArray()
    {
        Assert.Catch<ArgumentException>(() => _query.Insert(_schema, _table, Array.Empty<string>()));
        Assert.Catch<ArgumentException>(() => _query.Insert(_table, Array.Empty<string>()));
    }
}