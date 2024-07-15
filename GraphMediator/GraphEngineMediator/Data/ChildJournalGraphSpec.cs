using ExtensionMethods.Probability.Quantities;
using GraphEngine.Graph;
using GraphEngine.Quantities;
using static GraphEngine.Quantities.Unit;

namespace GraphMediator.GraphEngineMediator.Data
{
    internal static class ChildJournalGraphSpec
    {
        internal static readonly Dimension Age= new Dimension("Age", Year,  [ 1.Weeks(), 2.Weeks(), 1.Months(), 1.Quarters(), 1.Years(), 2.Years(), 5.Years() ]);
        internal static readonly Dimension Height = new Dimension("Height", Centimeter, [ 1.cm(), 5.cm(), 10.cm(), 25.cm()]);
        internal static readonly Dimension Weight = new Dimension("Weight", Kilogram, [ 1.kg(), 5.kg(), 10.kg(), 25.kg()]);
        internal static readonly Dimension BMI = new Dimension("Bmi", Bmi, [ 1.BMI(), 5.BMI(), 10.BMI()]);
        internal static readonly Dimension HeadCircumference= new Dimension("HeadCircumference", Millimeter, [1.mm(), 5.mm(), 10.mm()]);

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
