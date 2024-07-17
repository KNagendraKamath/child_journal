using GraphEngine.Quantities;
using static GraphEngine.Quantities.RatioQuantity;
using static GraphEngine.Quantities.Unit;

namespace GraphMediator.GraphEngineMediator;

// Understands label formatting rules for various Dimensions
internal class HeightFormatter : FriendlyFormatter {
    public static readonly HeightFormatter Instance = new();
    
    public List<Unit> Units => [Centimeter];

    public List<string> Format(List<(RatioQuantity quantity, List<double> convertedAmounts)> listOfAmounts) => 
        [listOfAmounts[0].convertedAmounts[0].ToString()];
}

internal class WeightFormatter : FriendlyFormatter {
    public static readonly WeightFormatter Instance = new();
    
    public List<Unit> Units => [Kilogram];

    public List<string> Format(List<(RatioQuantity quantity, List<double> convertedAmounts)> listOfAmounts) => 
        [listOfAmounts[0].convertedAmounts[0].ToString()];
}

internal class HeadCircumferenceFormatter : FriendlyFormatter {
    public static readonly HeadCircumferenceFormatter Instance = new();
    
    public List<Unit> Units => [Millimeter];

    public List<string> Format(List<(RatioQuantity quantity, List<double> convertedAmounts)> listOfAmounts) => 
        [listOfAmounts[0].convertedAmounts[0].ToString()];
}

internal class BmiFormatter : FriendlyFormatter {
    public static readonly BmiFormatter Instance = new();
    
    public List<Unit> Units => [Bmi];

    public List<string> Format(List<(RatioQuantity quantity, List<double> convertedAmounts)> listOfAmounts) => 
        [listOfAmounts[0].convertedAmounts[0].ToString()];
}