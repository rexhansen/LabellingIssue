using System;
using System.Windows.Threading;
using Esri.ArcGISRuntime.Layers;

namespace LabellingIssue
{
    internal static class GraphicsLayerExtensions
    {
        public static void Invoke(this GraphicsLayer layer, Action<GraphicsLayer> action)
        {
            GraphicsLayer tempLayer = layer;
            tempLayer.Dispatcher.Invoke(DispatcherPriority.Render, new Action(() => action(tempLayer)));
        }
    }
}