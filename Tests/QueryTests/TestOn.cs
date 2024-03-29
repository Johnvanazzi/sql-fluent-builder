using FluentAssertions;
using SqlFluentBuilder.Operators;
using NUnit.Framework;

namespace Tests.QueryTests;

[TestFixture]
public class TestOn : BaseConfig
{
    [Test]
    public void When_Keys_Connective_And_Condition_Are_Passed()
    {
        var conditions = new Condition()
            .Equals(_columns[0], 1).And
            .IsNull(_columns[1]);

        string result = _query.On("table1", "key1", "table2", "key2", Connective.And, conditions).ToSql();

        result.Should().Be($" ON [table1].[key1] = [table2].[key2] AND ([{_columns[0]}] = 1) AND ([{_columns[1]}] IS NULL)");
    }

    [Test]
    public void When_Only_Keys_Are_Passed()
    {
        string result = _query.On("table1","key1", "table2","key2").ToSql();

        result.Should().Be(" ON [table1].[key1] = [table2].[key2]");
    }
}