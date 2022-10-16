namespace SqlFluentBuilder.Clauses;

public interface IUpdate
{
    public ISet Update(string table);
    public ISet Update(string schema, string table);
}