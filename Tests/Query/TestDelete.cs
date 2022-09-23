using FluentAssertions;
using NUnit.Framework;

namespace Tests.Query;

[TestFixture]
public class TestDelete : BaseConfig
{
    [Test]
    public void When_Schema_And_Table_Are_Passed()
    {
        string raw = _query.Delete(_schema, _table).ToSql();

        raw.Should().Be($"DELETE FROM [{_schema}].[{_table}]");
    }

    [Test]
    public void When_Only_Table_Is_Passed()
    {
        string raw = _query.Delete(_table).ToSql();

        raw.Should().Be($"DELETE FROM [{_table}]");
    }
}