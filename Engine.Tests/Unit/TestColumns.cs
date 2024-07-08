using Engine.ResultRecords;
using System;
using static Engine.ResultRecords.Column;

namespace GraphEngine.Tests.Unit;

internal static class TestColumns
{
   

    internal static readonly Column DateOfExamination = new(
        nameof(DateOfExamination),
        (column, value) => throw new ArgumentException("Not a valid column to search"));

    internal static readonly Column Weight = new(
        nameof(Weight),
        (column, value) => throw new ArgumentException("Not a valid column to search"));

    internal static readonly Column DateColumn = new(
        nameof(DateColumn),
        (column, value) => new Criteria.DateRangeCriterion(column, (DateRange)value),
        DateRange);


}
