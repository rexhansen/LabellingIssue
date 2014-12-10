using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Esri.ArcGISRuntime.Layers;
using Esri.ArcGISRuntime.Symbology;

namespace LabellingIssue
{
    internal sealed class ComponentLayer : GraphicsLayer
    {
        public ComponentLayer()
        {
            RenderingMode = GraphicsRenderingMode.Dynamic;
            Labeling.IsEnabled = true;
            
            Labeling.LabelClasses = new AttributeLabelClassCollection
            {
                new AttributeLabelClass
                {
                    IsVisible = true,
                    IsWordWrapEnabled = false,
                    LabelPlacement = LabelPlacement.LineAboveAlong,
                    LabelPosition = LabelPosition.FixedPositionWithOverlaps,
                    LabelPriority = LabelPriority.Automatic,
                    DuplicateLabels = DuplicateLabels.PreserveDuplicates,
                    TextExpression = "[LENGTH]",
                    MinScale = 500d,
                    Symbol = new TextSymbol
                    {
                        Color = Colors.DarkRed,
                        BorderLineColor = Colors.White,
                        BorderLineSize = 2d,
                        Font =
                            new SymbolFont("Arial", 10.5d, SymbolFontStyle.Normal, SymbolTextDecoration.None,
                                SymbolFontWeight.Normal)
                    }
                }
            };
        }
    }
}
