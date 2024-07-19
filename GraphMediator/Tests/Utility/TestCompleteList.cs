using Engine.ResultRecords;
using static GraphMediator.GraphEngineMediator.ChildJournalColumns;

namespace GraphMediator.Tests.Utility
{
    public static class TestCompleteList
    {
        public static CompleteList MultipleExaminations = new([
            Record((ExaminationDate,"22-04-2005"),(Weight,2.5),(Height,50.3)),
            Record((ExaminationDate,"20-05-2005"),(Weight,3.67),(Height,58)),
            Record((ExaminationDate,"15-07-2005"),(Weight,5.3),(Height,60.0)),
            Record((ExaminationDate,"21-09-2005"),(Weight,6.32),(Height,70.0)),
            Record((ExaminationDate,"22-04-2006"),(Weight,8.43),(Height,80.0)),
            Record((ExaminationDate,"22-08-2006"),(Weight,9.65),(Height,83.0)),
            Record((ExaminationDate,"22-04-2007"),(Weight,10.29),(Height,84.0)),
            Record((ExaminationDate,"22-08-2007"),(Weight,10.5),(Height,85.0)),
            Record((ExaminationDate,"21-04-2008"),(Weight,11.84),(Height,86.0)),
            Record((ExaminationDate,"22-07-2008"),(Weight,12.56),(Height,88.0)),
            Record((ExaminationDate,"22-04-2009"),(Weight,13.58),(Height,90.0)),
            Record((ExaminationDate,"21-09-2009"),(Weight,15.6),(Height,95.0)),
            Record((ExaminationDate,"22-03-2010"),(Weight,14.89),(Height,99.7)),
        ]);
        public static CompleteList NewBornExaminations = new([
            Record((ExaminationDate,"22-04-2005"),(Weight,2.5),(Height,50.3)),
            Record((ExaminationDate,"20-05-2005"),(Weight,3.67),(Height,58)),
            Record((ExaminationDate,"15-07-2005"),(Weight,5.3),(Height,60.0)),
        ]);
        public static CompleteList SingleExamination(double weight = 2.5) => new([
           Record((ExaminationDate, "22-04-2005"), (Weight, weight)),
        ]);
        public static CompleteList NoExaminations = new([]);
        private static ResultRecord Record(params (Column column, object value)[] fieldValues)
        {
            return new ResultRecord(fieldValues.ToDictionary(x => x.column.ToString(), x => x.value));
        }
    }
}
