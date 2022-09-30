using SqlFluentBuilder.Operators;

namespace SqlFluentBuilder.Clauses;

public interface IHaving : IOrderBy
{
    public IOrderBy Having(IConnective condition);
    public IOrderBy Having(Func<Condition, IConnective> condition);
}