﻿using GraphEngine.Graph;
using static GraphEngine.Quantities.Unit;
using static ExtensionMethods.Probability.Quantities.QuantityConstructors;

namespace GraphMediator.GraphEngineMediator.Data {
    internal static class ChildJournalGraphSpec {
        internal static readonly Dimension Age = new(
            "Age",
            Year,
            [1.Weeks(), 1.Months(), 1.Quarters(), 1.Years(), 2.Years(), 5.Years()],
            0.Months(),
            5.Months());

        internal static readonly Dimension Height = new(
            "Height",
            Centimeter,
            [1.cm(), 5.cm(), 10.cm(), 25.cm()],
            50.cm(),
            140.cm());

        internal static readonly Dimension Weight = new(
            "Weight",
            Kilogram,
            [1.kg(), 5.kg(), 10.kg(), 25.kg()],
            3.kg(),
            20.kg());

        // ReSharper disable once InconsistentNaming
        internal static readonly Dimension BMI = new(
            "Bmi",
            Bmi,
            [1.BMI(), 5.BMI(), 10.BMI(), 25.BMI()],
            10.BMI(),
            20.BMI());

        internal static readonly Dimension HeadCircumference = new(
            "HeadCircumference",
            Millimeter,
            [1.mm(), 5.mm(), 10.mm(), 25.mm()],
            30.mm(),
            55.mm());

        internal static readonly GraphSpec AgeWeight = new(Age, Weight,"Weight", new object());
        internal static readonly GraphSpec AgeHeight = new(Age, Height,"Height", new object());
        // ReSharper disable once InconsistentNaming
        internal static readonly GraphSpec AgeBMI = new(Age, BMI,"BMI", new object());
        internal static readonly GraphSpec AgeHeadCircumference = new(Age, HeadCircumference,"Head Circumference", new object());
        internal static readonly GraphSpec HeightWeight = new(Height, Weight,"Height/Weight", new object());
        internal static readonly GraphSpec HeightHeadCircumference = new(Height, HeadCircumference,"Height/Head Circumference", new object());
        internal static readonly GraphSpec WeightHeadCircumference = new(Weight, HeadCircumference,"Weight/Head Circumference", new object());

        internal static List<GraphSpec> GraphSpecs = new() {
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
