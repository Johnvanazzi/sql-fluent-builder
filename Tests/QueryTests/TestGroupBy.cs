using System;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.QueryTests;

[TestFixture]
public class TestGroupBy : BaseConfig
{
    [Test]
    public void When_Columns_Are_Passed()
    {
        string result = _query.GroupBy(_columns).ToSql();

        result.Should().Be($" GROUP BY [{_columns[0]}], [{_columns[1]}], [{_columns[2]}]");
    }

    [Test]
    public void When_Empty_Array_Is_Passed()
    {
        Action action = () => _query.GroupBy(Array.Empty<string>());

        action.Should().Throw<ArgumentException>("method does not accept empty arrays");
    }
}