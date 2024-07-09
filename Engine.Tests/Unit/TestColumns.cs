using Engine.ResultRecords;
using System;
using static Engine.ResultRecords.Column;

namespace GraphEngine.Tests.Unit;

internal static class TestColumns
{
   
    internal static readonly Column Age = new(
        nameof(Age),
        (column, value) => throw new ArgumentException("Not a valid column to search"),
        Double);

    internal static readonly Column Weight = new(
        nameof(Weight),
        (column, value) => throw new ArgumentException("Not a valid column to search"),
         Double);

    internal static readonly Column Height = new(
        nameof(Height),
        (column, value) => throw new ArgumentException("Not a valid column to search"), 
        Double);

    internal static readonly Column DateColumn = new(
        nameof(DateColumn),
        (column, value) => new Criteria.DateRangeCriterion(column, (DateRange)value),
        DateRange);
}
