using ExtensionMethods.Probability.Quantities;
using GraphEngine.Graph;
using GraphEngine.Visitors;
using GraphMediator.GraphEngineMediator;
using GraphMediator.GraphEngineMediator.Data;
using GraphMediator.Stubs;
using GraphMediator.Tests.Utility;
using Xunit;
using static GraphMediator.GraphEngineMediator.Data.ChildJournalGraphSpec;

namespace GraphMediator.Tests.Unit {
    public class GraphDataFactoryTest {
        [Fact]
        public void ReferenceDataTest() {
            var graphData = new GraphFactory(
                AgeWeight,
                TestCompleteList.MultipleExaminations,
                ChildJournalColumns.Age,
                ChildJournalColumns.Weight,
                WhoReference.Records(AgeWeight, Gender.Female),
                new DateTime(2005, 04, 22),
                12).GraphData();
            var dump = new GraphDataDump(graphData);
            Assert.Equal(new Axis("Age", 0.Years(), 5.Years(), 1.Years()),
                dump.GraphDataDTO.XAxis);
        }
        [Fact]
        public void NoExaminationData()
        {
            var graphData = new GraphFactory(
                AgeWeight,
                TestCompleteList.NoExaminations,
                ChildJournalColumns.Age,
                ChildJournalColumns.Weight,
                WhoReference.Records(AgeWeight, Gender.Female),
                new DateTime(2005, 04, 22),
                12).GraphData();
            var dump = new GraphDataDump(graphData);
            Assert.Equal(new Axis("Age", 0.Months(), 5.Months(), 1.Months()),
                dump.GraphDataDTO.XAxis);
            Assert.Equal(new Axis("Weight", 2.kg(), 9.kg(), 1.kg()),
                dump.GraphDataDTO.YAxis);
        }

        [Fact]
        public void OneRecordNoReferenceData()
        {
            var graphData = new GraphFactory(
                AgeWeight,
                TestCompleteList.SingleExamination(),
                ChildJournalColumns.Age,
                ChildJournalColumns.Weight,
                WhoReference.Records(AgeWeight, Gender.Female),
                new DateTime(1995, 04, 22),
                12).GraphData();
            var dump = new GraphDataDump(graphData);
            Assert.Equal(new Axis("Age", 9.Years()+9.Months(), 10.Years() + 3.Months(), 1.Months()),
                dump.GraphDataDTO.XAxis);
        }
        [Fact]
        public void LessThan2Sigma()
        {
            var graphData = new GraphFactory(
                AgeWeight,
                TestCompleteList.SingleExamination(0.9),
                ChildJournalColumns.Age,
                ChildJournalColumns.Weight,
                WhoReference.Records(AgeWeight, Gender.Female),
                new DateTime(2005, 04, 22),
                12).GraphData();
            var dump = new GraphDataDump(graphData);
            Assert.Equal(new Axis("Age", 0.Months(), 5.Months(), 1.Months()),
                dump.GraphDataDTO.XAxis);
            Assert.Equal(new Axis("Weight", 0.kg(), 9.kg(), 1.kg()),
                dump.GraphDataDTO.YAxis);
        }
    }
}