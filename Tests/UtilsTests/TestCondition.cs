using System;
using FluentAssertions;
using SqlFluentBuilder;
using SqlFluentBuilder.Operators;
using NUnit.Framework;

namespace Tests.UtilsTests;

public class TestCondition
{
    private Condition _condition;
    private string _column;
    private string _table;
    private Query _subQuery;

    [OneTimeSetUp]
    public void SetUp()
    {
        _condition = new Condition();
        _column = "col";
        _table = "table";
        _subQuery = new Query();

        _subQuery.Select().From(_table);
    }

    [TearDown]
    public void ClearStringBuilder()
    {
        _condition.Sb.Clear();
    }

    [Test]
    public void When_Differs_Is_Called()
    {
        string result = _condition.Differs(_column, 1).ToSql();

        result.Should().Be($"([{_column}] != 1)");
    }

    [Test]
    public void When_Equals_Is_Called()
    {
        string result = _condition.Equals(_column, 'a').ToSql();

        result.Should().Be($"([{_column}] = 'a')");
    }

    [Test]
    public void When_Exists_Is_Called()
    {
        string result = _condition.Exists(_subQuery).ToSql();

        result.Should().Be($"EXISTS ({_subQuery.ToSql()})");
    }
    
    [Test]
    public void When_Greater_Is_Called()
    {
        string result = _condition.Greater(_column, new DateTime(1970,1,1,0,0,0)).ToSql();

        result.Should().Be($"([{_column}] > '1970-01-01T00:00:00')");
    }
    
    [Test]
    public void When_GreaterEqual_Is_Called()
    {
        string result = _condition.GreaterEqual(_column, 1).ToSql();

        result.Should().Be($"([{_column}] >= 1)");
    }
    
    [Test]
    public void When_In_With_Array_Is_Called()
    {
        object?[] values = { 1, "abc", 2 };
        string result = _condition.In(_column, values).ToSql();

        result.Should().Be($"([{_column}] IN (1, 'abc', 2))");
    }
    
    [Test]
    public void When_In_With_SubQuery_Is_Called()
    {
        string result = _condition.In(_column, _subQuery).ToSql();

        result.Should().Be($"([{_column}] IN ({_subQuery.ToSql()}))");
    }
    
    [Test]
    public void When_Is_Is_Called()
    {
        string result = _condition.IsNull(_column).ToSql();

        result.Should().Be($"([{_column}] IS NULL)");
    }
    
    [Test]
    public void When_IsNot_Is_Called()
    {
        string result = _condition.IsNotNull(_column).ToSql();

        result.Should().Be($"([{_column}] IS NOT NULL)");
    }
    
    [Test]
    public void When_Less_Is_Called()
    {
        string result = _condition.Less(_column, 0.123).ToSql();

        result.Should().Be($"([{_column}] < 0.123)");
    }
    
    [Test]
    public void When_LessEqual_Is_Called()
    {
        string result = _condition.LessEqual(_column, 0).ToSql();

        result.Should().Be($"([{_column}] <= 0)");
    }
    
    [Test]
    public void When_Nested_Is_Called()
    {
        string result = _condition.Nested(c => c.Equals(_column, 1)).ToSql();

        result.Should().Be($"(([{_column}] = 1))");
    }
    
    [Test]
    public void When_NotBetween_Is_Called()
    {
        string result = _condition.NotBetween(_column, 0, 1).ToSql();

        result.Should().Be($"([{_column}] NOT BETWEEN 0 AND 1)");
    }
    
    [Test]
    public void When_NotIn_With_Array_Is_Called()
    {
        string result = _condition.NotIn(_column, 0, 'a').ToSql();

        result.Should().Be($"([{_column}] NOT IN (0, 'a'))");
    }
    
    [Test]
    public void When_NotIn_With_Subquery_Is_Called()
    {
        string result = _condition.NotIn(_column, _subQuery).ToSql();

        result.Should().Be($"([{_column}] NOT IN ({_subQuery.ToSql()}))");
    }
    
    [Test]
    public void When_All_Is_Called()
    {
        string result = _condition.All(_column, Comparer.Equals, _subQuery).ToSql();

        result.Should().Be($"([{_column}] = ALL ({_subQuery.ToSql()}))");
    }
    
    [Test]
    public void When_Any_Is_Called()
    {
        string result = _condition.Any(_column, Comparer.Equals, _subQuery).ToSql();

        result.Should().Be($"([{_column}] = ANY ({_subQuery.ToSql()}))");
    }
    
    [Test]
    public void When_Between_Is_Called()
    {
        string result = _condition.Between(_column, 0.001, 1).ToSql();

        result.Should().Be($"([{_column}] BETWEEN 0.001 AND 1)");
    }
    
    [Test]
    public void When_NotLike_Is_Called()
    {
        string result = _condition.NotLike(_column, "%a%").ToSql();

        result.Should().Be($"([{_column}] NOT LIKE '%a%')");
    }
    
    [Test]
    public void When_Like_Is_Called()
    {
        string result = _condition.Like(_column, "%a%").ToSql();

        result.Should().Be($"([{_column}] LIKE '%a%')");
    }

    [Test]
    public void When_Connective_Or_Is_Used()
    {
        string orResult = _condition.Equals(_column, "A").Or.IsNotNull(_column).ToSql();
        
        orResult.Should().Be($"([{_column}] = 'A') OR ([{_column}] IS NOT NULL)");   
    }
    
    [Test]
    public void When_Connective_And_Is_Used()
    {
        string andResult = _condition.Less(_column, 0).And.Greater(_column, 2).ToSql();

        andResult.Should().Be($"([{_column}] < 0) AND ([{_column}] > 2)");   
    }
}