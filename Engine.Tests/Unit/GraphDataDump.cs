using System.Collections.Generic;
using GraphEngine.Graph;
using GraphEngine.Quantities;
using static GraphEngine.Graph.DataSet;

// ReSharper disable InconsistentNaming
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace GraphEngine.Tests.Unit;

internal class GraphDataDump : GraphDataVisitor {
    internal GraphDataDto GraphDataDTO;
    internal DataSetDto DataSetDTO;
    
    private Scale _xAxis;
    private Scale _yAxis;
    private List<DataSetDto> _dataSets = new();

    internal GraphDataDump(GraphData data) {
        data.Accept(this);
    }
    
    internal GraphDataDump(DataSet dataSet) {
        dataSet.Accept(this);
    }

    public void PreVisit(GraphData graphData, Scale xAxis, Scale yAxis, List<DataSet> dataSets)
    {
        _xAxis = xAxis;
        _yAxis = yAxis;
    }

    public void PreVisit(DataSet dataSet, GraphSpec spec, Scale xAxis)
    {
        DataSetDTO = new DataSetDto(dataSet.Count, new(), spec, xAxis);
        _dataSets.Add(DataSetDTO);
    }

    public void Visit(DataSetRecord record) {
        DataSetDTO.Records.Add(record);
    }

    public void PostVisit(GraphData graphData) {
        GraphDataDTO = new GraphDataDto(_xAxis, _yAxis, _dataSets);
    }

    internal class GraphDataDto {
        internal readonly Scale XAxis;
        internal readonly Scale YAxis;
        internal readonly List<DataSetDto> DataSets;

        internal GraphDataDto(Scale xAxis, Scale yAxis, List<DataSetDto> dataSets) {
            XAxis = xAxis;
            YAxis = yAxis;
            DataSets = dataSets;
        }
    }

    internal record AxisDto {
        internal readonly double Min;
        internal readonly double Max;
        internal readonly double Step;
        internal readonly string Label;

        internal AxisDto(double min, double max, double step, string label) {
            Min = min;
            Max = max;
            Step = step;
            Label = label;
        }
    }

    internal record DataSetDto {
        internal readonly int Count;
        internal readonly List<DataSetRecord> Records;
        internal readonly GraphSpec Spec;
        internal readonly Scale Scale;

        internal DataSetDto(int count, List<DataSetRecord> records, GraphSpec spec, Scale scale) {
            Count = count;
            Records = records;
            Spec = spec;
            Scale = scale;
        }
    }

    private class RatioQuantityDataDump : QuantityVisitor {
        internal record RatioQuantityDto(double Amount, string Unit);
        internal RatioQuantityDto? RatioQuantityDTO;
        private double _amount;
        
        internal RatioQuantityDataDump(RatioQuantity quantity) {
            quantity.Accept(this);
        }

        public void Visit(RatioQuantity quantity, double amount, Quantities.Unit unit) {
            _amount = amount;
        }

        public void Visit(Quantities.Unit unit, Quantities.Unit baseUnit, double baseUnitRatio, double offset, string label) {
            RatioQuantityDTO = new RatioQuantityDto(_amount, label);
        }
    }
}