/*
 * Copyright (c) 2024 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  frredgeorge@acm.org
 */

using ExtensionMethods.Probability.Quantities;
using GraphEngine.Quantities;
using System;
using System.Collections.Generic;
using Xunit;

namespace GraphEngine.Tests.Unit;

// Ensures Quantities operate correctly
public class QuantityTest {
    [Fact]
    public void EqualityOfLikeUnits() {
        Assert.Equal(8.0.Weeks(), 8.0.Weeks());
        Assert.NotEqual(8.Weeks(), 6.0.Weeks());
        Assert.NotEqual(8.Weeks(), new object());
#pragma warning disable xUnit2000
        Assert.NotEqual(8.Weeks(), null);
#pragma warning restore xUnit2000
    }

    [Fact]
    public void EqualityOfDifferentUnits() {
        Assert.NotEqual(8.0.Weeks(), 8.0.Days());
        Assert.Equal(2.5.Weeks(), 17.5.Days());
        Assert.Equal(365.Days(), 1.Years());
    }

    [Fact]
    public void Set() {
        Assert.Single(new HashSet<RatioQuantity> { 8.0.Weeks(), 8.0.Weeks() });
        Assert.Contains(8.0.Weeks(), new HashSet<RatioQuantity> { 8.0.Weeks() });
    }

    [Fact]
    public void Hash() {
        Assert.Equal(8.0.Weeks().GetHashCode(), 8.0.Weeks().GetHashCode());
        Assert.Equal(2.5.Weeks().GetHashCode(), 17.5.Days().GetHashCode());
    }

    [Fact]
    public void Arithmetic() {
        Assert.Equal(17.Days(), 2.Weeks() + 3.Days());
        Assert.Equal((-6).Weeks(), -6.Weeks());
        Assert.Equal(-1.Weeks(), 2.Weeks() - 21.Days());
    }

    [Fact]
    public void CrossUnitEquality() {
        Assert.NotEqual(1.Days(), 1.kg());
    }

    [Fact]
    public void CrossUnitArithmetic() {
        Assert.Throws<ArgumentException>(() => 3.Days() - 4.cm());
    }
    [Fact]
    public void RoundDown() { 
        Assert.Equal(50.cm(), 50.cm().RoundDown(5.cm()));
        Assert.Equal(50.cm(), 54.9.cm().RoundDown(5.cm()));
    }
    [Fact]
    public void RoundUp()
    {
        Assert.Equal(50.cm(), 50.cm().RoundUp(5.cm()));
        Assert.Equal(55.cm(), 54.9.cm().RoundUp(5.cm()));
        Assert.Equal(55.cm(), 50.1.cm().RoundUp(5.cm()));
    }
}