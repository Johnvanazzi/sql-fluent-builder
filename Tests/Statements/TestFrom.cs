using NUnit.Framework;

namespace Tests.Statements;

[TestFixture]
public class TestFrom : BaseConfig
{
    [Test]
    public void WithTable()
    {
        string raw = _query.From(_table).ToSql();
        Assert.AreEqual($" FROM [{_table}]", raw);
    }

    [Test]
    public void WithSchemaAndTable()
    {
        string raw = _query.Select().From(_schema, _table).ToSql();
        Assert.AreEqual($"SELECT * FROM [{_schema}].[{_table}]", raw);
    }
}