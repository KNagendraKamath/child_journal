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
            "cm Test",
            Centimeter,
            new List<RatioQuantity> { 1.cm(), 5.cm(), 10.cm(), 20.cm(), 50.cm() });
        
        private static readonly Dimension d_age = new(
            "age Test",
            Year,
            new List<RatioQuantity> { 1.Weeks(), 2.Weeks(), 1.Months(), 0.25.Years(), 1.Years(), 2.Years(), 5.Years()});

        [Fact]
        public void SimpleMetricsScale() {
            Assert.Equal(
                new Scale("cm Test", 50.cm(), 70.cm(), 5.cm()),
                d_cm.Axis(50.cm(), 70.cm(), 10));
            Assert.Equal(
                new Scale("cm Test", 50.cm(), 70.cm(), 5.cm()),
                d_cm.Axis(52.5.cm(), 67.8.cm(), 10));
            ;
        }
        [Fact]
        public void Age()
        {
            Assert.Equal(
                new Scale("age Test", 0.Weeks(), 14.Weeks(),2.Weeks()),
                d_age.Axis(0.Weeks(),13.Weeks(), 12));
            Assert.Equal(
                new Scale("age Test", 0.Weeks(), 14.Weeks(), 2.Weeks()),
                d_age.Axis(0.Years(), 0.2493.Years(), 12));
            Assert.Equal(
                new Scale("age Test", 0.Weeks(), 12.Months(), 1.Months()),
                d_age.Axis(0.Years(), 1.Years(), 12));
            Assert.Equal(
               new Scale("age Test", 18.Months(), 24.Months(), 1.Months()),
               d_age.Axis(1.5.Years(), 2.Years(), 12));
            Assert.Equal(
               new Scale("age Test", 0.Weeks(), 9.Weeks(), 1.Weeks()),
               d_age.Axis(0.Months(), 2.Months(), 12));
            Assert.Equal(
               new Scale("age Test", 0.Years(), 2.Years(), 0.25.Years()),
               d_age.Axis(0.Years(), 2.Years(), 12));
            Assert.Equal(
               new Scale("age Test", 0.Years(), 3.Years(), 0.25.Years()),
               d_age.Axis(0.Years(), 3.Years(), 12));
            Assert.Equal(
               new Scale("age Test", 3.Years()+10.Months(), 4.Years(), 1.Weeks()),
               d_age.Axis(3.Years()+10.Months(), 4.Years(), 12));


        }
    }
}