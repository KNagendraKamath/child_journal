using Engine.ResultRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphMediator.GraphEngineMediator;
using static GraphMediator.GraphEngineMediator.ChildJournalColumns;

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


