using System.Text;

namespace Lib.QueryBuilder.Utils;

public interface IConnective
{
    public StringBuilder Sb { get; }
    public IComparer And();
    public IComparer Or();
}