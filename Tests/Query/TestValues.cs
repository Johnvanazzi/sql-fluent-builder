using System;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;

namespace Tests.Query;

[TestFixture]
public class TestValues : BaseConfig
{
    [Test]
    public void When_Empty_Array_Is_Passed()
    {
        Action action1 = () => _query.Values(Array.Empty<object?>());
        Action action2 = () => _query.Values(Array.Empty<object?[]>());

        using (new AssertionScope())
        {
            action1.Should().Throw<ArgumentException>();
            action2.Should().Throw<ArgumentException>();
        }
    }

    [Test]
    public void When_Simple_Array_Is_Passed()
    {
        object?[] values = { 0, "Test", null };

        string raw = _query.Values(values).ToSql();

        raw.Should().Be(" VALUES (0, 'Test', NULL)");
    }

    [Test]
    public void When_MultiIndex_Array_Is_Passed()
    {
        object?[][] values2 =
        {
            new object?[] { 0, "Test1", null },
            new object?[] { 1, "Test2", 0.123 }
        };
        
        string result = _query.Values(values2).ToSql();
        
        result.Should().Be(" VALUES (0, 'Test1', NULL), (1, 'Test2', 0.123)");
    }
}