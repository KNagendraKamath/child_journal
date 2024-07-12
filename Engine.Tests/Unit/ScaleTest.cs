using ExtensionMethods.Probability.Quantities;
using GraphEngine.Graph;
using GraphEngine.Quantities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static GraphEngine.Graph.Dimension;
using static GraphEngine.Quantities.Unit;

namespace GraphEngine.Tests.Unit {
    public class ScaleTest {
        private static readonly Dimension d_cm = new(
            "TestDimension",
            Centimeter,
            new List<RatioQuantity> { 1.cm(), 5.cm(), 10.cm(), 20.cm(), 50.cm() });

        [Fact]
        public void NoRoundingScale() {
            Assert.Equal(
                new Scale("TestDimension", 50.cm(), 70.cm(), 5.cm()),
                d_cm.Axis(50.cm(), 70.cm(), 10));
            Assert.Equal(
                new Scale("TestDimension", 50.cm(), 70.cm(), 5.cm()),
                d_cm.Axis(52.5.cm(), 67.8.cm(), 10));
            ;
        }
    }
}