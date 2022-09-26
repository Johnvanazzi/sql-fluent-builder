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
        ArrayValidations.ItsNotEmpty(columns, nameof(columns));

        AppendCommand("INSERT INTO", schema, table)._sb
            .Append(" (")
            .AppendJoin(", ", columns)
            .Append(')');

        return this;
    }

    public IValues Insert(string table, string[] columns)
    {
        ArrayValidations.ItsNotEmpty(columns, nameof(columns));

        AppendCommand("INSERT INTO", table)._sb
            .Append(" (")
            .AppendJoin(", ", columns)
            .Append(')');

        return this;
    }

    private Query AppendCommand(string command, string table)
    {
        _sb.Append($"{command} [{table}]");

        return this;
    }

    private Query AppendCommand(string command, string schema, string table) =>
        AppendCommand(command, $"{schema}].[{table}");
}