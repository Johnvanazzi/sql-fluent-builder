namespace SqlFluentBuilder.Clauses;

public interface IInto
{
    public IFrom Into(string newTable);
    public IFrom Into(string schema, string newTable);
    public IFrom IntoIn(string newTable, string externalDb);
    public IFrom IntoIn(string schema, string newTable, string externalDb);
}