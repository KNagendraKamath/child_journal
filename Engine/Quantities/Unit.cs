/*
 * Copyright (c) 2024 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  fredgeorge@acm.org
 */

using GraphEngine.Graph;
using GraphEngine.Quantities;
using System;
using System.Collections.Generic;
using static GraphEngine.Quantities.IntervalQuantity;

namespace GraphEngine.Quantities {
    // Understands a specific metric
    public class Unit {
        private static readonly Dictionary<string, Unit> AllUnits = new Dictionary<string, Unit>();

        public static RatioQuantity RatioQuantityFrom(double amount, string unitLabel) =>
            new RatioQuantity(amount, AllUnits[unitLabel]);

        public static readonly Unit Millimeter = new Unit("mm");
        public static readonly Unit Centimeter = new Unit("cm", 10, Millimeter);

        public static readonly Unit Kilogram = new Unit("kg");

        public static readonly Unit Bmi = new Unit("BMI");

        public static readonly Unit Day = new Unit("days");
        public static readonly Unit Week = new Unit("weeks", 7, Day);
        // public static readonly Unit Year = new("years", 365.2425, Day);
        public static readonly Unit Year = new Unit("years", 365, Day);
        public static readonly Unit Month = new Unit("months", 1.0 / 12, Year);
        public static readonly Unit Quarter = new Unit("quarters", 1.0 / 4.0, Year);

        private readonly string _label;
        private readonly Unit _baseUnit;
        private readonly double _baseUnitRatio;
        private readonly double _offset;

        private Unit(string label) {
            _label = label;
            _baseUnit = this;
            _baseUnitRatio = 1.0;
            _offset = 0.0;
            AllUnits[label] = this;
        }

        private Unit(string label, double relativeRatio, Unit relativeUnit) : this(label,
            relativeRatio,
            0.0,
            relativeUnit) { }

        private Unit(string label, double relativeRatio, double offset, Unit relativeUnit) {
            _label = label;
            _baseUnit = relativeUnit._baseUnit;
            _baseUnitRatio = relativeRatio * relativeUnit._baseUnitRatio;
            _offset = offset;
            AllUnits[label] = this;
        }

        public override string ToString() => _label;

        internal double ConvertedAmount(double otherAmount, Unit other) {
            if (!this.IsCompatible(other)) throw new ArgumentException("Incompatible Units for arithmetic");
            return (otherAmount - other._offset) * other._baseUnitRatio / this._baseUnitRatio + this._offset;
        }

        internal int HashCode(double amount) => Math.Round((amount - _offset) * _baseUnitRatio / Epsilon).GetHashCode();

        internal bool IsCompatible(Unit other) => this._baseUnit == other._baseUnit;
        
        internal void Accept(QuantityVisitor visitor) => visitor.Visit(this, _baseUnit, _baseUnitRatio, _offset, _label);
    }
}

namespace ExtensionMethods.Probability.Quantities {
    public static class QuantityConstructors {
        public static RatioQuantity cm(this int amount) => new RatioQuantity(amount, Unit.Centimeter);
        public static RatioQuantity cm(this double amount) => new RatioQuantity(amount, Unit.Centimeter);

        public static RatioQuantity mm(this int amount) => new RatioQuantity(amount, Unit.Millimeter);
        public static RatioQuantity mm(this double amount) => new RatioQuantity(amount, Unit.Millimeter);


        public static RatioQuantity kg(this int amount) => new RatioQuantity(amount, Unit.Kilogram);
        public static RatioQuantity kg(this double amount) => new RatioQuantity(amount, Unit.Kilogram);

        public static RatioQuantity BMI(this int amount) => new RatioQuantity(amount, Unit.Bmi);
        public static RatioQuantity BMI(this double amount) => new RatioQuantity(amount, Unit.Bmi);

        public static RatioQuantity Days(this int amount) => new RatioQuantity(amount, Unit.Day);
        public static RatioQuantity Days(this double amount) => new RatioQuantity(amount, Unit.Day);
        public static RatioQuantity Weeks(this int amount) => new RatioQuantity(amount, Unit.Week);
        public static RatioQuantity Weeks(this double amount) => new RatioQuantity(amount, Unit.Week);
        public static RatioQuantity Months(this int amount) => new RatioQuantity(amount, Unit.Month);
        public static RatioQuantity Months(this double amount) => new RatioQuantity(amount, Unit.Month);
        public static RatioQuantity Quarters(this int amount) => new RatioQuantity(amount, Unit.Quarter);
        public static RatioQuantity Quarters(this double amount) => new RatioQuantity(amount, Unit.Quarter);
        public static RatioQuantity Years(this int amount) => new RatioQuantity(amount, Unit.Year);
        public static RatioQuantity Years(this double amount) => new RatioQuantity(amount, Unit.Year);
    }
}