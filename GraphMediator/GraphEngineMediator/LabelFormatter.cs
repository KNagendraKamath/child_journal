using GraphEngine.Quantities;
using static GraphEngine.Quantities.RatioQuantity;
using static GraphEngine.Quantities.Unit;

namespace GraphMediator.GraphEngineMediator;

// Understands label formatting rules for various Dimensions
internal class HeightFormatter : FriendlyFormatter {
    public static readonly HeightFormatter Instance = new();
    
    public List<Unit> Units => [Centimeter];

    public string Format(RatioQuantity quantity, List<double> convertedAmounts) => 
        convertedAmounts.First().ToString();
}

internal class WeightFormatter : FriendlyFormatter {
    public static readonly WeightFormatter Instance = new();
    
    public List<Unit> Units => [Kilogram];

    public string Format(RatioQuantity quantity, List<double> convertedAmounts) => 
        convertedAmounts.First().ToString();
}

internal class HeadCircumferenceFormatter : FriendlyFormatter {
    public static readonly HeadCircumferenceFormatter Instance = new();
    
    public List<Unit> Units => [Millimeter];

    public string Format(RatioQuantity quantity, List<double> convertedAmounts) => 
        convertedAmounts.First().ToString();
}

internal class BmiFormatter : FriendlyFormatter {
    public static readonly BmiFormatter Instance = new();
    
    public List<Unit> Units => [Bmi];

    public string Format(RatioQuantity quantity, List<double> convertedAmounts) => 
        convertedAmounts.First().ToString();
}