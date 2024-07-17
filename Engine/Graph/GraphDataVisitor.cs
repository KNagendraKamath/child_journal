using Engine.ResultRecords;
using GraphEngine.Quantities;
using System.Collections.Generic;
using static GraphEngine.Graph.DataSet;

namespace GraphEngine.Graph
{
    public interface GraphDataVisitor {
        void PreVisit(GraphData graphData, Axis xAxis, Axis yAxis, List<DataSet> dataSets);
        void PreVisit(List<DataSet> dataSets);
        void PreVisit(DataSet dataSet, GraphSpec spec, Axis xAxis) ;
        void Visit(DataSetRecord record) ;
        void PostVisit(DataSet dataSet, GraphSpec spec) ;
        void PostVisit(List<DataSet> dataSets) ;
        void PostVisit(GraphData graphData) ;
    }

    public interface QuantityVisitor {
        void Visit(RatioQuantity quantity, double amount, Unit unit) ;
        void Visit(IntervalQuantity quantity, double amount, Unit unit) ;
        void Visit(Unit unit, Unit baseUnit, double baseUnitRatio, double offset, string label) ;   
    }
    public abstract class DefaultQuantityVisitor :QuantityVisitor
    {
       public virtual void Visit(RatioQuantity quantity, double amount, Unit unit) { }
       public virtual void Visit(IntervalQuantity quantity, double amount, Unit unit) { }
       public virtual void Visit(Unit unit, Unit baseUnit, double baseUnitRatio, double offset, string label) { }
    }
    public abstract class DefaultGraphDataVisitor : GraphDataVisitor 
    {
       public virtual void PreVisit(GraphData graphData, Axis xAxis, Axis yAxis, List<DataSet> dataSets) { }
       public virtual void PreVisit(List<DataSet> dataSets) { }
       public virtual void PreVisit(DataSet dataSet, GraphSpec spec, Axis xAxis) { }
       public virtual void Visit(DataSetRecord record) { }
       public virtual void PostVisit(DataSet dataSet, GraphSpec spec) { }
       public virtual void PostVisit(List<DataSet> dataSets) { }
       public virtual void PostVisit(GraphData graphData) { }
    }
}