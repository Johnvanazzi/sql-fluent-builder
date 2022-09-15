using System;
using NUnit.Framework;

namespace Tests.Statements;

[TestFixture]
public class TestGroupBy : BaseConfig
{
    [Test]
    public void WithColumns()
    {
        string raw = _query.GroupBy(_columns).ToSql();
        Assert.AreEqual($" GROUP BY {_columns[0]}, {_columns[1]}, {_columns[2]}", raw);
    }

    [Test]
    public void WithEmptyArray()
    {
        Assert.Catch<ArgumentException>(() =>_query.GroupBy(Array.Empty<string>()));
    }
}