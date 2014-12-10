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
            MyMapView.MinScale = 10;

            var centerOfWorld = new MapPoint(-11692031.5488882, 4956667.39181376);

            CreateLine(new[] { centerOfWorld + new Vector(0,  0), centerOfWorld + new Vector(20, 0) }, layer);
            CreateLine(new[] { centerOfWorld + new Vector(20, 0), centerOfWorld + new Vector(40, 0) }, layer);
            CreateLine(new[] { centerOfWorld + new Vector(40, 0), centerOfWorld + new Vector(60, 0) }, layer);

            MyMapView.SetView(new Envelope(centerOfWorld, centerOfWorld + new Vector(60, 0)).Expand(1.2));
        }

        private static void CreateLine(IEnumerable<MapPoint> points, GraphicsLayer layer)
        {
            var path = new Polyline(points);

            var graphic = new Graphic
            {
                Geometry = path
            };
            graphic.Attributes["LENGTH"] = "20'";
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
