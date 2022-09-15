using NUnit.Framework;

namespace Tests.Statements;

[TestFixture]
public class TestUpdate : BaseConfig
{
    [Test]
    public void WithSchemaAndTable()
    {
        string raw = _query.Update(_schema, _table).ToSql();
        Assert.AreEqual($"UPDATE [{_schema}].[{_table}]", raw);
    }
    
    [Test]
    public void WithTable()
    {
        string raw = _query.Update(_table).ToSql();
        Assert.AreEqual($"UPDATE [{_table}]", raw);
    }
}