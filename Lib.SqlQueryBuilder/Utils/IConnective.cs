using System.Text;

namespace Lib.QueryBuilder.Utils;

public interface IConnective
{
    public string ToSql();
    public StringBuilder Sb { get; }
    public IComparer And();
    public IComparer Or();
}