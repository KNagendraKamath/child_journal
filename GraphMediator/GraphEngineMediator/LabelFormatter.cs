using GraphEngine.Quantities;
using System;
using System.Collections.Generic;
using System.Linq;
using static GraphEngine.Quantities.RatioQuantity;
using static GraphEngine.Quantities.Unit;

namespace GraphMediator.GraphEngineMediator
{
    // Understands label formatting rules for various Dimensions
    internal class HeightFormatter : FriendlyFormatter {
        public static readonly HeightFormatter Instance = new HeightFormatter();

        public List<Unit> Units => new List<Unit> { Centimeter };

        public List<(double, string)> Format(List<List<double>> listOfAmounts) =>
        new List<(double, string)> { (listOfAmounts[0][0], listOfAmounts[0][0].ToString()) };
    }

    internal class WeightFormatter : FriendlyFormatter {
        public static readonly WeightFormatter Instance = new WeightFormatter();

        public List<Unit> Units => new List<Unit> { Kilogram };

        public List<(double, string)> Format(List<List<double>> listOfAmounts) =>
        new List<(double, string)> { (listOfAmounts[0][0], listOfAmounts[0][0].ToString()) };
    }

    internal class HeadCircumferenceFormatter : FriendlyFormatter {
        public static readonly HeadCircumferenceFormatter Instance = new HeadCircumferenceFormatter();

        public List<Unit> Units => new List<Unit> { Millimeter };

        public List<(double, string)> Format(List<List<double>> listOfAmounts) =>
        new List<(double, string)> { (listOfAmounts[0][0], listOfAmounts[0][0].ToString()) };
    }

    internal class BmiFormatter : FriendlyFormatter {
        public static readonly BmiFormatter Instance = new BmiFormatter();

        public List<Unit> Units => new List<Unit> { Bmi };

        public List<(double, string)> Format(List<List<double>> listOfAmounts) =>
        new List<(double, string)> { (listOfAmounts[0][0], listOfAmounts[0][0].ToString()) };
    }

    internal class AgeFormatter : FriendlyFormatter
    {
        private delegate (double, string) Formatter(List<double> convertedAmounts);
        private const int MaxWeeks = 20;
        private const int MaxMonths = 12;

        public static readonly AgeFormatter Instance = new AgeFormatter();

        public List<Unit> Units => new List<Unit> { Week, Month, Year };

        public List<(double, string)> Format(List<List<double>> listOfAmounts)
        {
            Formatter formatter = FormatterFor(listOfAmounts.Last());
            return listOfAmounts.Select(convertedAmounts => formatter.Invoke(convertedAmounts)).ToList();
        }

        private Formatter FormatterFor(List<double> convertedAmounts)
        {
            if (convertedAmounts[0] <= MaxWeeks) return amounts => (amounts[0], $"{amounts[0]}w");
            if (convertedAmounts[1] <= MaxMonths) return amounts => (amounts[1], $"{amounts[1]}m");
            return amounts => {
                var years = Math.Floor(amounts[2]);
                var months = amounts[1] - 12 * years;
                return months == 0 ? (amounts[2], $"{years}y") : (amounts[2], $"{years}y{months}m");
            };
        }
    }
}