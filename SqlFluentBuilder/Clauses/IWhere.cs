using SqlFluentBuilder.Operators;

namespace SqlFluentBuilder.Clauses;

public interface IWhere : IUnion, IQuery
{
    public IGroupBy Where(IConnective condition);
}