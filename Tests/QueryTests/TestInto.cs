using FluentAssertions;
using NUnit.Framework;

namespace Tests.QueryTests;

[TestFixture]
public class TestInto : BaseConfig
{
    [Test]
    public void When_Into_With_Only_NewTable_Is_Called()
    {
        string result = _query.Into(_table).ToSql();

        result.Should().Be($" INTO [{_table}]");
    }
    
    [Test]
    public void When_Into_With_NewTable_And_Schema_Is_Called()
    {
        string result = _query.Into(_schema, _table).ToSql();

        result.Should().Be($" INTO [{_schema}].[{_table}]");
    }
    
    [Test]
    public void When_IntoIn_With_NewTable_And_ExternalDb_Is_Called()
    {
        string externalDb = "testDb";
        string result = _query.IntoIn(_table, externalDb).ToSql();

        result.Should().Be($" INTO [{_table}] IN '{externalDb}'");
    }
    
    [Test]
    public void When_IntoIn_With_NewTable_Schema_And_ExternalDb_Is_Called()
    {
        string externalDb = "testDb";
        string result = _query.IntoIn(_schema, _table, externalDb).ToSql();

        result.Should().Be($" INTO [{_schema}].[{_table}] IN '{externalDb}'");
    }
}