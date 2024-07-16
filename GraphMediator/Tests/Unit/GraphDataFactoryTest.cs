using Engine.ResultRecords;
using ExtensionMethods.Probability.Quantities;
using GraphEngine.Graph;
using GraphEngine.Visitors;
using GraphMediator.GraphEngineMediator;
using GraphMediator.GraphEngineMediator.Data;
using GraphMediator.Stubs;
using GraphMediator.Tests.Utility;
using Xunit;
using static GraphMediator.Tests.Utility.TestCompleteList;
using static GraphMediator.GraphEngineMediator.Data.ChildJournalGraphSpec;

namespace GraphMediator.Tests.Unit {
    public class GraphDataFactoryTest {
        [Fact]
        public void ReferenceDataTest() {
            var dump = Dump(MultipleExaminations, new DateTime(2005, 04, 22));
            Assert.Equal(new Axis("Age", 0.Years(), 5.Years(), 1.Years()),
                dump.GraphDataDTO.XAxis);
        }
        [Fact]
        public void NoExaminationData()
        {
            var dump = Dump(NoExaminations, new DateTime(2005, 04, 22));
            Assert.Equal(new Axis("Age", 0.Months(), 5.Months(), 1.Months()),
                dump.GraphDataDTO.XAxis);
            Assert.Equal(new Axis("Weight", 2.kg(), 9.kg(), 1.kg()),
                dump.GraphDataDTO.YAxis);
        }

        [Fact]
        public void OneRecordNoReferenceData()
        {
            var dump = Dump(SingleExamination(), new DateTime(1995, 04, 22));
            Assert.Equal(new Axis("Age", 9.Years()+9.Months(), 10.Years() + 3.Months(), 1.Months()),
                dump.GraphDataDTO.XAxis);
        }
        [Fact]
        public void LessThan2Sigma()
        {
            var dump = Dump(SingleExamination(0.9), new DateTime(2005, 04, 22));
            Assert.Equal(new Axis("Age", 0.Months(), 5.Months(), 1.Months()),
                dump.GraphDataDTO.XAxis);
            Assert.Equal(new Axis("Weight", 0.kg(), 9.kg(), 1.kg()),
                dump.GraphDataDTO.YAxis);
        }
        [Fact]
        public void GreaterThan2Sigma()
        {
            var dump = Dump(SingleExamination(10.2), new DateTime(2005, 05, 22));
            Assert.Equal(new Axis("Age", 0.Months(), 5.Months(), 1.Months()),
                dump.GraphDataDTO.XAxis);
            Assert.Equal(new Axis("Weight", 2.kg(), 11.kg(), 1.kg()),
                dump.GraphDataDTO.YAxis);
        }
        private GraphDataDump Dump(CompleteList list,DateTime birthDate)
        {
            var graphData = new GraphFactory(
               AgeWeight,
               list,
               ChildJournalColumns.Age,
               ChildJournalColumns.Weight,
               WhoReference.Records(AgeWeight, Gender.Female),
               birthDate,
               12
               ).GraphData();
           return new GraphDataDump(graphData);
        }
    }
}