using SqlFluentBuilder.Operators;

namespace SqlFluentBuilder.Clauses;

public interface IOn : IWhere
{
    public IJoin On(string leftTable, string leftKey, string rightTable, string rightKey);
    public IJoin On(string leftTable, string leftKey, string rightTable,string rightKey, Connective connective, 
        IConnective condition);
}