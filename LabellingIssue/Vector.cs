using Esri.ArcGISRuntime.Geometry;

namespace LabellingIssue
{
    internal class Vector
    {
        private readonly double _x;
        private readonly double _y;

        public Vector(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public double X
        {
            get { return _x; }
        }

        public double Y
        {
            get { return _y; }
        }

        public static MapPoint operator +(MapPoint point, Vector vector)
        {
            return new MapPoint(point.X + vector.X, point.Y + vector.Y);
        }
    }
}