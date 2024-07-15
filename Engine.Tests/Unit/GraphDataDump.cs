using System.Collections.Generic;
using Engine.ResultRecords;
using GraphEngine.Graph;
using static GraphEngine.Graph.BasicDataSet;

// ReSharper disable InconsistentNaming
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace GraphEngine.Tests.Unit;

internal class GraphDataDump : GraphDataVisitor {
    internal GraphDataDto GraphDataDTO;
    internal AxisDto AxisDTO => _xAxisDto;
    internal DataSetDto DataSetDTO;
    
    private Axis? _xAxis;
    private Axis? _yAxix;
    private AxisDto _xAxisDto;
    private AxisDto _yAxixDto;
    private List<DataSetDto> _dataSets = new();

    internal GraphDataDump(GraphData data) {
        data.Accept(this);
    }
    
    internal GraphDataDump(BasicDataSet dataSet) {
        dataSet.Accept(this);
    }
        
    internal GraphDataDump(Axis axis)
    {
        axis.Accept(this);
    }

    public void PreVisit(GraphData graphData, Axis xAxis, Axis yAxis) {
        _xAxis = xAxis;
        _yAxix = yAxis;
    }

    public void Visit(Axis axis, string label, double min, double max, double step)
    {
        var dto = new AxisDto(min, max, step, label);
        if (_xAxis == null || axis == _xAxis) _xAxisDto = dto;
        else _yAxixDto = dto;
    }

    public void PreVisit(BasicDataSet dataSet, Column xColumn, Column yColumn, object memento, double min, double max) {
        DataSetDTO = new DataSetDto(dataSet.Count, new(), xColumn, yColumn, min, max, memento);
        _dataSets.Add(DataSetDTO);
    }

    public void Visit(DataSetRecord record) {
        DataSetDTO.Records.Add(record);
    }

    public void PostVisit(GraphData graphData) {
        GraphDataDTO = new GraphDataDto(_xAxisDto, _yAxixDto, _dataSets);
    }

    internal class GraphDataDto {
        internal readonly AxisDto XAxis;
        internal readonly AxisDto YAxis;
        internal readonly List<DataSetDto> DataSets;

        internal GraphDataDto(AxisDto xAxis, AxisDto yAxis, List<DataSetDto> dataSets) {
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
        internal readonly Column XColumn;
        internal readonly Column YColumn;
        internal readonly double Min;
        internal readonly double Max;
        internal readonly object Memento;

        internal DataSetDto(int count, List<DataSetRecord> records, Column xColumn, Column yColumn, double min, double max, object memento) {
            Count = count;
            Records = records;
            XColumn = xColumn;
            YColumn = yColumn;
            Min = min;
            Max = max;
            Memento = memento;
        }
    }
}