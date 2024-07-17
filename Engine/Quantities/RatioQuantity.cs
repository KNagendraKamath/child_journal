using GraphEngine.Graph;
using static GraphEngine.Quantities.Unit;

namespace GraphEngine.Quantities {
    
    // Understands a specific measurement
    public class RatioQuantity : IntervalQuantity, IComparable<RatioQuantity> {
        public interface FriendlyFormatter {
            public List<Unit> Units { get; }
            public List<string> Format(List<(RatioQuantity quantity, List<double> convertedAmounts)> listOfAmounts);
        };

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

        public int CompareTo(RatioQuantity? other) {
            if (this.Equals(other)) return 0;
            return this._amount.CompareTo(this.ConvertedAmount(other!));
        }

        public RatioQuantity ScaleBy(double factor)=> new RatioQuantity(_amount*factor,_unit);

        internal double ConvertedAmount(Unit unit) => unit.ConvertedAmount(_amount, _unit);
    }
}

namespace GraphEngine.Quantities.Extensions {
    public static class RatioQuantityExtensions {
        
        public static List<string> Format(
            this List<RatioQuantity> quantities,
            RatioQuantity.FriendlyFormatter formatter) {
            return formatter.Format(
                quantities.Select(q => (q, formatter.Units.Select(unit => q.ConvertedAmount(unit)).ToList())).ToList());
        }
    }
}