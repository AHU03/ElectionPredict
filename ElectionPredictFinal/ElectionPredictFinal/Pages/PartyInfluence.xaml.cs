using ElectionPredictFinal.Pages.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ElectionPredictFinal.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PartyInfluence : ContentPage
    {
        public PartyInfluence()
        {
            InitializeComponent();
            LoadParties();
        }
        private Dictionary<Button, Party> buttonpartylink = new Dictionary<Button, Party>(); 
        private void LoadParties()
        {
            using (var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetManifestResourceNames().Single(str => str.Contains("partyindex.txt")))))
            {
                while (!reader.EndOfStream)
                {
                    Party p = new Party(reader.ReadLine());
                    Button b = CreateSelectableParty(p);
                    buttonpartylink.Add(b, p);
                    SelectionStackLayout.Children.Add(b);
                }
            }
        }
        private Button CreateSelectableParty(Party p)
        {
            Button b = new MultilineButton()
            {
                Text = p.partyname,
                FontSize = 14,
                TextColor = Color.FloralWhite,
                BackgroundColor = Color.FromHex("#282828"),
                FontFamily = "Menlo"
            };
            b.Clicked += SelectableButtonClicked;
            return b;
        }
        private void CreateActiveParty(Button b, Party p)
        {
            b.IsVisible = false;
        }
        void SelectableButtonClicked(object sender, EventArgs args)
        {
            CreateActiveParty((Button)sender, buttonpartylink[(Button)sender]);
        }
    }
    public class MultilineButton : Button
    {

    }
}