using ElectionPredictFinal.Pages.Classes;
using NSwag.Collections;
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
        Dictionary<Button, Party> buttonpartylink = new Dictionary<Button, Party>();
        Dictionary<Frame, Party> framepartylink = new Dictionary<Frame, Party>();
        Dictionary<Party, ListView> listpartylink = new Dictionary<Party, ListView>();
        public static ObservableDictionary<Party, List<Vote>> activevotes = new ObservableDictionary<Party, List<Vote>>();
        List<Party> selectableparties = new List<Party>();
        List<Party> activeparties = new List<Party>();

        public PartyInfluence()
        {
            InitializeComponent();
            LoadParties();
            PartyInfluence.activevotes.CollectionChanged += Activevotes_CollectionChanged;
        }

        private void Activevotes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(PartyInfluence.activevotes.Count > 0)
            {
                MainLabelDescription.IsVisible = true;
                List<List<Vote>> activevoteslist = new List<List<Vote>>();
                foreach(var l in PartyInfluence.activevotes.Values)
                {
                    List<Vote> templist = new List<Vote>();
                    foreach(var v in l)
                    {
                        templist.Add(v);
                    }
                    activevoteslist.Add(templist);
                }
                List<Vote> commonlist = CommonVotes(activevoteslist);
                if(commonlist.Count == 0)
                {
                    MainLabel.Text = "Keine Daten verfügbar";
                    MainLabel.IsVisible = true;
                    MainLabelDescription.IsVisible = false;
                    BasedOnLabel.IsVisible = false;
                    Label1.IsVisible = false;
                    Label2.IsVisible = false;
                    Label3.IsVisible = false;
                    Label4.IsVisible = false;
                    Label1Description.IsVisible = false;
                    Label2Description.IsVisible = false;
                    Label3Description.IsVisible = false;
                    Label4Description.IsVisible = false;
                    SideStackStart.WidthRequest = 0;
                    SideStackEnd.WidthRequest = 0;
                }
                else
                {
                    int adoptednum = 0;
                    double totalpartystrengths = 0;
                    double totalpercentageyes = 0;
                    double lowestpercentageyes = commonlist[0].percentageyes;
                    double highestpercentageyes = commonlist[0].percentageyes;
                    foreach (Vote v in commonlist)
                    {
                        if (v.percentageyes > highestpercentageyes)
                        {
                            highestpercentageyes = v.percentageyes;
                        }
                        if (v.percentageyes < lowestpercentageyes)
                        {
                            lowestpercentageyes = v.percentageyes;
                        }
                        totalpartystrengths += v.partystrength;
                        totalpercentageyes += v.percentageyes;
                        if (v.adopted == "Angenommen")
                        {
                            adoptednum++;
                        }
                    }
                    MainLabel.Text = Convert.ToString(Math.Round(((double)adoptednum) / ((double)(commonlist.Count)) * 100.00)) + "%";
                    BasedOnLabel.Text = "Zahlen basieren auf "+ commonlist.Count +" Abstimmungen von " + commonlist[0].year + " bis " + commonlist[commonlist.Count -1].year+".";
                    Label1.Text = Convert.ToString(Math.Round((double)highestpercentageyes)) + "%";
                    Label2.Text = Convert.ToString(Math.Round((double)lowestpercentageyes)) + "%";
                    Label3.Text = Convert.ToString(Math.Round(((double)totalpercentageyes) / ((double)(commonlist.Count)))) + "%";
                    Label4.Text = Convert.ToString(Math.Round(((double)totalpartystrengths) / ((double)(commonlist.Count)))) + "%";
                    BasedOnLabel.IsVisible = true;
                    Label1.IsVisible = true;
                    Label2.IsVisible = true;
                    Label3.IsVisible = true;
                    Label4.IsVisible = true;
                    Label1Description.IsVisible = true;
                    Label2Description.IsVisible = true;
                    Label3Description.IsVisible = true;
                    Label4Description.IsVisible = true;
                    SideStackStart.WidthRequest = 150;
                    SideStackEnd.WidthRequest = 150;
                }
            }
            else
            {
                MainLabel.Text = "Bitte Daten auswählen";
                MainLabelDescription.IsVisible = false;
                BasedOnLabel.IsVisible = false;
                Label1.IsVisible = false;
                Label2.IsVisible = false;
                Label3.IsVisible = false;
                Label4.IsVisible = false;
                Label1Description.IsVisible = false;
                Label2Description.IsVisible = false;
                Label3Description.IsVisible = false;
                Label4Description.IsVisible = false;
                SideStackStart.WidthRequest = 0;
                SideStackEnd.WidthRequest = 0;
            }
            foreach (Party p in activeparties)
            {
                try
                {
                    ListView lv = listpartylink[p];
                    List<string> votenames = new List<string>();
                    foreach (Vote v in activevotes[p])
                    {
                        votenames.Add(v.title);
                    }
                    lv.ItemsSource = votenames.ToArray();
                }
                catch { Console.WriteLine(p.partyname); }
            }
        }
        private List<Vote> CommonVotes(List<List<Vote>> mainlist)
        {
            List<Vote> returnlist = mainlist[0];
            Dictionary<int, double> strengths = new Dictionary<int, double>();
            foreach (Vote v in returnlist)
            {
                v.partystrength = 0.00;
                strengths.Add(v.index, 0);
            }
            foreach (List<Vote> l in mainlist)
            {
                List<int> tempintlist = new List<int>();
                foreach(Vote v in l)
                {
                    tempintlist.Add(v.index);
                    if (strengths.ContainsKey(v.index))
                    {
                        strengths[v.index] += v.partystrength * (double)Convert.ToInt32((bool)(v.endorsement == "Ja"));
                    }
                }
                List<Vote> templist = new List<Vote>();
                foreach(Vote v in returnlist)
                {
                    if (tempintlist.Contains(v.index))
                    {
                        templist.Add(v);
                    }
                }
                returnlist.Clear();
                returnlist = templist;
            }
            List<Vote> newreturnlist = new List<Vote>();
            foreach (Vote v in returnlist)
            {
                newreturnlist.Add(new Vote(v.index, v.title, v.year, v.domain, v.adopted, v.percentageyes, strengths[v.index], null, 0, null));
            }
            return newreturnlist;
        }

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
            selectableparties.Add(p);
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
            selectableparties.Remove(p);
            activeparties.Add(p);
            Frame activeparty = ActivePartyFrame(p);
            framepartylink.Add(activeparty, p);
            ActivePartiesStackLayout.Children.Add(activeparty);
        }
        private Frame ActivePartyFrame(Party p)
        {
            Frame f = new Frame
            {
                BackgroundColor = Color.FromHex("151515"),
                HasShadow = false,
                WidthRequest = 200,
                HeightRequest = 400,
                CornerRadius = 20
            };
            Label shorth = new Label()
            {
                Text = p.partyshorthand,
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Margin = new Thickness(0, -10, 0, 0)
            };
            StackLayout s = new StackLayout()
            {
                Orientation = StackOrientation.Vertical
            };
            Label l = new Label()
            {
                Text = p.partyname,
                FontSize = 14,
                HorizontalTextAlignment = TextAlignment.Start,
                HorizontalOptions = LayoutOptions.Start
            };
            Button CloseButton = new Button()
            {
                Text = "X",
                TextColor = Color.FloralWhite,
                BackgroundColor = Color.FromHex("#282828"),
                WidthRequest = 30,
                HeightRequest = 30,
                HorizontalOptions = LayoutOptions.End,
                Padding = 0,
                CornerRadius = 15,
                Margin = new Thickness(0,-10,-10, 0)
            };
            CloseButton.Clicked += CloseButtonClicked;
            StackLayout topstack = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal
            };
            ListView lv = new ListView()
            {
                ItemsSource = new string[] { },
                SelectionMode = ListViewSelectionMode.None,
                BackgroundColor = Color.FromHex("#151515"),
                SeparatorVisibility = SeparatorVisibility.None,
                SeparatorColor = Color.FromHex("#151515"),
                Margin = 0
            };
            listpartylink.Add(p, lv);
            topstack.Children.Add(shorth);
            topstack.Children.Add(CloseButton);
            s.Children.Add(topstack);
            s.Children.Add(l);
            ThreewaySwitch t = new ThreewaySwitch(new string[] { "Ja", "Neutral", "Nein" }, p);
            Frame Options = t.Switch;
            Options.HorizontalOptions = LayoutOptions.Start;
            s.Children.Add(Options);
            s.Children.Add(lv);
            f.Content = s;
            return f;
        }
        private void CloseButtonClicked(object sender, EventArgs args)
        {
            Party p = framepartylink[(Frame)((Button)sender).Parent.Parent.Parent];
            selectableparties.Add(p);
            activeparties.Remove(p);
            PartyInfluence.activevotes.Remove(framepartylink[(Frame)((Button)sender).Parent.Parent.Parent]);
            listpartylink.Remove(p);
            ActivePartiesStackLayout.Children.Remove((Frame)((Button)sender).Parent.Parent.Parent);
            var b = buttonpartylink.FirstOrDefault(x => x.Value == framepartylink[(Frame)((Button)sender).Parent.Parent.Parent]).Key;
            ((Button)b).IsVisible = true;
            framepartylink.Remove((Frame)((Button)sender).Parent.Parent.Parent);
        }
        private void SelectableButtonClicked(object sender, EventArgs args)
        {
            CreateActiveParty((Button)sender, buttonpartylink[(Button)sender]);
        }
    }
}