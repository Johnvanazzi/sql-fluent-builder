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
    public void When_Into_With_NewTable_And_ExternalDb_Is_Called()
    {
        string externalDb = "testDb";
        string result = _query.Into(_table, externalDb).ToSql();

        result.Should().Be($" INTO [{_table}] IN '{externalDb}'");
    }
}