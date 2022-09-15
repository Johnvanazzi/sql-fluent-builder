using System;
using Lib.QueryBuilder.Operators;
using Lib.QueryBuilder.Utils;
using NUnit.Framework;

namespace Tests;

public class ConverterTest
{
    [SetUp]
    public void SetUp()
    {
        
    }

    [Test]
    public void TestObjectToSql()
    {
        Assert.AreEqual("NULL", Converter.ToSql(null));
        Assert.AreEqual("1", ((int) 1).ToSql());
        Assert.AreEqual("10000000000", ((long) 1e+10).ToSql());
        Assert.AreEqual("'c'", ((char) 'c').ToSql());
        Assert.AreEqual("'test string'", ((string) "test string").ToSql());
        Assert.AreEqual("1", ((byte) 1).ToSql());
        Assert.AreEqual("TRUE", true.ToSql());
        Assert.AreEqual("FALSE", false.ToSql());
        Assert.AreEqual("12.3456789", ((decimal) 12.3456789).ToSql());
        Assert.AreEqual("12.3456789", ((double) 12.3456789).ToSql());
        Assert.AreEqual("12.3456", ((float) 12.3456).ToSql());
        Assert.AreEqual("'00000000-0000-0000-0000-000000000000'", Guid.Empty.ToSql());
        Assert.AreEqual("'1970-01-01T00:00:00'", new DateTime(1970, 1, 1, 0, 0, 0).ToSql());
        Assert.Catch<ArgumentOutOfRangeException>(() => new object().ToSql());
    }
    
    [Test]
    public void TestLogicalToSql()
    {
        Assert.AreEqual("AND", Connective.And.ToSql());
        Assert.AreEqual("OR", Connective.Or.ToSql());
    }
    
    [Test]
    public void TestComparerToSql()
    {
        Assert.AreEqual("!=", Comparer.Differs.ToSql());
        Assert.AreEqual("=", Comparer.Equals.ToSql());
        Assert.AreEqual(">=", Comparer.GreaterEqualThan.ToSql());
        Assert.AreEqual(">", Comparer.GreaterThan.ToSql());
        Assert.AreEqual("IS", Comparer.Is.ToSql());
        Assert.AreEqual("IS NOT", Comparer.IsNot.ToSql());
        Assert.AreEqual("<", Comparer.LessThan.ToSql());
        Assert.AreEqual("<=", Comparer.LessEqualThan.ToSql());
    }
}