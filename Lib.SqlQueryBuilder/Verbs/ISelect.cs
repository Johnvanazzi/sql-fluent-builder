﻿using Lib.QueryBuilder.Clauses;

namespace Lib.QueryBuilder.Verbs;

public interface ISelect
{
    public IFrom Select();
}