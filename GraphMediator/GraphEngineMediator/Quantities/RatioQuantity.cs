/*
 * Copyright (c) 2024 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  fredgeorge@acm.org
 */

using System.Runtime.CompilerServices;
using Engine.Order;

namespace Engine.Quantities;

// Understands a specific measurement
public class RatioQuantity : IntervalQuantity, Orderable<RatioQuantity> {

    internal RatioQuantity(double amount, Unit unit) : base(amount, unit) { }

    public static RatioQuantity operator +(RatioQuantity left, RatioQuantity right) =>
        new(left._amount + left.ConvertedAmount(right), left._unit);

    public static RatioQuantity operator -(RatioQuantity q) => new(-q._amount, q._unit);

    public static RatioQuantity operator -(RatioQuantity left, RatioQuantity right) => left + -right;
    
    public bool IsBetterThan(RatioQuantity other) => base.IsBetterThan(other);
}