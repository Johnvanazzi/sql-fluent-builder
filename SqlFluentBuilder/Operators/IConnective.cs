using System.Text;

namespace SqlFluentBuilder.Operators;

public interface IConnective
{
    public string ToSql();
    public StringBuilder Sb { get; }
    public IComparer And { get; }
    public IComparer Or { get; }
}