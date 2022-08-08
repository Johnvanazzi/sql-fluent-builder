namespace Lib.QueryBuilder.Operators;

public static class Aggregator
{
    public static string Avg() => "AVG(*)";
    public static string Avg(string column) => $"AVG({column})";
    public static string Count() => "COUNT(*)";
    public static string Count(string column) => $"COUNT({column})";
    public static string Max() => "MAX(*)";
    public static string Max(string column) => $"MAX({column})";
    public static string Min() => "MIN(*)";
    public static string Min(string column) => $"MIN({column})";
    public static string Sum() => "SUM(*)";
    public static string Sum(string column) => $"SUM({column})";
}