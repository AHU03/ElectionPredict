using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ElectionPredictFinal.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShellPage : Shell
    {
        public AppShellPage()
        {
            InitializeComponent();
            ContentPage DV = new DataViewer();
            ContentPage PI = new PartyInfluence();
            PredictionModel PM = new PredictionModel();
            PM.LoadAll();
            DataViewerTab.Content = DV;
            PartyInfluenceTab.Content = PI;
            PredicitonModelTab.Content = PM;
        }
    }
}