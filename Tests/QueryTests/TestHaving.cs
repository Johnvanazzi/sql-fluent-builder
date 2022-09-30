using System;
using FluentAssertions;
using SqlFluentBuilder.Operators;
using NUnit.Framework;

namespace Tests.QueryTests;

[TestFixture]
public class TestHaving : BaseConfig
{
    [Test]
    public void When_Complex_Condition_Is_Passed()
    {
        var conditions = new Condition()
            .GreaterEqual(_columns[0], new DateTime(2022, 01, 01)).And
            .Nested(c => c
                .Greater(_columns[1], 1.2).Or
                .GreaterEqual(_columns[2], 2)
            ).Or
            .LessEqual(_columns[2], 5);

        string result = _query.Having(conditions).ToSql();

        result.Should().Be($" HAVING ({_columns[0]} >= '2022-01-01T00:00:00') AND (({_columns[1]} > 1.2) OR ({_columns[2]} >= 2)) OR ({_columns[2]} <= 5)");
    }

    [Test]
    public void When_Lambda_Condition_Is_Passed()
    {
        string result = _query.Having(cond => cond.GreaterEqual(_columns[0], new DateTime(2022, 01, 01)).And
            .Nested(c => c
                .Greater(_columns[1], 1.2).Or
                .GreaterEqual(_columns[2], 2)
            ).Or
            .LessEqual(_columns[2], 5)
        ).ToSql();

        result.Should().Be($" HAVING ({_columns[0]} >= '2022-01-01T00:00:00') AND (({_columns[1]} > 1.2) OR ({_columns[2]} >= 2)) OR ({_columns[2]} <= 5)");
    }
}