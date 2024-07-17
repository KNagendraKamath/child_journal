using Engine.ResultRecords;
using System.Linq;
using static GraphMediator.GraphEngineMediator.ChildJournalColumns;
using System.Collections.Generic;


namespace GraphMediator.Tests.Utility
{
    public static class TestCompleteList
    {
        public static CompleteList MultipleExaminations = new CompleteList( new List<ResultRecord>() {
            Record((ExaminationDate,"22-04-2005"),(Weight,2.5)),
            Record((ExaminationDate,"20-05-2005"),(Weight,3.67)),
            Record((ExaminationDate,"15-07-2005"),(Weight,5.3)),
            Record((ExaminationDate,"21-09-2005"),(Weight,6.32)),
            Record((ExaminationDate,"22-04-2006"),(Weight,8.43)),
            Record((ExaminationDate,"22-08-2006"),(Weight,9.65)),
            Record((ExaminationDate,"22-04-2007"),(Weight,10.29)),
            Record((ExaminationDate,"22-08-2007"),(Weight,10.5)),
            Record((ExaminationDate,"21-04-2008"),(Weight,11.84)),
            Record((ExaminationDate,"22-07-2008"),(Weight,12.56)),
            Record((ExaminationDate,"22-04-2009"),(Weight,13.58)),
            Record((ExaminationDate,"21-09-2009"),(Weight,15.6)),
            Record((ExaminationDate,"22-04-2010"),(Weight,14.89)),
        });
        public static CompleteList SingleExamination(double weight = 2.5) => new CompleteList( new List<ResultRecord>() {
           Record((ExaminationDate, "22-04-2005"), (Weight, weight)),
        });
        public static CompleteList NoExaminations = new CompleteList(new List<ResultRecord> { });
        private static ResultRecord Record(params (Column column, object value)[] fieldValues)
        {
            return new ResultRecord(fieldValues.ToDictionary(x => x.column.ToString(), x => x.value));
        }
    }
}
