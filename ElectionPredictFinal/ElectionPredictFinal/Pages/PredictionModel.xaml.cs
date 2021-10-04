using ElectionPredictFinal.Pages.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ElectionPredictFinal.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PredictionModel : ContentPage
    {
        private List<Referendum> Referendums = new List<Referendum>();
        private List<Canton> Cantons = new List<Canton>();
        private Dictionary<Label, StackLayout> LabelStackLink = new Dictionary<Label, StackLayout>();
        private Dictionary<Party, ThreewaySwitch> PartySwitchStateLink = new Dictionary<Party, ThreewaySwitch>();
        private ThreeLevelSelection areas;
        private Selection type;
        private Selection year;
        private SimResults Simulation;
        public PredictionModel()
        {
            InitializeComponent();
        }
        public void LoadAll()
        {
            LoadSideBar();
            LoadReferendums();
            LoadCantons();
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
        private void LoadCantons()
        {
            using (var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetManifestResourceNames().Single(str => str.Contains("cantons.tsv")))))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine().Split('\t');
                    Cantons.Add(new Canton(line[0], line[1], Convert.ToDouble(line[2])));
                }
            }
            foreach(Canton c in Cantons)
            {
                foreach(Referendum r in Referendums)
                {
                    c.AddReferendum(r);
                }
            }
            foreach (Canton c in Cantons)
            {
                c.CantonsCorrelations(Cantons);
            }
        }
        private void LoadSideBar()
        {
            LoadYearSelector();
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
                Text = "Rechtsform ▲",
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(0, 5, 0, 0)
            };
            TapGestureRecognizer subtitletap = new TapGestureRecognizer();
            subtitletap.Tapped += SubTitle_Tapped;
            l.GestureRecognizers.Add(subtitletap);
            SelectionStackLayout.Children.Add(l);
            type = new Selection("Rechtsform.txt");
            type.Stack.IsVisible = false;
            LabelStackLink.Add(l, type.Stack);
            SelectionStackLayout.Children.Add(type.Stack);
        }
        private void LoadYearSelector()
        {
            List<string> years = new List<string>();
            for(int i = DateTime.Now.Year; i >= 1866; i--)
            {
                years.Add(Convert.ToString(i));
            }
            Label l = new Label()
            {
                Text = "Jahr ▲",
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(0, 5, 0, 0)
            };
            TapGestureRecognizer subtitletap = new TapGestureRecognizer();
            subtitletap.Tapped += SubTitle_Tapped;
            l.GestureRecognizers.Add(subtitletap);
            SelectionStackLayout.Children.Add(l);
            year = new Selection(years);
            year.Stack.IsVisible = false;
            LabelStackLink.Add(l, year.Stack);
            SelectionStackLayout.Children.Add(year.Stack);
        }
        private void LoadAreaSelector()
        {
            Label l = new Label()
            {
                Text = "Politikbereich ▲",
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(0, 5, 0, 0)
            };
            TapGestureRecognizer subtitletap = new TapGestureRecognizer();
            subtitletap.Tapped += SubTitle_Tapped;
            l.GestureRecognizers.Add(subtitletap);
            SelectionStackLayout.Children.Add(l);
            areas = new ThreeLevelSelection("Politikbereiche.txt");
            areas.Stack.IsVisible = false;
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
                            Text = "Parolen ▲",
                            FontSize = 20,
                            FontAttributes = FontAttributes.Bold,
                            Margin = new Thickness(0, 5, 0, 0)
                        };
                        TapGestureRecognizer subtitletap = new TapGestureRecognizer();
                        subtitletap.Tapped += SubTitle_Tapped;
                        l.GestureRecognizers.Add(subtitletap);
                        SelectionStackLayout.Children.Add(l);
                        StackLayout s = new StackLayout()
                        {
                            IsVisible = false
                        };
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
        private async void Button_Clicked(object sender, EventArgs e)
        {
            MainButton.IsEnabled = false;
            ResultsStack.IsVisible = false;
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
            foreach (Referendum r in Referendums)
            {
                r.CalculateSimilarties(Convert.ToInt32(type.selectedindex), areas.selectedarea, year.selectedstring, partyselections);
            }
            foreach (Canton c in Cantons)
            {
                c.ReferendumDistribution(Referendums);
            }
            await MonteCarloSimulation();
        }
        private async Task MonteCarloSimulation()
        {
            LoadingStack.IsVisible = true;
            SimResults Sim = new SimResults(Cantons, type.selectedindex == "1" || type.selectedindex == "3" || type.selectedindex == "4");
            for(int i = 0; i < 2500; i++)
            {
                double p = Convert.ToDouble(i) / 2500.0;
                LoadingLabel.Text = String.Format("{0:0.00}%",p * 100);
                LoadingFrame.WidthRequest = p * (LoadingStack.Width);
                await Task.Run(()=>Sim.RunSim(1));
            }
            MainButton.IsEnabled = true;
            LoadingStack.IsVisible = false;
            LoadingFrame.WidthRequest = 0;
            ResultsStack.IsVisible = true;
            Simulation = Sim;
            CreateResultsStack();
        }
        private void CreateResultsStack()
        {
            List<View> enumlist = ResultsStack.Children.ToList<View>();
            foreach(View v in enumlist)
            {
                v.IsVisible = false;
                ResultsStack.Children.Remove(v);
            }
            Label title = new Label()
            {
                Text = "Resultate",
                FontAttributes = FontAttributes.Bold,
                FontSize = 36,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start
            };
            ResultsStack.Children.Add(title);
            StackLayout numsStack = new StackLayout();
            Frame numsFrame = new Frame()
            {
                BackgroundColor = Color.FromHex("#151515"),
                Content = numsStack,
                HasShadow = false,
                CornerRadius = 20,
                WidthRequest = 600
            };
            Label numstitle = new Label()
            {
                Text = "Kenndaten",
                FontAttributes = FontAttributes.Bold,
                FontSize = 20
            };
            Graph g = new Graph(Simulation.distribution, 0.05, "Verteilung der Volksmehrheiten");
            ResultsStack.Children.Add(g.Stack);
            StringBuilder infostring = new StringBuilder();
            infostring.Append(String.Format("Durchschnittler Ja-Anteil: {0:0.00}%", Simulation.meanpopvote*100));
            if (type.selectedindex == "1" || type.selectedindex == "3" || type.selectedindex == "4")
            {
                infostring.Append(String.Format("\nMittelwert der Ja-Stände: {0:0.0}", Simulation.meanstände));
            }
            infostring.Append(String.Format("\nAnteil der Simulationen mit angenommener Vorlage: {0:0.00}%", Simulation.percentageyes*100));
            infostring.Append("\nVertrauen in die Vorhersage: " +Cantons[0].confidence);
            infostring.Append("\nÄhnlichste gefundene Vorlagen:");
            Referendum[] rs = TopRefs();
            for(int i = 1; i < 6; i++)
            {
                infostring.Append("\n\t" + i + ". " + rs[i - 1].title + ", " + rs[i - 1].year);
            }
            Label numsbody = new Label()
            {
                Text = infostring.ToString(),
                FontSize = 13
            };
            numsStack.Children.Add(numstitle);
            numsStack.Children.Add(numsbody);
            numsFrame.Content = numsStack;
            ResultsStack.Children.Add(numsFrame);
        }
        private Referendum[] TopRefs()
        {
            Referendum[] rs = new Referendum[5] { Referendums[0], Referendums[0], Referendums[0], Referendums[0], Referendums[0] };
            foreach (Referendum r in Referendums)
            {
                for (int i = 0; i < rs.Length; i++)
                {
                    if (r.similarity * r.direction > rs[i].similarity * rs[i].direction)
                    {
                        if (i != 4)
                        {
                            if (!(r.similarity * r.direction > rs[i + 1].similarity * rs[i + 1].direction))
                            {
                                rs[i] = r;
                                break;
                            }
                        }
                        else
                        {
                            rs[i] = r;
                            break;
                        }
                    }
                }
            }
            return rs;
        }
    }
}