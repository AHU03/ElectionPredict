using SkiaSharp.Extended.Svg;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace ElectionPredictFinal.Pages.Classes
{
    class CantonRender
    {
        private SKSvg sksvg = new SKSvg();
        private string mysvgdata = "";
        private SKCanvasView mypanel = new SKCanvasView()
        {
            WidthRequest = 200,
            HeightRequest = 200,
            VerticalOptions = LayoutOptions.Center
        };
        public CantonRender(Canton c)
        {
            Dictionary<double, string> colorstrings = new Dictionary<double, string>()
            {
                {1.01, "#6bcffe"},
                {0.90, "#a4dcf4"},
                {0.60, "#969696"},
                {0.40, "#fbbbc3"},
                {0.10, "#f19a9c"}
            };
            mysvgdata = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetManifestResourceNames().Single(str => str.Contains(c.shorthand.ToUpper()+".svg")))).ReadToEnd().ToLower(); ;
            string selcolor = "";
            foreach(double d in colorstrings.Keys)
            {
                if(d > 1 - c.distribution.CumulativeDistribution(0.5))
                {
                    selcolor = colorstrings[d];
                }
            }
            byte[] byteArray = Encoding.ASCII.GetBytes(mysvgdata.Replace("#000000", selcolor));
            Stream output = new MemoryStream(byteArray);
            sksvg.Load(output);
            mypanel.PaintSurface += mappanel_PaintSurface;
            mypanel.InvalidateSurface(); //Calls PaintSurface-Function
        }
        private void mappanel_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            try
            {
                e.Surface.Canvas.DrawPicture(sksvg.Picture);
            }
            catch { }
        }
        public SKCanvasView map
        {
            get { return mypanel; }
        }
    }
}
