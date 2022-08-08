using Lib.QueryBuilder.Operators;
using NUnit.Framework;

namespace Tests;

public class AggregatorTest
{
    private string _column;
    
    [SetUp]
    public void SetUp()
    {
        _column = "col1";
    }

    [Test]
    public void TestSum()
    {
        Assert.AreEqual("SUM(*)", Aggregator.Sum());
        Assert.AreEqual($"SUM({_column})", Aggregator.Sum(_column));
    }
    
    [Test]
    public void TestAvg()
    {
        Assert.AreEqual("AVG(*)", Aggregator.Avg());
        Assert.AreEqual($"AVG({_column})", Aggregator.Avg(_column));
    }
    
    [Test]
    public void TestMin()
    {
        Assert.AreEqual("MIN(*)", Aggregator.Min());
        Assert.AreEqual($"MIN({_column})", Aggregator.Min(_column));
    }
    
    [Test]
    public void TestMax()
    {
        Assert.AreEqual("MAX(*)", Aggregator.Max());
        Assert.AreEqual($"MAX({_column})", Aggregator.Max(_column));
    }
    
    [Test]
    public void TestCount()
    {
        Assert.AreEqual("COUNT(*)",Aggregator.Count());
        Assert.AreEqual($"COUNT({_column})",Aggregator.Count(_column));
    }
}