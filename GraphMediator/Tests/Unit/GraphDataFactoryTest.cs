using GraphEngine.Graph;
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
                TestCompleteList.completeList,
                ChildJournalColumns.Age,
                ChildJournalColumns.Weight,
                WhoReference.Records(AgeWeight, Gender.Female),
                new DateTime(2005, 05, 01),
                12).GraphData(); 
        }
    }
}