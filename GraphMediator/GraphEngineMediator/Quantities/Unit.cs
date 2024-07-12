/*
 * Copyright (c) 2024 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  fredgeorge@acm.org
 */

using Engine.Quantities;
using static Engine.Quantities.IntervalQuantity;

namespace Engine.Quantities {
    // Understands a specific metric
    public class Unit {
        public static readonly Unit Centimeter = new();
        
        public static readonly Unit Kilogram = new();
        
        public static readonly Unit Day = new();
        public static readonly Unit Week = new(7, Day);
        // public static readonly Unit Year = new(365.2425, Day);
        public static readonly Unit Year = new(365, Day);
        public static readonly Unit Month = new(1.0 / 12, Year);

        private readonly Unit _baseUnit;
        private readonly double _baseUnitRatio;
        private readonly double _offset;

        private Unit() {
            _baseUnit = this;
            _baseUnitRatio = 1.0;
            _offset = 0.0;
        }

        private Unit(double relativeRatio, Unit relativeUnit) : this(relativeRatio, 0.0, relativeUnit) { }

        private Unit(double relativeRatio, double offset, Unit relativeUnit) {
            _baseUnit = relativeUnit._baseUnit;
            _baseUnitRatio = relativeRatio * relativeUnit._baseUnitRatio;
            _offset = offset;
        }

        internal double ConvertedAmount(double otherAmount, Unit other) {
            if (!this.IsCompatible(other)) throw new ArgumentException("Incompatible Units for arithmetic");
            return (otherAmount - other._offset) * other._baseUnitRatio / this._baseUnitRatio + this._offset;
        }

        internal int HashCode(double amount) => Math.Round((amount - _offset) * _baseUnitRatio / Epsilon).GetHashCode();

        internal bool IsCompatible(Unit other) => this._baseUnit == other._baseUnit;
    }
}

namespace ExtensionMethods.Probability.Quantities {
    public static class QuantityConstructors {
        public static RatioQuantity Centimeters(this int amount) => new(amount, Unit.Centimeter);
        public static RatioQuantity Centimeters(this double amount) => new(amount, Unit.Centimeter);
        
        public static RatioQuantity Kilograms(this int amount) => new(amount, Unit.Kilogram);
        public static RatioQuantity Kilograms(this double amount) => new(amount, Unit.Kilogram);
        
        public static RatioQuantity Days(this int amount) => new(amount, Unit.Day);
        public static RatioQuantity Days(this double amount) => new(amount, Unit.Day);
        public static RatioQuantity Weeks(this int amount) => new(amount, Unit.Week);
        public static RatioQuantity Weeks(this double amount) => new(amount, Unit.Week);
        public static RatioQuantity Months(this int amount) => new(amount, Unit.Month);
        public static RatioQuantity Months(this double amount) => new(amount, Unit.Month);
        public static RatioQuantity Years(this int amount) => new(amount, Unit.Year);
        public static RatioQuantity Years(this double amount) => new(amount, Unit.Year);
    }
}