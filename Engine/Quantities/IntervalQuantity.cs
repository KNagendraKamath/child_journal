/*
 * Copyright (c) 2024 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  fredgeorge@acm.org
 */

namespace GraphEngine.Quantities;

// Understands a specific point on a axis
public class IntervalQuantity  {
    internal const double Epsilon = 1e-10;
    protected readonly double _amount;
    protected readonly Unit _unit;

    internal IntervalQuantity(double amount, Unit unit) {
        _amount = amount;
        _unit = unit;
    }

    public override bool Equals(object? obj) =>
        this == obj || obj is IntervalQuantity other && this.Equals(other);

    private bool Equals(IntervalQuantity other) =>
        this.IsCompatible(other) && Math.Abs(this._amount - ConvertedAmount(other)) < Epsilon;

    private bool IsCompatible(IntervalQuantity other) => this._unit.IsCompatible(other._unit);

    protected double ConvertedAmount(IntervalQuantity other) => 
        this._unit.ConvertedAmount(other._amount, other._unit);

    public override int GetHashCode() => _unit.HashCode(_amount);
}