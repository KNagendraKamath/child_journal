
namespace GraphEngine.Graph
{
    public class Axis
    {
        private readonly double min;
        private readonly double max;
        private readonly double step;
        private readonly string label;

        public Axis(double min, double max, double step, string label)
        {
            this.min = min;
            this.max = max;
            this.step = step;
            this.label = label;
        }

        public bool Contains(double patientAge) => patientAge >= min && patientAge <= max;
    }
}