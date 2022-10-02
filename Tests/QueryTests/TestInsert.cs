using System;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;

namespace Tests.QueryTests;

[TestFixture]
public class TestInsert : BaseConfig
{
    [Test]
    public void WithSchemaAndTable()
    {
        string result = _query.InsertInto(_schema, _table).ToSql();

        result.Should().Be($"INSERT INTO [{_schema}].[{_table}]");
    }

    [Test]
    public void WithTable()
    {
        string result = _query.InsertInto(_table).ToSql();

        result.Should().Be($"INSERT INTO [{_table}]");
    }

    [Test]
    public void WithSchemaAndTableAndColumns()
    {
        string result = _query.InsertInto(_schema, _table, _columns).ToSql();

        result.Should().Be($"INSERT INTO [{_schema}].[{_table}] ([{_columns[0]}], [{_columns[1]}], [{_columns[2]}])");
    }

    [Test]
    public void WithTableAndColumns()
    {
        string result = _query.InsertInto(_table, _columns).ToSql();

        result.Should().Be($"INSERT INTO [{_table}] ([{_columns[0]}], [{_columns[1]}], [{_columns[2]}])");
    }

    [Test]
    public void When_Empty_Array_Is_Passed()
    {
        Action action1 = () => _query.InsertInto(_schema, _table, Array.Empty<string>());
        Action action2 = () => _query.InsertInto(_table, Array.Empty<string>());

        using (new AssertionScope())
        {
            action1.Should().Throw<ArgumentException>();
            action2.Should().Throw<ArgumentException>();
        }
    }
}