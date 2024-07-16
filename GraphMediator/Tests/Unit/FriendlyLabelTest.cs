using ExtensionMethods.Probability.Quantities;
using GraphMediator.GraphEngineMediator;
using Xunit;

namespace GraphMediator.Tests.Unit;

// Understands a logical user-viewable representation of a RatioQuantity
public class FriendlyLabelTest {

    [Fact]
    public void SimpleLabel() {
        Assert.Equal("6", 6.cm().Format(HeightFormatter.Instance));
        Assert.Equal("6", 6.mm().Format(HeadCircumferenceFormatter.Instance));
        Assert.Equal("6", 6.kg().Format(WeightFormatter.Instance));
        Assert.Equal("6", 6.BMI().Format(BmiFormatter.Instance));
    }
    //
    // [Fact]
    // public void AgeLabels() {
    //     Assert.Equal("1w", 1.Weeks().Format(AgeFormatter.Instance));
    // }
}