namespace Lib.QueryBuilder.Operators;

public static class Aggregator
{
    /// <summary>
    /// Creates the string representing SQL average function.
    /// </summary>
    /// <returns>The string with average function applied to all table columns</returns>
    public static string Avg() => "AVG(*)";

    /// <summary>
    /// Creates the string representing SQL average function.
    /// </summary>
    /// <param name="column">The column whose average will be taken.</param>
    /// <returns>The string with average function applied to one column.</returns>
    public static string Avg(string column) => $"AVG({column})";

    /// <summary>
    /// Creates the string representing SQL count function.
    /// </summary>
    /// <returns>The string with count function applied to all table columns.</returns>
    public static string Count() => "COUNT(*)";

    /// <summary>
    /// Creates the string representing SQL count function.
    /// </summary>
    /// <param name="column">The column whose count will be taken.</param>
    /// <returns>The string with count function applied to one column.</returns>
    public static string Count(string column) => $"COUNT({column})";

    /// <summary>
    /// Creates the string representing SQL max function.
    /// </summary>
    /// <returns>The string with max function applied to all table columns.</returns>
    public static string Max() => "MAX(*)";

    /// <summary>
    /// Creates the string representing SQL maximum function.
    /// </summary>
    /// <param name="column">The column in which maximum function will act.</param>
    /// <returns>The string with maximum function applied one column.</returns>
    public static string Max(string column) => $"MAX({column})";

    /// <summary>
    /// Creates the string representing SQL minimum function.
    /// </summary>
    /// <returns>The string with minimum function applied to all table columns.</returns>
    public static string Min() => "MIN(*)";

    /// <summary>
    /// Creates the string representing SQL minimum function.
    /// </summary>
    /// <param name="column">The column which minimum function will act.</param>
    /// <returns>The string with minimum function applied to one column.</returns>
    public static string Min(string column) => $"MIN({column})";

    /// <summary>
    /// Creates the string representing SQL sum function.
    /// </summary>
    /// <returns>The string with sum function applied to all table columns.</returns>
    public static string Sum() => "SUM(*)";

    /// <summary>
    /// Creates the string representing SQL sum function.
    /// </summary>
    /// <param name="column">The column which sum function will act.</param>
    /// <returns>The string with sum function applied to one column.</returns>
    public static string Sum(string column) => $"SUM({column})";
}