using FluentAssertions;
using NUnit.Framework;

namespace Tests.Query;

[TestFixture]
public class TestJoin : BaseConfig
{
    [Test]
    public void When_Table_Is_Passed_To_InnerJoin()
    {
        string result = _query.InnerJoin(_table).ToSql();

        result.Should().Be($" INNER JOIN [{_table}]");
    }

    [Test]
    public void When_Schema_And_Table_Is_Passed_To_InnerJoin()
    {
        string result = _query.InnerJoin(_schema, _table).ToSql();

        result.Should().Be($" INNER JOIN [{_schema}].[{_table}]");
    }

    [Test]
    public void When_Table_Is_Passed_To_OuterJoin()
    {
        string result = _query.OuterJoin(_table).ToSql();

        result.Should().Be($" OUTER JOIN [{_table}]");
    }

    [Test]
    public void When_Schema_And_Table_Is_Passed_To_OuterJoin()
    {
        string result = _query.OuterJoin(_schema, _table).ToSql();

        result.Should().Be($" OUTER JOIN [{_schema}].[{_table}]");
    }

    [Test]
    public void When_Table_Is_Passed_To_LeftJoin()
    {
        string result = _query.LeftJoin(_table).ToSql();

        result.Should().Be($" LEFT JOIN [{_table}]");
    }

    [Test]
    public void When_Schema_And_Table_Is_Passed_To_LeftJoin()
    {
        string result = _query.LeftJoin(_schema, _table).ToSql();

        result.Should().Be($" LEFT JOIN [{_schema}].[{_table}]");
    }

    [Test]
    public void When_Table_Is_Passed_To_RightJoin()
    {
        string result = _query.RightJoin(_table).ToSql();

        result.Should().Be($" RIGHT JOIN [{_table}]");
    }

    [Test]
    public void When_Schema_And_Table_Is_Passed_To_RightJoin()
    {
        string result = _query.RightJoin(_schema, _table).ToSql();

        result.Should().Be($" RIGHT JOIN [{_schema}].[{_table}]");
    }

    [Test]
    public void When_Table_Is_Passed_To_CrossJoin()
    {
        string result = _query.CrossJoin(_table).ToSql();

        result.Should().Be($" CROSS JOIN [{_table}]");
    }

    [Test]
    public void When_Schema_And_Table_Is_Passed_To_CrossJoin()
    {
        string result = _query.CrossJoin(_schema, _table).ToSql();

        result.Should().Be($" CROSS JOIN [{_schema}].[{_table}]");
    }
}