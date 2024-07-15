/*
 * Copyright (c) 2024 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  fredgeorge@acm.org
 */


using System.Data;
using GraphEngine.Graph;

namespace GraphEngine.Quantities;

// Understands a specific measurement
public class RatioQuantity : IntervalQuantity {

    internal RatioQuantity(double amount, Unit unit) : base(amount, unit) { }

    public RatioQuantity RoundDown(RatioQuantity gap) => 
        new(Math.Floor(gap.ConvertedAmount(this) / gap._amount) * gap._amount, gap._unit);

    public RatioQuantity RoundUp(RatioQuantity gap)=>
        new(Math.Ceiling(Math.Round(gap.ConvertedAmount(this),10) / gap._amount) * gap._amount, gap._unit);
    
    public static RatioQuantity operator +(RatioQuantity left, RatioQuantity right) =>
        new(left._amount + left.ConvertedAmount(right), left._unit);

    public static RatioQuantity operator -(RatioQuantity q) => new(-q._amount, q._unit);

    public static RatioQuantity operator -(RatioQuantity left, RatioQuantity right) => left + -right;
    
    public static double operator /(RatioQuantity left, RatioQuantity right) => 
        left._amount / left.ConvertedAmount(right);

    public override string ToString() => $"{_amount} {_unit}";

    public void Accept(QuantityVisitor visitor) {
        visitor.Visit(this, _amount, _unit);
        _unit.Accept(visitor);
    }
    public static bool operator >=(RatioQuantity left, RatioQuantity right)
    {
        if(left.Equals(right)) return true;
        return left._amount >= left.ConvertedAmount(right);
    }
    public static bool operator <=(RatioQuantity left, RatioQuantity right)
    {
        if (left.Equals(right)) return true;
        return left._amount <= left.ConvertedAmount(right);
    }
}