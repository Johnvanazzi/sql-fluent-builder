using FluentAssertions;
using NUnit.Framework;

namespace Tests.QueryTests;

[TestFixture]
public class TestUnion : BaseConfig
{
    [Test]
    public void When_Union_Is_Called()
    {
        string result = _query.Union().ToSql();

        result.Should().Be(" UNION ");
    }
    
    [Test]
    public void When_UnionAll_Is_Called()
    {
        string result = _query.UnionAll().ToSql();

        result.Should().Be(" UNION ALL ");
    }
}