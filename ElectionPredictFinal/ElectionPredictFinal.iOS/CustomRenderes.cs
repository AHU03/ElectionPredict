using CoreGraphics;
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
[assembly: ExportRenderer(typeof(CustomShell), typeof(CustomShellRenderer))]
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
    public class CustomShellRenderer : ShellRenderer
    {
        protected override IShellSectionRenderer CreateShellSectionRenderer(ShellSection shellSection)
        {
            var renderer = base.CreateShellSectionRenderer(shellSection);

            if (renderer != null)
            {
                if (renderer is ShellSectionRenderer shellRenderer)
                {


                    var appearance = new UINavigationBarAppearance();
                    appearance.ConfigureWithOpaqueBackground();
                    appearance.BackgroundColor = new UIColor(0.08235294117647058823529411764706f, 0.08235294117647058823529411764706f, 0.08235294117647058823529411764706f, 1f);

                    appearance.TitleTextAttributes = new UIStringAttributes() { ForegroundColor = UIColor.White };


                    shellRenderer.NavigationBar.Translucent = false;
                    shellRenderer.NavigationBar.StandardAppearance = appearance;
                    shellRenderer.NavigationBar.ScrollEdgeAppearance = shellRenderer.NavigationBar.StandardAppearance;

                }
            }

            return renderer;
        }

        protected override IShellItemRenderer CreateShellItemRenderer(ShellItem item)
        {
            var renderer = base.CreateShellItemRenderer(item);

            if (renderer != null)
            {
                if (renderer is ShellItemRenderer shellItemRenderer)
                {
                    var appearance = new UITabBarAppearance();

                    appearance.ConfigureWithOpaqueBackground();
                    appearance.BackgroundColor = new UIColor(0.08235294117647058823529411764706f, 0.08235294117647058823529411764706f, 0.08235294117647058823529411764706f, 1f);
                    shellItemRenderer.TabBar.Translucent = false;
                    shellItemRenderer.TabBar.StandardAppearance = appearance;

                }


            }

            return renderer;
        }
    }
}