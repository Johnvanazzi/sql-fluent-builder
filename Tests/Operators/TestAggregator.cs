using FluentAssertions;
using FluentAssertions.Execution;
using Lib.QueryBuilder.Operators;
using NUnit.Framework;

namespace Tests.Operators;

public class TestAggregator
{
    private string _column;

    [OneTimeSetUp]
    public void SetUp()
    {
        _column = "col1";
    }

    [Test]
    public void When_Sum_IsCalled()
    {
        string sumResult = Aggregator.Sum();
        string sumColumnResult = Aggregator.Sum(_column);
        
        using (new AssertionScope())
        {
            sumResult.Should().Be("SUM(*)");
            sumColumnResult.Should().Be($"SUM({_column})");
        }
    }

    [Test]
    public void When_Avg_IsCalled()
    {
        string avgResult = Aggregator.Avg();
        string avgColumnResult = Aggregator.Avg(_column);
        
        using (new AssertionScope())
        {
            avgResult.Should().Be("AVG(*)");
            avgColumnResult.Should().Be($"AVG({_column})");
        }
    }

    [Test]
    public void When_Min_IsCalled()
    {
        string minResult = Aggregator.Min();
        string minColumnResult = Aggregator.Min(_column);
        
        using (new AssertionScope())
        {
            minResult.Should().Be("MIN(*)");
            minColumnResult.Should().Be($"MIN({_column})");
        }
    }

    [Test]
    public void When_Max_IsCalled()
    {
        string maxResult = Aggregator.Max();
        string maxColumnResult = Aggregator.Max(_column);
        
        using (new AssertionScope())
        {
            maxResult.Should().Be("MAX(*)");
            maxColumnResult.Should().Be($"MAX({_column})");
        }
    }

    [Test]
    public void When_Count_IsCalled()
    {
        string countResult = Aggregator.Count();
        string countColumnResult = Aggregator.Count(_column);
        
        using (new AssertionScope())
        {
            countResult.Should().Be("COUNT(*)");
            countColumnResult.Should().Be($"COUNT({_column})");
        }
    }
}