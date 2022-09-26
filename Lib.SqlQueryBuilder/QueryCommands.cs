using Lib.QueryBuilder.Clauses;
using Lib.QueryBuilder.Validations;

namespace Lib.QueryBuilder;

public partial class Query
{
    public IFrom Select()
    {
        _sb.Append("SELECT *");

        return this;
    }

    public IFrom Select(params string[] columns)
    {
        ArrayValidations.ItsNotEmpty(columns, "Array of columns is empty.");

        _sb.Append("SELECT ").AppendJoin(", ", columns);

        return this;
    }

    public ISet Update(string table) => AppendCommand("UPDATE", table);
    public ISet Update(string schema, string table) => AppendCommand("UPDATE", schema, table);
    public IWhere Delete(string table) => AppendCommand("DELETE FROM", table);
    public IWhere Delete(string schema, string table) => AppendCommand("DELETE FROM", schema, table);
    public IValues Insert(string table) => AppendCommand("INSERT INTO", table);
    public IValues Insert(string schema, string table) => AppendCommand("INSERT INTO", schema, table);

    public IValues Insert(string schema, string table, string[] columns)
    {
        ArrayValidations.ItsNotEmpty(columns, "Array of columns is empty.");

        _sb.Append($"INSERT INTO [{schema}].[{table}] (").AppendJoin(", ", columns).Append(')');

        return this;
    }

    public IValues Insert(string table, string[] columns)
    {
        ArrayValidations.ItsNotEmpty(columns, "Array of columns is empty.");

        _sb.Append($"INSERT INTO [{table}] (").AppendJoin(", ", columns).Append(')');

        return this;
    }

    private Query AppendCommand(string statement, string table)
    {
        _sb.Append($"{statement} [{table}]");

        return this;
    }

    private Query AppendCommand(string statement, string schema, string table)
    {
        _sb.Append($"{statement} [{schema}].[{table}]");

        return this;
    }
}