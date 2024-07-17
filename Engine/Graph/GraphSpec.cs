namespace GraphEngine.Graph
{
    public class GraphSpec
    {
        public readonly Dimension _xDimension;
        public readonly Dimension _yDimension;
        public readonly object _memento;

        public GraphSpec(Dimension XDimension, Dimension YDimension, object Memento)
        {
            _xDimension = XDimension;
            _yDimension = YDimension;
            _memento = Memento;
        }
    }
}
