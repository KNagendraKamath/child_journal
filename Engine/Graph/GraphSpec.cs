namespace GraphEngine.Graph
{
    public class GraphSpec
    {
        public readonly Dimension _xDimension;
        public readonly Dimension _yDimension;
        public readonly string _label;
        public readonly object _memento;

        public GraphSpec(Dimension XDimension, Dimension YDimension, string label, object Memento)
        {
            _xDimension = XDimension;
            _yDimension = YDimension;
            _label = label;
            _memento = Memento;
        }
    }
}
