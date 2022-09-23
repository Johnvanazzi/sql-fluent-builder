﻿namespace Lib.QueryBuilder.Clauses;

public interface IInsert
{
    public IValues Insert(string schema, string table, string[] columns);
    public IValues Insert(string schema, string table);
    public IValues Insert(string table, string[] columns);
    public IValues Insert(string table);
}