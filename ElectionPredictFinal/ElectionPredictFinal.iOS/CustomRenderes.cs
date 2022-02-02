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
[assembly: ExportRenderer(typeof(Shell), typeof(CustomShellRenderer))]
[assembly: ExportRenderer(typeof(ListView), typeof(CustomListViewRenderer))]
namespace ElectionPredictFinal.iOS
{
    //All of these were copied from StackOverflow while searching for bugfixes with only minimal adjustments
    public class CustomListViewRenderer : ListViewRenderer
    {
        public CustomListViewRenderer()
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);
            if(Convert.ToInt32(UIDevice.CurrentDevice.SystemVersion.Split('.')[0]) >= 15)
            {
                if (Control != null)
                {
                    DoTheDumbShit();
                }
            }
        }
        //The Statement contained in this method is necessary for this not to break on iOS 15.
        //The Method being separate is necessary for this not to break on iOS 14 and below.
        //FML
        private void DoTheDumbShit()
        {
            Control.SectionHeaderTopPadding = new nfloat(0);
        }
    }
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
                    shellRenderer.NavigationBar.ScrollEdgeAppearance = appearance;

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
                    shellItemRenderer.TabBar.StandardAppearance.ConfigureWithOpaqueBackground();
                    shellItemRenderer.TabBar.Translucent = false;
                }


            }

            return renderer;
        }
    }
}