using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEngine.Graph
{
    public class GraphSpec
    {
        private readonly Dimension _xDimension;
        private readonly Dimension _yDimension;
        private readonly object _memento;

        public GraphSpec(Dimension xDimension,Dimension yDimension,object memento)
        {
            _yDimension = yDimension;
            _xDimension = xDimension;
            _memento = memento;
        }   
    }
}
