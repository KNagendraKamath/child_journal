using GraphEngine.Graph;
using GraphEngine.Quantities;
using static GraphEngine.Quantities.Unit;

namespace GraphMediator.GraphEngineMediator.Data
{
    internal static class ChildJournalGraphSpec
    {
        internal static readonly Dimension Age= new Dimension("Age", Year, new List<RatioQuantity>());
        internal static readonly Dimension Height = new Dimension("Height", Centimeter, new List<RatioQuantity>());
        internal static readonly Dimension Weight = new Dimension("Weight", Kilogram, new List<RatioQuantity>());
        internal static readonly Dimension BMI = new Dimension("Bmi", Bmi, new List<RatioQuantity>());
        internal static readonly Dimension HeadCircumference= new Dimension("HeadCircumference", Millimeter, new List<RatioQuantity>());

        internal static readonly GraphSpec AgeWeight = new GraphSpec(Age, Weight, new object());
        internal static readonly GraphSpec AgeHeight = new GraphSpec(Age, Height, new object());
        internal static readonly GraphSpec AgeBMI = new GraphSpec(Age, BMI, new object());
        internal static readonly GraphSpec AgeHeadCircumference = new GraphSpec(Age, HeadCircumference, new object());
        internal static readonly GraphSpec HeightWeight = new GraphSpec(Height, Weight, new object());
        internal static readonly GraphSpec HeightHeadCircumference = new GraphSpec(Height, HeadCircumference, new object());
        internal static readonly GraphSpec WeightHeadCircumference = new GraphSpec(Weight, HeadCircumference, new object());

        internal static List<GraphSpec> graphSpecs = new List<GraphSpec>
        {
            AgeWeight,
            AgeHeight,
            AgeBMI,
            AgeHeadCircumference,
            HeightWeight,
            HeightHeadCircumference,
            WeightHeadCircumference
        };

    }
}
