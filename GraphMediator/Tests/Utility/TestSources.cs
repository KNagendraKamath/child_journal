using Engine.ResultRecords;
using static GraphMediator.GraphEngineMediator.ChildJournalColumns;
using static GraphMediator.GraphEngineMediator.WhoReference;

namespace GraphMediator.Tests.Utility;
internal class TestSources
{
    internal static Dictionary<Column, List<ReferenceRecord>> ReferenceData = new Dictionary<Column, List<ReferenceRecord>>
    {
        { Weight, new List<ReferenceRecord> {  } },
        { Height, new List<ReferenceRecord> {  } },
        { HeadCircumference, new List<ReferenceRecord> {  } },
        { BMI, new List<ReferenceRecord> {  } }
    };

    internal static CompleteList ExaminationData = new CompleteList([]);
}


