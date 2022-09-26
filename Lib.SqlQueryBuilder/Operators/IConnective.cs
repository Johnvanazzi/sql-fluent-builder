using System.Text;

namespace Lib.QueryBuilder.Operators;

public interface IConnective
{
    public string ToSql();
    public StringBuilder Sb { get; }
    public IComparer And();
    public IComparer Or();
}