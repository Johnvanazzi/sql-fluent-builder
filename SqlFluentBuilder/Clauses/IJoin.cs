namespace SqlFluentBuilder.Clauses;

public interface IJoin : IWhere, IGroupBy
{
    public IOn LeftJoin(string table);
    public IOn LeftJoin(string schema,string table);
    public IOn RightJoin(string table);
    public IOn RightJoin(string schema,string table);
    public IOn CrossJoin(string table);
    public IOn CrossJoin(string schema,string table);
    public IOn OuterJoin(string table);
    public IOn OuterJoin(string schema,string table);
    public IOn InnerJoin(string table);
    public IOn InnerJoin(string schema,string table);
}