using FluentAssertions;
using NUnit.Framework;

namespace Tests.QueryTests;

[TestFixture]
public class TestFrom : BaseConfig
{
    [Test]
    public void When_Only_Table_Is_Passed()
    {
        string raw = _query.From(_table).ToSql();
        raw.Should().Be($" FROM [{_table}]");
    }

    [Test]
    public void When_Schema_And_Table_Are_Passed()
    {
        string raw = _query.Select().From(_schema, _table).ToSql();
        raw.Should().Be($"SELECT * FROM [{_schema}].[{_table}]");
    }
}