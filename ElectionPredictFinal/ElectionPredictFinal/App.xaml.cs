using ElectionPredictFinal.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ElectionPredictFinal
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new Pages.AppShellPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
