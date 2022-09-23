using FluentAssertions;
using NUnit.Framework;

namespace Tests.QueryTests;

[TestFixture]
internal class TestQueryCore : BaseConfig
{
    [Test]
    public void When_Clear_Is_Called()
    {
        _query.Select();
        ClearQuery();

        string result = _query.ToSql();

        result.Should().Be("");
    }
}