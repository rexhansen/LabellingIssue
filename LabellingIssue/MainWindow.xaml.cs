using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Media;
using Esri.ArcGISRuntime.Controls;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Layers;
using Esri.ArcGISRuntime.Symbology;

namespace LabellingIssue
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MyMapView.Loaded += MapView_Loaded;
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            var layer = new ComponentLayer();
            MyMapView.Map.Layers.Add(layer);
            MyMapView.MaxScale = 0.001;

            var centerOfWorld = new MapPoint(-11692090.5488882, 4956767.39181376);

            CreateLine(new[] { centerOfWorld + new Vector(0,  0), centerOfWorld + new Vector(20, 0) }, layer, "20");
            CreateLine(new[] { centerOfWorld + new Vector(20, 0), centerOfWorld + new Vector(40, 0) }, layer, "20");
            CreateLine(new[] { centerOfWorld + new Vector(40, 0), centerOfWorld + new Vector(60, 0) }, layer, "20");

            MapPoint offCenterOfWorld = centerOfWorld.MoveTo(centerOfWorld.X, centerOfWorld.Y - 25);

            CreateLine(new[] { offCenterOfWorld + new Vector(0, 0), offCenterOfWorld + new Vector(20, 0) }, layer, "20");
            CreateLine(new[] { offCenterOfWorld + new Vector(20, 0), offCenterOfWorld + new Vector(40, 0) }, layer, "40");
            CreateLine(new[] { offCenterOfWorld + new Vector(40, 0), offCenterOfWorld + new Vector(60, 0) }, layer, "60");

            MapPoint offCenterOfWorld2 = centerOfWorld.MoveTo(centerOfWorld.X, centerOfWorld.Y - 50);

            CreateLine(new[] { offCenterOfWorld2 + new Vector(0, 0), offCenterOfWorld2 + new Vector(20, 0) }, layer, "20");
            CreateLine(new[] { offCenterOfWorld2 + new Vector(21, 0), offCenterOfWorld2 + new Vector(41, 0) }, layer, "20");
            CreateLine(new[] { offCenterOfWorld2 + new Vector(42, 0), offCenterOfWorld2 + new Vector(62, 0) }, layer, "20");
            
            MyMapView.SetView(new Envelope(centerOfWorld, offCenterOfWorld2 + new Vector(60, 0)).Expand(1.4));
        }

        private static void CreateLine(IEnumerable<MapPoint> points, GraphicsLayer layer, string labelValue)
        {
            var path = new Polyline(points);

            var graphic = new Graphic
            {
                Geometry = path
            };
            graphic.Attributes["LENGTH"] = string.Format("{0}'", labelValue);
            var symbol = new SimpleLineSymbol
            {
                Color = Colors.DarkRed,
                Style = SimpleLineStyle.Solid,
                Width = 2
            };

            graphic.Symbol = symbol;
            graphic.ZIndex = 1000;
            layer.Invoke(l => l.Graphics.Add(graphic));
        }

        private void MyMapView_LayerLoaded(object sender, LayerLoadedEventArgs e)
        {
            if (e.LoadError == null)
                return;

            Debug.WriteLine(string.Format("Error while loading layer : {0} - {1}", e.Layer.ID, e.LoadError.Message));
        }
    }
}
