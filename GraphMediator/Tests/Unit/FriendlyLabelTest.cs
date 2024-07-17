using ExtensionMethods.Probability.Quantities;
using GraphEngine.Graph;
using GraphEngine.Quantities;
using GraphMediator.GraphEngineMediator;
using Xunit;
using static GraphEngine.Quantities.Extensions.RatioQuantityExtensions;
using static GraphEngine.Quantities.RatioQuantity;

namespace GraphMediator.Tests.Unit;

// Understands a logical user-viewable representation of a RatioQuantity
public class FriendlyLabelTest {

    [Fact]
    public void SimpleLabel() {
        AssertLabels(["6"], [6.cm()], HeightFormatter.Instance);
        AssertLabels(["6"], [6.mm()], HeadCircumferenceFormatter.Instance);
        AssertLabels(["6"], [6.kg()], WeightFormatter.Instance);
        AssertLabels(["6"], [6.BMI()], BmiFormatter.Instance);
    }
    
    [Fact]
    public void AgeLabels() {
        AssertLabels(["1w"], [1.Weeks()], AgeFormatter.Instance);
        AssertLabels(["20w"], [20.Weeks()], AgeFormatter.Instance);
        AssertLabels(["6m"], [6.Months()], AgeFormatter.Instance);
        AssertLabels(["12m"], [12.Months()], AgeFormatter.Instance);
        AssertLabels(["2y"], [2.Years()], AgeFormatter.Instance);
        AssertLabels(["10y6m"], [10.5.Years()], AgeFormatter.Instance);
        AssertLabels(
            ["0m", "1m", "2m", "3m", "4m", "5m"], 
            [0.Months(), 1.Months(), 2.Months(), 3.Months(), 4.Months(), 5.Months()], 
            AgeFormatter.Instance);
        AssertLabels(
            ["9y", "9y3m", "9y6m", "9y9m", "10y", "10y3m", "10y6m"], 
            [9.Years(), 9.25.Years(), 9.5.Years(), 9.75.Years(), 10.Years(), 10.25.Years(), 10.5.Years()], 
            AgeFormatter.Instance);
    }

    [Fact]
    public void AxisLabel() {
        AssertLabels(
            ["9y", "9y3m", "9y6m", "9y9m", "10y", "10y3m", "10y6m"],
            new Axis("Age", 9.Years(), 10.5.Years(), 0.25.Years()),
            AgeFormatter.Instance);
        AssertLabels(
            ["7y", "8y", "9y", "10y", "11y", "12y", "13y", "14y", "15y"],
            new Axis("Age", 7.Years(), 15.Years(), 1.Years()),
            AgeFormatter.Instance);
        AssertLabels(
            ["3w", "4w", "5w", "6w", "7w", "8w", "9w", "10w"],
            new Axis("Age", 3.Weeks(), 10.Weeks(), 1.Weeks()),
            AgeFormatter.Instance);
        AssertLabels(
            ["6m", "7m", "8m", "9m", "10m", "11m", "12m"],
            new Axis("Age", 6.Months(), 12.Months(), 1.Months()),
            AgeFormatter.Instance);
    }
    
    private void AssertLabels(List<string> expected, List<RatioQuantity> quantities, FriendlyFormatter formatter) {
        Assert.Equal(expected, quantities.Format(formatter).Select(tuple => tuple.Item2));
    }
    
    private void AssertLabels(List<string> expected, Axis axis, FriendlyFormatter formatter) {
        Assert.Equal(expected, axis.Labels(formatter).Select(tuple => tuple.Item2));
    }
}