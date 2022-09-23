using Lib.QueryBuilder;
using NUnit.Framework;

namespace Tests.QueryTests;

public class BaseConfig
{
    protected string[] _columns;
    protected string _table;
    protected string _schema;
    protected Query _query;

    [OneTimeSetUp]
    public void Setup()
    {
        _columns = new[] { "col1", "col2", "col3" };
        _table = "table1";
        _schema = "schema1";
        _query = new Query();
    }

    [TearDown]
    public void ClearQuery() => _query.Clear();
}