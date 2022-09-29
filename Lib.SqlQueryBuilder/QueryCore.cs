using System.Text;

namespace Lib.QueryBuilder;

public partial class Query
{
    private readonly StringBuilder _sb;

    public Query()
    {
        _sb = new StringBuilder();
    }

    /// <summary>
    /// Creates the final string representation of the SQL statements and clauses used on the builder. It does not clear the builder.
    /// Therefore, the builder can be modified afterwards.
    /// </summary>
    /// <returns>The final string representing the SQL query.</returns>
    public string ToSql() => _sb.ToString();
    
    /// <summary>
    /// Clears the memory of the string builder. All previously SQL statements and clauses added to the builder will be cleared off.
    /// After called, the builder is ready to be reused. 
    /// </summary>
    public void Clear() => _sb.Clear();
}