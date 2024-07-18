using ExtensionMethods.Probability.Quantities;
using GraphEngine.Graph;
using GraphEngine.Quantities;
using System.Collections.Generic;
using static GraphEngine.Quantities.Unit;

namespace GraphEngine.Tests.Unit
{
    internal static class TestDimensions {
        internal static readonly Dimension Age = new Dimension(
            "Age",
            Year,
            new List<RatioQuantity>() { 1.Weeks(), 1.Months(), 1.Quarters(), 1.Years(), 2.Years(), 5.Years() },
            0.Months(),
            5.Months());

        internal static readonly Dimension Height = new Dimension(
            "Height",
            Centimeter,
            new List<RatioQuantity>() { 1.cm(), 5.cm(), 10.cm(), 25.cm() },
            0.Months(),
            5.Months());

        internal static readonly Dimension Weight = new Dimension(
            "Weight",
            Kilogram,
            new List<RatioQuantity>() { 1.kg(), 5.kg(), 10.kg(), 25.kg() },
            0.Months(),
            5.Months());

        internal static readonly GraphSpec AgeWeight = new GraphSpec(Age, Weight, "Weight", new object());
    }
}