using Engine.ResultRecords;

namespace GraphEngine.Graph
{
    public class PointData
    {
        private readonly CompleteList _completeList;
        private readonly Column _xColumn;
        private readonly Column _yColumn;


        public PointData(CompleteList completeList, Column xColumn, Column yColumn)
        {
            _yColumn = yColumn;
            _xColumn = xColumn;
            _completeList = completeList;
        }
    }
}