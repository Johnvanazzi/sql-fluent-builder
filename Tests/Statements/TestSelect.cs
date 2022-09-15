using System;
using NUnit.Framework;

namespace Tests.Statements;

[TestFixture]
public class TestSelect : BaseConfig
{
    [Test]
    public void NoColumnSpecified()
    {
        string raw = _query.Select().ToSql();
        Assert.AreEqual("SELECT *;", raw);
    }
    
    [Test]
    public void WithColumnSpecified()
    {
        string raw = _query.Select(_columns).ToSql();
        Assert.AreEqual($"SELECT {_columns[0]}, {_columns[1]}, {_columns[2]};", raw);
    }
    
    [Test]
    public void WithEmptyArray()
    {
        Assert.Catch<ArgumentException>(() => _query.Select(Array.Empty<string>()));
    }
}