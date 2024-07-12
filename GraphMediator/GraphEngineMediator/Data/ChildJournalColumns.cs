using Engine.ResultRecords;
using System;
using static Engine.ResultRecords.Column;

namespace GraphMediator.GraphEngineMediator;

internal static class ChildJournalColumns
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

    internal static readonly Column HeadCircumference = new(
        nameof(HeadCircumference),
        (column, value) => throw new ArgumentException("Not a valid column to search"),
        Double);

    internal static readonly Column BMI = new(
        nameof(BMI),
        (column, value) => throw new ArgumentException("Not a valid column to search"),
        Double);

    internal static readonly Column DateColumn = new(
        nameof(DateColumn),
        (column, value) => new Criteria.DateRangeCriterion(column, (DateRange)value),
        DateRange);

    internal static readonly Column ExaminationDate = new(
        nameof(ExaminationDate),
        (column, value) => throw new ArgumentException("Not a valid column to search"),
        DateRange);
}
