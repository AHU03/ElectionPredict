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
    public partial class PredictionModel : ContentPage
    {
        private List<Referendum> Referendums = new List<Referendum>();
        private Dictionary<Label, StackLayout> LabelStackLink = new Dictionary<Label, StackLayout>();
        private Dictionary<Party, ThreewaySwitch> PartySwitchStateLink = new Dictionary<Party, ThreewaySwitch>();
        private ThreeLevelSelection areas;
        private Selection type;
        public PredictionModel()
        {
            InitializeComponent();
            LoadSideBar();
            LoadReferendums();
        }
        private void LoadReferendums()
        {
            using (var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetManifestResourceNames().Single(str => str.Contains("modelbasedata.tsv")))))
            {
                while (!reader.EndOfStream)
                {
                    Referendum r = new Referendum(reader.ReadLine());
                    Referendums.Add(r);
                }
            }
        }
        private void LoadSideBar()
        {
            LoadTypeSelector();
            LoadAreaSelector();
            LoadParties();
            Label copydescription = new Label()
            {
                Text = "Alle Daten gemäss dem Swissvotes-Datensatz",
                FontSize = 10
            };
            StackLayout bottommargin = new StackLayout()
            {
                Margin = 10
            };
            bottommargin.Children.Add(copydescription);
            SelectionStackLayout.Children.Add(bottommargin);
        }
        private void LoadTypeSelector()
        {
            Label l = new Label()
            {
                Text = "Rechtsform ▼",
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(0, 5, 0, 0)
            };
            TapGestureRecognizer subtitletap = new TapGestureRecognizer();
            subtitletap.Tapped += SubTitle_Tapped;
            l.GestureRecognizers.Add(subtitletap);
            SelectionStackLayout.Children.Add(l);
            type = new Selection("Rechtsform.txt");
            LabelStackLink.Add(l, type.Stack);
            SelectionStackLayout.Children.Add(type.Stack);

        }
        private void LoadAreaSelector()
        {
            Label l = new Label()
            {
                Text = "Politikbereich ▼",
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(0, 5, 0, 0)
            };
            TapGestureRecognizer subtitletap = new TapGestureRecognizer();
            subtitletap.Tapped += SubTitle_Tapped;
            l.GestureRecognizers.Add(subtitletap);
            SelectionStackLayout.Children.Add(l);
            areas = new ThreeLevelSelection("Politikbereiche.txt");
            LabelStackLink.Add(l, areas.Stack);
            SelectionStackLayout.Children.Add(areas.Stack);
        }
        private void LoadParties()
        {
            using (var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetManifestResourceNames().Single(str => str.Contains("modelparties.txt")))))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line == "$Parteien")
                    {
                        Label l = new Label()
                        {
                            Text = "Parolen ▼",
                            FontSize = 20,
                            FontAttributes = FontAttributes.Bold,
                            Margin = new Thickness(0, 5, 0, 0)
                        };
                        TapGestureRecognizer subtitletap = new TapGestureRecognizer();
                        subtitletap.Tapped += SubTitle_Tapped;
                        l.GestureRecognizers.Add(subtitletap);
                        SelectionStackLayout.Children.Add(l);
                        StackLayout s = new StackLayout();
                        while (true)
                        {
                            line = reader.ReadLine();
                            if (!line.Contains("$"))
                            {
                                Party p = new Party(line);
                                StackLayout framecontainer = new StackLayout();
                                Frame f = new Frame()
                                {
                                    HasShadow = false,
                                    BackgroundColor = Color.FromHex("#282828"),
                                    CornerRadius = 10
                                };
                                Label title = new Label()
                                {
                                    Text = p.partyshorthand
                                };
                                Label subtitle = new Label()
                                {
                                    Text = p.partyname,
                                    FontSize = 14
                                };
                                StackLayout titlelayout = new StackLayout()
                                {
                                    Orientation = StackOrientation.Horizontal
                                };
                                Stream imgstrm = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetManifestResourceNames().Single(str => str.Contains(p.partyname + ".png")))).BaseStream;
                                ImageSource imgsrc = ImageSource.FromStream(() => imgstrm);
                                Image logo = new Image()
                                {
                                    Source = imgsrc,
                                    Aspect = Aspect.AspectFit,
                                    HeightRequest = 20,
                                    VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.StartAndExpand
                                };
                                ThreewaySwitch t = new ThreewaySwitch(new string[] { "Ja", "Keine Auswahl", "Nein" }, p, false)
                                {
                                    activestate = "Keine Auswahl",
                                    BackgroundColor = Color.FromHex("#282828"),
                                    ForegroundColor = Color.Gray,
                                    HorizontalOption = LayoutOptions.StartAndExpand
                                };
                                PartySwitchStateLink.Add(p, t);
                                titlelayout.Children.Add(title);
                                titlelayout.Children.Add(logo);
                                framecontainer.Children.Add(titlelayout);
                                framecontainer.Children.Add(subtitle);
                                framecontainer.Children.Add(t.Switch);
                                f.Content = framecontainer;
                                s.Children.Add(f);
                            }
                            else
                            {
                                break;
                            }
                        }
                        SelectionStackLayout.Children.Add(s);
                        LabelStackLink.Add(l, s);
                    }
                    else
                    {
                    }
                }
            }
        }
        private void SubTitle_Tapped(object sender, EventArgs e)
        {
            Label l = (Label)sender;
            if (l.Text[l.Text.Length - 1] == '▼')
            {
                l.Text = l.Text.Replace('▼', '▲');
                LabelStackLink[l].IsVisible = false;
            }
            else
            {
                l.Text = l.Text.Replace('▲', '▼');
                LabelStackLink[l].IsVisible = true;
            }
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            Dictionary<string, int> partyselections = new Dictionary<string, int>();
            foreach(Party p in PartySwitchStateLink.Keys)
            {
                if(PartySwitchStateLink[p].activestate == "Ja")
                {
                    partyselections.Add(p.partyshorthand, 1);
                }
                if (PartySwitchStateLink[p].activestate == "Nein")
                {
                    partyselections.Add(p.partyshorthand, 2);
                }
            }
            foreach(Referendum r in Referendums)
            {
                r.CalculateSimilarties(Convert.ToInt32(type.selected), areas.selectedarea, partyselections);
            }
        }
    }
}