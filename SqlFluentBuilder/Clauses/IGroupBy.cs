﻿namespace SqlFluentBuilder.Clauses;

public interface IGroupBy : IOrderBy
{
    public IHaving GroupBy(params string[] columns);
}