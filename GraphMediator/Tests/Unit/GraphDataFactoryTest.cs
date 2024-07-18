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
using System;

namespace GraphMediator.Tests.Unit {
    public class GraphDataFactoryTest {
        [Fact]
        public void ReferenceDataTest() { // Examination from 0y to 5y
            var dump = AgeWeightDump(MultipleExaminations, new DateTime(2005, 04, 22));
            Assert.Equal(new Axis("Age", 0.Years(), 5.Years(), 1.Years()),
                dump.GraphDataDTO.XAxis);
        }

        [Fact]
        public void MiniGraph() { // Examination from 10y to 15y
            var dump = AgeWeightDump(MultipleExaminations, new DateTime(1994, 04, 22), 5);
            Assert.Equal(new Axis("Age", 11.Years(), 16.Years(), 1.Years()),
                dump.GraphDataDTO.XAxis);
        }

        [Fact]
        public void MiniGraphHalfYear() { // Examinations from 9.5y to 14.5y
            var dump = AgeWeightDump(MultipleExaminations, new DateTime(1995, 10, 22), 5);
            Assert.Equal(new Axis("Age", 9.Years(), 15.Years(), 1.Years()),
                dump.GraphDataDTO.XAxis);
        }

        [Fact]
        public void NoExaminationData() { // Examination from 0y to 5 y
            var dump = AgeWeightDump(NoExaminations, new DateTime(2005, 04, 22));
            Assert.Equal(new Axis("Age", 0.Months(), 5.Months(), 1.Months()),
                dump.GraphDataDTO.XAxis);
            Assert.Equal(new Axis("Weight", 2.kg(), 9.kg(), 1.kg()),
                dump.GraphDataDTO.YAxis);
        }

        [Fact]
        public void OneRecordNoReferenceData() { // Examination only on age 10u
            var dump = AgeWeightDump(SingleExamination(), new DateTime(1995, 04, 22));
            Assert.Equal(new Axis("Age", 9.Years() + 9.Months(), 10.Years() + 3.Months(), 1.Months()),
                dump.GraphDataDTO.XAxis);
        }

        [Fact]
        public void LessThan2Sigma() { // Examination only on 0y
            var dump = AgeWeightDump(SingleExamination(0.9), new DateTime(2005, 04, 22));
            Assert.Equal(new Axis("Age", 0.Months(), 5.Months(), 1.Months()),
                dump.GraphDataDTO.XAxis);
            Assert.Equal(new Axis("Weight", 0.kg(), 9.kg(), 1.kg()),
                dump.GraphDataDTO.YAxis);
        }

        [Fact]
        public void GreaterThan2Sigma() { // Examination only on 0y
            var dump = AgeWeightDump(SingleExamination(10.2), new DateTime(2005, 05, 22));
            Assert.Equal(new Axis("Age", 0.Months(), 5.Months(), 1.Months()),
                dump.GraphDataDTO.XAxis);
            Assert.Equal(new Axis("Weight", 2.kg(), 11.kg(), 1.kg()),
                dump.GraphDataDTO.YAxis);
        }

        [Fact]
        public void ReferenceHeightWeight() { // Examination from 0y to 5y
            var dump = HeightWeightDump(MultipleExaminations, new DateTime(2005, 04, 22));
            Assert.Equal(new Axis("Height", 50.cm(), 100.cm(), 5.cm()),
                dump.GraphDataDTO.XAxis);
            Assert.Equal(new Axis("Weight", 0.kg(), 25.kg(), 5.kg()),
                dump.GraphDataDTO.YAxis);
        }

        private GraphDataDump AgeWeightDump(CompleteList list, DateTime birthDate, int maxStepCount = 12) {
            var graphData = new GraphFactory(
                AgeWeight,
                list,
                ChildJournalColumns.Age,
                ChildJournalColumns.Weight,
                WhoReference.Records(AgeWeight, Gender.Female),
                birthDate,
                maxStepCount
            ).GraphData();
            return new GraphDataDump(graphData);
        }

        private GraphDataDump HeightWeightDump(CompleteList list, DateTime birthDate) {
            var graphData = new GraphFactory(
                HeightWeight,
                list,
                ChildJournalColumns.Height,
                ChildJournalColumns.Weight,
                WhoReference.Records(HeightWeight, Gender.Female),
                birthDate,
                12
            ).GraphData();
            return new GraphDataDump(graphData);
        }
    }
}