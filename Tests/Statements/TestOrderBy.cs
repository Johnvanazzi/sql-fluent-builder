using System;
using NUnit.Framework;

namespace Tests.Statements;

[TestFixture]
public class TestOrderBy : BaseConfig
{
    [Test]
    public void WithColumns()
    {
        string raw = _query.OrderBy(_columns).ToSql();
        Assert.AreEqual($" ORDER BY {_columns[0]}, {_columns[1]}, {_columns[2]}", raw);
    }
    
    [Test]
    public void WithEmptyArray()
    {
        Assert.Catch<ArgumentException>(() => _query.OrderBy(Array.Empty<string>()));
    }
}