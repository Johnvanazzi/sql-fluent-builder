using System;
using NUnit.Framework;

namespace Tests.Statements;

[TestFixture]
public class TestValues : BaseConfig
{
    [Test]
    public void WithEmptyArray()
    {
        Assert.Catch<ArgumentException>(() => _query.Values(Array.Empty<object?>()));
        Assert.Catch<ArgumentException>(() => _query.Values(Array.Empty<object?[]>()));
    }
    
    [Test]
    public void WithSimpleArray()
    {
        object?[] values = { 0, "Test", null };
        string raw = _query.Values(values).ToSql();
        Assert.AreEqual(" VALUES (0, 'Test', NULL)", raw);
    }
    
    [Test]
    public void WithMultiIndexArray()
    {
        object?[][] values2 =
        {
            new object?[]{ 0, "Test1", null },
            new object?[]{ 1, "Test2", 0.123 }
        }; 
        string raw = _query.Values(values2).ToSql();
        Assert.AreEqual(" VALUES (0, 'Test1', NULL), (1, 'Test2', 0.123)", raw);
    }
}