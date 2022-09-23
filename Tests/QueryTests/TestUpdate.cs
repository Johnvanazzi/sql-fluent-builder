using FluentAssertions;
using NUnit.Framework;

namespace Tests.QueryTests;

[TestFixture]
public class TestUpdate : BaseConfig
{
    [Test]
    public void When_Schema_And_Table_Are_Passed()
    {
        string result = _query.Update(_schema, _table).ToSql();
        
        result.Should().Be($"UPDATE [{_schema}].[{_table}]");
    }
    
    [Test]
    public void When_Only_Table_Is_Passed()
    {
        string result = _query.Update(_table).ToSql();
        
        result.Should().Be($"UPDATE [{_table}]");
    }
}