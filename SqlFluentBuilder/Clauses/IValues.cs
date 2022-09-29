namespace SqlFluentBuilder.Clauses;

public interface IValues : ISelect
{
    public IQuery Values(object?[] values);
    public IQuery Values(object?[][] values);
}