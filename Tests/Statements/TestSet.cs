using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Tests.Statements;

[TestFixture]
public class TestSet : BaseConfig
{
    [Test]
    public void WithEmptyDictionary()
    {
        Assert.Catch<ArgumentException>(() => _query.Set(new Dictionary<string, object?>()));
    }
    
    [Test]
    public void WithCustomDictionary()
    {
        var values = new Dictionary<string, object?>
        {
            { _columns[0], 0 },
            { _columns[1], "Test" },
            { _columns[2], null }
        };
        string raw = _query.Set(values).ToSql();
        Assert.AreEqual($" SET {_columns[0]}=0, {_columns[1]}='Test', {_columns[2]}=NULL", raw);
    }
}