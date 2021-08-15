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
                int adoptednum = 0;
                foreach (Vote v in commonlist)
                {
                    if (v.adopted == "Angenommen")
                    {
                        adoptednum++;
                    }
                }
                MainLabel.Text = Convert.ToString(Math.Round(((double)adoptednum) / ((double)(commonlist.Count)) * 100.00)) + "%";
            }
            else
            {
                MainLabel.Text = "Bitte Daten auswählen";
            }
        }
        private List<Vote> CommonVotes(List<List<Vote>> mainlist)
        {
            List<Vote> returnlist = mainlist[0];
            foreach (List<Vote> l in mainlist)
            {
                List<int> tempintlist = new List<int>();
                foreach(Vote v in l)
                {
                    tempintlist.Add(v.index);
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
            return returnlist;
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
            topstack.Children.Add(shorth);
            topstack.Children.Add(CloseButton);
            s.Children.Add(topstack);
            s.Children.Add(l);
            ThreewaySwitch t = new ThreewaySwitch(new string[] { "Ja", "Neutral", "Nein" }, p);
            Frame Options = t.Switch;
            Options.HorizontalOptions = LayoutOptions.Start;
            s.Children.Add(Options);
            f.Content = s;
            return f;
        }
        private void CloseButtonClicked(object sender, EventArgs args)
        {
            if (PartyInfluence.activevotes.ContainsKey(framepartylink[(Frame)((Button)sender).Parent.Parent.Parent]))
            {
                PartyInfluence.activevotes.Remove(framepartylink[(Frame)((Button)sender).Parent.Parent.Parent]);
            }
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