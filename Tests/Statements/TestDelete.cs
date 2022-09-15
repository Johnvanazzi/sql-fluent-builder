using NUnit.Framework;

namespace Tests.Statements;

[TestFixture]
public class TestDelete : BaseConfig
{
    [Test]
    public void WithSchemaAndTable()
    {
        string raw = _query.Delete(_schema, _table).ToSql();
        Assert.AreEqual($"DELETE FROM [{_schema}].[{_table}]", raw);
    }
    
    [Test]
    public void WithTable()
    {
        string raw = _query.Delete(_table).ToSql();
        Assert.AreEqual($"DELETE FROM [{_table}]", raw);
    }
}