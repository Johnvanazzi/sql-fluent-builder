using Lib.QueryBuilder.Verbs;

namespace Lib.QueryBuilder;

public interface ICommand : ISelect, IDelete, IUpdate, IInsert 
{
    
}