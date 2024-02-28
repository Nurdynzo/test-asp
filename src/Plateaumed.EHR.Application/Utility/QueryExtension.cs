using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Plateaumed.EHR.ValueObjects;

namespace Plateaumed.EHR.Utility;

public static class QueryExtension
{
    public static Money Sum<T>(this IEnumerable<T> source, Expression<Func<T, Money>> selector)
    {
        if (source == null || !source.Any())
        {
            return new Money();
        }
        var compiledSelector = selector.Compile();
        return source
            .Select(item => item != null ? compiledSelector(item) : new Money())
            .Aggregate(new Money(), (current, item) => current + item);
    }
    
    public static Money Sum(this IEnumerable<Money> source)
    {
        if (source == null || !source.Any())
        {
            return new Money();
        }
        var total = new Money();
        return source.Aggregate(total, (current, item) => current + item);
    }
  
}
