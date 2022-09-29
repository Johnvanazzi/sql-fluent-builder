namespace SqlFluentBuilder.Clauses;

public interface IOrderBy : IUnion
{
    public IUnion OrderBy(params string[] columns);
}