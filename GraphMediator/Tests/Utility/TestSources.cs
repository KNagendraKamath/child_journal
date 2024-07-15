using Engine.ResultRecords;
using static GraphMediator.GraphEngineMediator.ChildJournalColumns;
using static GraphMediator.GraphEngineMediator.WhoReference;

namespace GraphMediator.Tests.Utility;
internal class TestSources
{
    internal static Dictionary<Column, List<RawReferenceRecord>> ReferenceData = new Dictionary<Column, List<RawReferenceRecord>>
    {
        { Weight, new List<RawReferenceRecord> {  } },
        { Height, new List<RawReferenceRecord> {  } },
        { HeadCircumference, new List<RawReferenceRecord> {  } },
        { BMI, new List<RawReferenceRecord> {  } }
    };

    internal static CompleteList ExaminationData = new CompleteList([]);
}


