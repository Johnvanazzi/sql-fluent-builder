using FluentAssertions;
using FluentAssertions.Execution;
using SqlFluentBuilder.Operators;
using NUnit.Framework;

namespace Tests.OperatorsTests;

public class TestAggregator
{
    private string _column;

    [OneTimeSetUp]
    public void SetUp()
    {
        _column = "col1";
    }

    [Test]
    public void When_Sum_Is_Called()
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
    public void When_Avg_Is_Called()
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
    public void When_Min_Is_Called()
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
    public void When_Max_Is_Called()
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
    public void When_Count_Is_Called()
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