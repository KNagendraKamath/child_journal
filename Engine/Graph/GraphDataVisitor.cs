using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEngine.Graph
{
    public interface GraphDataVisitor
    {
        void Visit(Axis xAxis, double min, double max,double step,string label);   
    }
}
