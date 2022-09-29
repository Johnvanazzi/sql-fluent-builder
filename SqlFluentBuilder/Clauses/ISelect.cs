namespace SqlFluentBuilder.Clauses;

public interface ISelect : IQuery
{
    public IFrom Select();
    public IFrom Select(params string[] columns);
}