using System;
using FluentAssertions;
using FluentAssertions.Execution;
using Lib.QueryBuilder.Operators;
using Lib.QueryBuilder.Utils;
using NUnit.Framework;

namespace Tests.Utils;

public class TestConverter
{
    [Test]
    public void When_ToSql_IsCalled_In_Object()
    {
        string nullResult = Converter.ToSql(null);
        string intResult = ((int)1).ToSql();
        string longResult = ((long)1e+10).ToSql();
        string charResult = ((char)'c').ToSql();
        string stringResult = ((string)"test string").ToSql();
        string byteResult = ((byte)1).ToSql();
        string trueResult = true.ToSql();
        string falseResult = false.ToSql();
        string decimalResult = ((decimal)12.3456789).ToSql();
        string doubleResult = ((double)12.3456789).ToSql();
        string floatResult = ((float)12.3456).ToSql();
        string guidResult = Guid.Empty.ToSql();
        string dateResult = new DateTime(1970, 1, 1, 0, 0, 0).ToSql();
        Action actionResult = () => new object().ToSql();

        using (new AssertionScope())
        {
            nullResult.Should().Be("NULL");
            intResult.Should().Be("1");
            longResult.Should().Be("10000000000");
            charResult.Should().Be("'c'");
            stringResult.Should().Be("'test string'");
            byteResult.Should().Be("1");
            trueResult.Should().Be("TRUE");
            falseResult.Should().Be("FALSE");
            decimalResult.Should().Be("12.3456789");
            doubleResult.Should().Be("12.3456789");
            floatResult.Should().Be("12.3456");
            guidResult.Should().Be("'00000000-0000-0000-0000-000000000000'");
            dateResult.Should().Be("'1970-01-01T00:00:00'");
            actionResult.Should().Throw<ArgumentOutOfRangeException>();
        }
    }

    [Test]
    public void When_ToSql_IsCalled_In_Logical()
    {
        string andResult = Connective.And.ToSql();
        string orResult = Connective.Or.ToSql();

        using (new AssertionScope())
        {
            andResult.Should().Be("AND");
            orResult.Should().Be("OR");
        }
    }

    [Test]
    public void When_ToSql_IsCalled_In_Comparer()
    {
        string allResult = Comparer.All.ToSql();
        string anyResult = Comparer.Any.ToSql();
        string betweenResult = Comparer.Between.ToSql();
        string differsResult = Comparer.Differs.ToSql();
        string equalsResult = Comparer.Equals.ToSql();
        string greaterEqualResult = Comparer.GreaterEqual.ToSql();
        string greaterResult = Comparer.Greater.ToSql();
        string inResult = Comparer.In.ToSql();
        string isResult = Comparer.Is.ToSql();
        string isNotResult = Comparer.IsNot.ToSql();
        string lessResult = Comparer.Less.ToSql();
        string lessEqualResult = Comparer.LessEqual.ToSql();
        string likeResult = Comparer.Like.ToSql();
        string notBetweenResult = Comparer.NotBetween.ToSql();
        string notInResult = Comparer.NotIn.ToSql();
        string notLikeResult = Comparer.NotLike.ToSql();

        using (new AssertionScope())
        {
            allResult.Should().Be("ALL");
            anyResult.Should().Be("ANY");
            betweenResult.Should().Be("BETWEEN");
            differsResult.Should().Be("!=");
            equalsResult.Should().Be("=");
            greaterEqualResult.Should().Be(">=");
            greaterResult.Should().Be(">");
            inResult.Should().Be("IN");
            isResult.Should().Be("IS");
            isNotResult.Should().Be("IS NOT");
            lessResult.Should().Be("<");
            lessEqualResult.Should().Be("<=");
            likeResult.Should().Be("LIKE");
            notBetweenResult.Should().Be("NOT BETWEEN");
            notInResult.Should().Be("NOT IN");
            notLikeResult.Should().Be("NOT LIKE");
        }
    }
}