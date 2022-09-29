namespace SqlFluentBuilder.Clauses;

public interface IInsertInto
{
    public IValues InsertInto(string schema, string table, string[] columns);
    public IValues InsertInto(string schema, string table);
    public IValues InsertInto(string table, string[] columns);
    public IValues InsertInto(string table);
}