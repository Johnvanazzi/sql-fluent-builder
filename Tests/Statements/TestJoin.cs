using Lib.QueryBuilder.Operators;
using Lib.QueryBuilder.Utils;
using NUnit.Framework;

namespace Tests.Statements;

[TestFixture]
public class TestJoin : BaseConfig
{
    [Test]
    public void WithOnlyTable()
    {
        string raw1 = _query.InnerJoin(_table).ToSql();
        _query.Clear();
        string raw2 = _query.OuterJoin(_table).ToSql();
        _query.Clear();
        string raw3 = _query.LeftJoin(_table).ToSql();
        _query.Clear();
        string raw4 = _query.RightJoin(_table).ToSql();
        _query.Clear();
        string raw5 = _query.CrossJoin(_table).ToSql();

        Assert.AreEqual($" INNER JOIN [{_table}]", raw1);
        Assert.AreEqual($" OUTER JOIN [{_table}]", raw2);
        Assert.AreEqual($" LEFT JOIN [{_table}]", raw3);
        Assert.AreEqual($" RIGHT JOIN [{_table}]", raw4);
        Assert.AreEqual($" CROSS JOIN [{_table}]", raw5);
    }
    
    [Test]
    public void WithTableAndSchema()
    {
        string raw1 = _query.InnerJoin(_schema, _table).ToSql();
        _query.Clear();
        string raw2 = _query.OuterJoin(_schema, _table).ToSql();
        _query.Clear();
        string raw3 = _query.LeftJoin(_schema, _table).ToSql();
        _query.Clear();
        string raw4 = _query.RightJoin(_schema, _table).ToSql();
        _query.Clear();
        string raw5 = _query.CrossJoin(_schema, _table).ToSql();

        Assert.AreEqual($" INNER JOIN [{_schema}].[{_table}]", raw1);
        Assert.AreEqual($" OUTER JOIN [{_schema}].[{_table}]", raw2);
        Assert.AreEqual($" LEFT JOIN [{_schema}].[{_table}]", raw3);
        Assert.AreEqual($" RIGHT JOIN [{_schema}].[{_table}]", raw4);
        Assert.AreEqual($" CROSS JOIN [{_schema}].[{_table}]", raw5);
    }
}