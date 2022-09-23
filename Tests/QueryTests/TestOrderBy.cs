using System;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.QueryTests;

[TestFixture]
public class TestOrderBy : BaseConfig
{
    [Test]
    public void When_Columns_Array_Is_Passed()
    {
        string result = _query.OrderBy(_columns).ToSql();

        result.Should().Be($" ORDER BY {_columns[0]}, {_columns[1]}, {_columns[2]}");
    }

    [Test]
    public void When_Empty_Array_Is_Passed()
    {
        Action action = () => _query.OrderBy(Array.Empty<string>());

        action.Should().Throw<ArgumentException>();
    }
}