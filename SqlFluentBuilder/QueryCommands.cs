using SqlFluentBuilder.Clauses;
using SqlFluentBuilder.Validations;

namespace SqlFluentBuilder;

public partial class Query : IInsertInto, ISelect, IDeleteFrom, IUpdate
{
    /// <summary>
    /// Appends a 'SELECT *' command to the query builder. Thus, will get all columns from the table specified.
    /// </summary>
    /// <returns></returns>
    public IFrom Select()
    {
        _sb.Append("SELECT *");

        return this;
    }

    /// <summary>
    /// Appends a 'SELECT' command to the query builder.
    /// </summary>
    /// <param name="columns">String array containing the columns names to be selected.</param>
    /// <returns></returns>
    public IFrom Select(params string[] columns)
    {
        ArrayValidations.ItsNotEmpty(columns, "Array of columns is empty.");

        _sb.Append("SELECT ").AppendJoin(", ", columns);

        return this;
    }

    /// <summary>
    /// Appends an 'UPDATE' command to the query builder.
    /// </summary>
    /// <param name="table">The name of the table whose data will be updated.</param>
    /// <returns></returns>
    public ISet Update(string table) => AppendCommand("UPDATE", table);

    /// <summary>
    /// Appends an 'UPDATE' command to the query builder.
    /// </summary>
    /// <param name="schema">The database schema where the table is.</param>
    /// <param name="table">The name of the table whose data will be updated.</param>
    /// <returns></returns>
    public ISet Update(string schema, string table) => AppendCommand("UPDATE", schema, table);

    /// <summary>
    /// Appends a 'DELETE FROM' command to the query builder.
    /// </summary>
    /// <param name="table">The name of the table whose data will be deleted.</param>
    /// <returns></returns>
    public IWhere DeleteFrom(string table) => AppendCommand("DELETE FROM", table);

    /// <summary>
    /// Appends a 'DELETE FROM' command to the query builder.
    /// </summary>
    /// <param name="schema">The database schema where the table is.</param>
    /// <param name="table">The name of the table whose data will be deleted.</param>
    /// <returns></returns>
    public IWhere DeleteFrom(string schema, string table) => AppendCommand("DELETE FROM", schema, table);

    /// <summary>
    /// Appends a 'INSERT INTO' command to the query builder.
    /// </summary>
    /// <param name="table">The name of the table where data will be inserted.</param>
    /// <returns></returns>
    public IValues InsertInto(string table) => AppendCommand("INSERT INTO", table);

    /// <summary>
    /// Appends a 'INSERT INTO' command to the query builder.
    /// </summary>
    /// <param name="schema">The database schema where the table is.</param>
    /// <param name="table">The name of the table where data will be inserted.</param>
    /// <returns></returns>
    public IValues InsertInto(string schema, string table) => AppendCommand("INSERT INTO", schema, table);

    /// <summary>
    /// Appends a 'INSERT INTO' command to the query builder.
    /// </summary>
    /// <param name="schema">The database schema where the table is.</param>
    /// <param name="table">The name of the table where data will be inserted.</param>
    /// <param name="columns">String array containing the columns names.</param>
    /// <returns></returns>
    public IValues InsertInto(string schema, string table, string[] columns)
    {
        ArrayValidations.ItsNotEmpty(columns, nameof(columns));

        AppendCommand("INSERT INTO", schema, table)
            ._sb.Append(" (")
            .AppendJoin(", ", columns)
            .Append(')');

        return this;
    }

    /// <summary>
    /// Appends a 'INSERT INTO' command to the query builder.
    /// </summary>
    /// <param name="table">The name of the table where data will be inserted.</param>
    /// <param name="columns">String array containing the columns names.</param>
    /// <returns></returns>
    public IValues InsertInto(string table, string[] columns)
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