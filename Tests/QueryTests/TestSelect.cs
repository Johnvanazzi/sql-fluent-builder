using System;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.QueryTests;

[TestFixture]
public class TestSelect : BaseConfig
{
    [Test]
    public void When_No_Parameter_Is_Passed()
    {
        string result = _query.Select().ToSql();
        
        
        result.Should().Be("SELECT *");
    }
    
    [Test]
    public void When_Column_Array_Is_Passed()
    {
        string result = _query.Select(_columns).ToSql();
        
        result.Should().Be($"SELECT [{_columns[0]}], [{_columns[1]}], [{_columns[2]}]");
    }
    
    [Test]
    public void When_Empty_Array_Is_Passed()
    {
        Action action = () => _query.Select(Array.Empty<string>());

        action.Should().Throw<ArgumentException>();
    }
}