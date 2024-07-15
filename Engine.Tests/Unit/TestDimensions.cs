using ExtensionMethods.Probability.Quantities;
using GraphEngine.Graph;
using static GraphEngine.Quantities.Unit;

namespace GraphEngine.Tests.Unit;

internal static class TestDimensions
{
    internal static readonly Dimension Age = new Dimension("Age", Year, [1.Weeks(), 2.Weeks(), 1.Months(), 1.Quarters(), 1.Years(), 2.Years(), 5.Years()]);
    internal static readonly Dimension Height = new Dimension("Height", Centimeter, [1.cm(), 5.cm(), 10.cm(), 25.cm()]);
    internal static readonly Dimension Weight = new Dimension("Weight", Kilogram, [1.kg(), 5.kg(), 10.kg(), 25.kg()]);
    internal static readonly GraphSpec AgeWeight = new GraphSpec(Age, Weight, new object());
}
