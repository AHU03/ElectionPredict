using ElectionPredictFinal.iOS;
using ElectionPredictFinal.Pages.Classes;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MultilineButton), typeof(MultilineButtonRenderer))]
namespace ElectionPredictFinal.iOS
{

    public class MultilineButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            Control.TitleEdgeInsets = new UIEdgeInsets(4, 4, 4, 4);
            Control.TitleLabel.LineBreakMode = UILineBreakMode.WordWrap;
            Control.TitleLabel.TextAlignment = UITextAlignment.Center;
        }
    }
}