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
        Assert.AreEqual("NULL", Converter.ObjectToSql(null));
        Assert.AreEqual("1", Converter.ObjectToSql((int) 1));
        Assert.AreEqual("1", Converter.ObjectToSql((long) 1));
        Assert.AreEqual("'c'", Converter.ObjectToSql((char) 'c'));
        Assert.AreEqual("'test string'", Converter.ObjectToSql((string) "test string"));
        Assert.AreEqual("1", Converter.ObjectToSql((byte) 1));
        Assert.AreEqual("TRUE", Converter.ObjectToSql(true));
        Assert.AreEqual("FALSE", Converter.ObjectToSql(false));
        Assert.AreEqual("12.3456789", Converter.ObjectToSql((decimal) 12.3456789));
        Assert.AreEqual("12.3456789", Converter.ObjectToSql((double) 12.3456789));
        Assert.AreEqual("12.3456", Converter.ObjectToSql((float) 12.3456));
        Assert.AreEqual("'00000000-0000-0000-0000-000000000000'", Converter.ObjectToSql(Guid.Empty));
        Assert.AreEqual("'1970-01-01T00:00:00'", Converter.ObjectToSql(new DateTime(1970, 1, 1, 0, 0, 0)));
        Assert.Catch<ArgumentOutOfRangeException>(() => Converter.ObjectToSql(new object()));
    }
    
    [Test]
    public void TestLogicalToSql()
    {
        Assert.AreEqual("AND", Converter.LogicalToSql(Connective.And));
        Assert.AreEqual("OR", Converter.LogicalToSql(Connective.Or));
    }
    
    [Test]
    public void TestComparerToSql()
    {
        Assert.AreEqual("!=", Converter.ComparerToSql(Comparer.Differs));
        Assert.AreEqual("=", Converter.ComparerToSql(Comparer.Equals));
        Assert.AreEqual(">=", Converter.ComparerToSql(Comparer.GreaterEqualThan));
        Assert.AreEqual(">", Converter.ComparerToSql(Comparer.GreaterThan));
        Assert.AreEqual("IS", Converter.ComparerToSql(Comparer.Is));
        Assert.AreEqual("IS NOT", Converter.ComparerToSql(Comparer.IsNot));
        Assert.AreEqual("<", Converter.ComparerToSql(Comparer.LessThan));
        Assert.AreEqual("<=", Converter.ComparerToSql(Comparer.LessEqualThan));
    }
}