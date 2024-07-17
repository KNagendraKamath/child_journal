using ExtensionMethods.Probability.Quantities;
using GraphEngine.Graph;
using GraphEngine.Quantities;
using GraphMediator.GraphEngineMediator;
using System.Collections.Generic;
using Xunit;
using static GraphEngine.Quantities.Extensions.RatioQuantityExtensions;
using static GraphEngine.Quantities.RatioQuantity;

namespace GraphMediator.Tests.Unit
{
    // Understands a logical user-viewable representation of a RatioQuantity
    public class FriendlyLabelTest {

        [Fact]
        public void SimpleLabel() {
            AssertLabels(new List<string> { "6" }, new List<RatioQuantity> { 6.cm() }, HeightFormatter.Instance);
            AssertLabels(new List<string> { "6" }, new List<RatioQuantity> { 6.mm() }, HeadCircumferenceFormatter.Instance);
            AssertLabels(new List<string> { "6" }, new List<RatioQuantity> { 6.kg() }, WeightFormatter.Instance);
            AssertLabels(new List<string> { "6" }, new List<RatioQuantity> { 6.BMI() }, BmiFormatter.Instance);
        }
    
        [Fact]
        public void AgeLabels() {
            AssertLabels(new List<string> { "1w" }, new List<RatioQuantity> { 1.Weeks() }, AgeFormatter.Instance);
            AssertLabels(new List<string> { "20w" }, new List<RatioQuantity> { 20.Weeks() }, AgeFormatter.Instance);
            AssertLabels(new List<string> { "6m" }, new List<RatioQuantity> { 6.Months() }, AgeFormatter.Instance);
            AssertLabels(new List<string> { "12m" }, new List<RatioQuantity> { 12.Months() }, AgeFormatter.Instance);
            AssertLabels(new List<string> { "2y" }, new List<RatioQuantity> { 2.Years() }, AgeFormatter.Instance);
            AssertLabels(new List<string> { "10y6m" }, new List<RatioQuantity> { 10.5.Years() }, AgeFormatter.Instance);
            AssertLabels(
                new List<string> { "0m", "1m", "2m", "3m", "4m", "5m" },
                new List<RatioQuantity> { 0.Months(), 1.Months(), 2.Months(), 3.Months(), 4.Months(), 5.Months() }, 
                AgeFormatter.Instance);
            AssertLabels(
                new List<string> { "9y", "9y3m", "9y6m", "9y9m", "10y", "10y3m", "10y6m" },
                new List<RatioQuantity> { 9.Years(), 9.25.Years(), 9.5.Years(), 9.75.Years(), 10.Years(), 10.25.Years(), 10.5.Years() }, 
                AgeFormatter.Instance);
        }
    
        private void AssertLabels(List<string> expected, List<RatioQuantity> quantities, FriendlyFormatter formatter) {
            Assert.Equal(expected, quantities.Format(formatter));
        }
    }
}