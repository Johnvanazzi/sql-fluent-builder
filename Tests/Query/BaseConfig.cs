using FluentAssertions;
using NUnit.Framework;

namespace Tests.Query;

public class BaseConfig
{
    protected string[] _columns;
    protected string _table;
    protected string _schema;
    protected Lib.QueryBuilder.Query _query;

    [OneTimeSetUp]
    public void Setup()
    {
        _columns = new[] { "col1", "col2", "col3" };
        _table = "table1";
        _schema = "schema1";
        _query = new Lib.QueryBuilder.Query();
    }

    [TearDown]
    public void ClearQuery() => _query.Clear();

    [Test]
    public void TestClear()
    {
        _query.Select();
        _query.Clear();

        string result = _query.ToSql();

        result.Should().Be("");
    }
}