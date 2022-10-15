namespace SqlFluentBuilder.Clauses;

public interface ISet : IQuery
{
    public IWhere Set(Dictionary<string, object?> columnsValues);
}