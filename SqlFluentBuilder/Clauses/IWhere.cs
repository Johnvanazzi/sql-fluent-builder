using SqlFluentBuilder.Operators;

namespace SqlFluentBuilder.Clauses;

public interface IWhere : IUnion
{
    public IGroupBy Where(IConnective condition);
    public IGroupBy Where(Func<Condition, IConnective> condition);
}