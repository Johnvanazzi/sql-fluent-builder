using System;
using Lib.QueryBuilder;
using Lib.QueryBuilder.Operators;
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
        Assert.AreEqual("AND", Converter.ToSql(Connective.And));
        Assert.AreEqual("OR", Converter.ToSql(Connective.Or));
    }
    
    [Test]
    public void TestComparerToSql()
    {
        Assert.AreEqual("!=", Converter.ToSql(Comparer.Differs));
        Assert.AreEqual("=", Converter.ToSql(Comparer.Equals));
        Assert.AreEqual(">=", Converter.ToSql(Comparer.GreaterEqualThan));
        Assert.AreEqual(">", Converter.ToSql(Comparer.GreaterThan));
        Assert.AreEqual("IS", Converter.ToSql(Comparer.Is));
        Assert.AreEqual("IS NOT", Converter.ToSql(Comparer.IsNot));
        Assert.AreEqual("<", Converter.ToSql(Comparer.LessThan));
        Assert.AreEqual("<=", Converter.ToSql(Comparer.LessEqualThan));
    }
}