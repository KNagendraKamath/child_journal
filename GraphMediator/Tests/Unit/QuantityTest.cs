/*
 * Copyright (c) 2024 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  fredgeorge@acm.org
 */

using System;
using System.Collections.Generic;
using Engine.Quantities;
using ExtensionMethods.Probability.Quantities;
using Xunit;
using static Engine.Quantities.Unit;

namespace Engine.Tests.Unit;

// Ensures Quantities operate correctly
public class QuantityTest {
    [Fact]
    public void EqualityOfLikeUnits() {
        Assert.Equal(8.0.Tablespoons(), 8.0.Tablespoons());
        Assert.NotEqual(8.Tablespoons(), 6.0.Tablespoons());
        Assert.NotEqual(8.Tablespoons(), new object());
        Assert.NotEqual(8.Tablespoons(), null);
    }

    [Fact]
    public void EqualityOfDifferentUnits() {
        Assert.NotEqual(8.0.Tablespoons(), 8.0.Pints());
        Assert.Equal(8.Tablespoons(), 0.5.Cups());
        Assert.Equal(768.Teaspoons(), 1.Gallons());
        Assert.Equal(18.Inches(), 0.5.Yards());
        Assert.Equal(1.Miles(), (12 * 5280).Inches());
        Assert.Equal(1.5.Leagues(), 36.Furlongs());
        Assert.Equal(22.Fathoms(), 2.Chains());
    }

    [Fact]
    public void Set() {
        Assert.Single(new HashSet<RatioQuantity> { 8.0.Tablespoons(), 8.0.Tablespoons() });
        Assert.Contains(8.0.Tablespoons(), new HashSet<RatioQuantity> { 8.0.Tablespoons() });
    }

    [Fact]
    public void Hash() {
        Assert.Equal(8.0.Tablespoons().GetHashCode(), 8.0.Tablespoons().GetHashCode());
        Assert.Equal(8.Tablespoons().GetHashCode(), 0.5.Cups().GetHashCode());
        Assert.Equal(18.Inches().GetHashCode(), 0.5.Yards().GetHashCode());
        Assert.Equal(10.Celsius().GetHashCode(), 50.Fahrenheit().GetHashCode());
        Assert.Equal(325.Fahrenheit().GetHashCode(), 3.GasMarks().GetHashCode());
    }

    [Fact]
    public void Arithmetic() {
        Assert.Equal(0.5.Quarts(), 6.Tablespoons() + 13.Ounces());
        Assert.Equal((-6).Tablespoons(), -6.Tablespoons());
        Assert.Equal(-0.5.Pints(), 10.Tablespoons() - 13.Ounces());
        Assert.Equal(-4.Feet(), 24.Inches() - 2.Yards());
    }

    [Fact]
    public void CrossUnitEquality() {
        Assert.NotEqual(1.Inches(), 1.Teaspoons());
        Assert.NotEqual(4.Ounces(), 2.Feet());
    }

    [Fact]
    public void CrossUnitArithmetic() {
        Assert.Throws<ArgumentException>(() => 3.Yards() - 4.Tablespoons());
    }

    [Fact]
    public void TemperatureEquality() {
        AssertBidirectionalEquality(0.Celsius(), 32.Fahrenheit());
        AssertBidirectionalEquality(10.Celsius(), 50.Fahrenheit());
        AssertBidirectionalEquality(100.Celsius(), 212.Fahrenheit());
        AssertBidirectionalEquality((-40).Celsius(), (-40).Fahrenheit());
        AssertBidirectionalEquality(325.Fahrenheit(), 3.GasMarks());
        AssertBidirectionalEquality(0.Celsius(), 273.15.Kelvin());
        AssertBidirectionalEquality(50.Fahrenheit(), 283.15.Kelvin());
        AssertBidirectionalEquality(50.Fahrenheit(), 509.67.Rankin());
    }

    [Fact]
    public void TemperatureArithmetic() {
        // The following should not even compile
        // var q = 10.Celsius() + 50.Fahrenheit();
    }

    private void AssertBidirectionalEquality(IntervalQuantity left, IntervalQuantity right) {
        Assert.Equal(left, right);
        Assert.Equal(right, left);
    }
}