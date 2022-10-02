using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.QueryTests;

[TestFixture]
public class TestSet : BaseConfig
{
    [Test]
    public void When_Empty_Dictionary_Is_Passed()
    {
        Action action = () => _query.Set(new Dictionary<string, object?>());

        action.Should().Throw<ArgumentException>();
    }
    
    [Test]
    public void When_Dictionary_Is_Passed()
    {
        var values = new Dictionary<string, object?>
        {
            { _columns[0], 0 },
            { _columns[1], "Test" },
            { _columns[2], null }
        };
        string result = _query.Set(values).ToSql();
        
        result.Should().Be($" SET [{_columns[0]}]=0, [{_columns[1]}]='Test', [{_columns[2]}]=NULL");
    }
}