﻿using ElectionPredictFinal.Pages.Classes;
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
        Dictionary<Party, Label> listpartylink = new Dictionary<Party, Label>();
        Dictionary<Label, List<Button>> subtitleorder = new Dictionary<Label, List<Button>>();
        public static ObservableDictionary<Party, List<Vote>> activevotes = new ObservableDictionary<Party, List<Vote>>();
        List<Party> selectableparties = new List<Party>();
        List<Party> activeparties = new List<Party>();
        List<Party> visibleselectableparties = new List<Party>();

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
                if(commonlist.Count == 0)
                {
                    MainLabel.Text = "Keine Daten verfügbar";
                    MainLabelDescription.Text = "";
                    BasedOnLabel.Text = "";
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
                    Label1.Text = Convert.ToString(Math.Round((double)highestpercentageyes)) + "%";
                    Label2.Text = Convert.ToString(Math.Round((double)lowestpercentageyes)) + "%";
                    Label3.Text = Convert.ToString(Math.Round(((double)totalpercentageyes) / ((double)(commonlist.Count)))) + "%";
                    Label4.Text = Convert.ToString(Math.Round(((double)totalpartystrengths) / ((double)(commonlist.Count)))) + "%";
                    BasedOnLabel.Text = "Zahlen basieren auf " + commonlist.Count + " Abstimmungen von " + commonlist[0].year + " bis " + commonlist[commonlist.Count - 1].year + ".";
                    MainLabelDescription.Text = "Anteil der angenommenen Abstimmungen";
                    SideStackStart.WidthRequest = 150;
                    SideStackEnd.WidthRequest = 150;
                    Label1.IsVisible = true;
                    Label2.IsVisible = true;
                    Label3.IsVisible = true;
                    Label4.IsVisible = true;
                    Label1Description.IsVisible = true;
                    Label2Description.IsVisible = true;
                    Label3Description.IsVisible = true;
                    Label4Description.IsVisible = true;
                    BasedOnLabel.IsVisible = true;
                    MainLabelDescription.IsVisible = true;
                }
            }
            else
            {
                MainLabel.Text = "Bitte Daten auswählen";
                MainLabelDescription.Text = "";
                BasedOnLabel.Text = "";
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
            foreach (Party p in activeparties)
            {
                Label l = listpartylink[p];
                StringBuilder outputstring = new StringBuilder();
                if (activevotes.ContainsKey(p))
                {
                    outputstring.Append("\n");
                    foreach (Vote v in activevotes[p])
                    {
                        outputstring.Append(v.year + ": " + v.title);
                        if(v.numsections > 0)
                        {
                            outputstring.Append(", Abweichende Sektionen: " + v.sections);
                        }
                        outputstring.Append("\n\n");
                    }
                }
                l.Text = outputstring.ToString();
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
                    string line = reader.ReadLine();
                    if (line[0] == '$')
                    {
                        Label l = new Label()
                        {
                            Text = line.Remove(0, 1) + " ▼",
                            FontSize = 20,
                            FontAttributes = FontAttributes.Bold,
                            Margin = new Thickness(0, 5, 0, 0),
                            TextColor = Color.FloralWhite
                        };
                        TapGestureRecognizer subtitletap = new TapGestureRecognizer();
                        subtitletap.Tapped += SubTitle_Tapped;
                        l.GestureRecognizers.Add(subtitletap);
                        subtitleorder.Add(l, new List<Button>());
                        SelectionStackLayout.Children.Add(l);
                    }
                    else
                    {
                        Party p = new Party(line);
                        Button b = CreateSelectableParty(p);
                        buttonpartylink.Add(b, p);
                        subtitleorder[subtitleorder.Keys.Last()].Add(b);
                        SelectionStackLayout.Children.Add(b);
                    }

                }
            }
            Label copydescription = new Label()
            {
                Text = "Alle Daten gemäss dem Swissvotes-Datensatz",
                FontSize = 10,
                TextColor = Color.FloralWhite
            };
            StackLayout bottommargin = new StackLayout()
            {
                Margin = 10
            };
            bottommargin.Children.Add(copydescription);
            SelectionStackLayout.Children.Add(bottommargin);
        }
        private void SubTitle_Tapped(object sender, EventArgs e)
        {
            Label l = (Label)sender;
            if(l.Text[l.Text.Length - 1] == '▼')
            {
                l.Text = l.Text.Replace('▼', '▲');
                foreach(Button b in subtitleorder[l])
                {
                    b.IsVisible = false;
                    visibleselectableparties.Remove(buttonpartylink[b]);
                }
            }
            else
            {
                l.Text = l.Text.Replace('▲', '▼');
                foreach (Button b in subtitleorder[l])
                {
                    if (selectableparties.Contains(buttonpartylink[b]))
                    {
                        b.IsVisible = true;
                        visibleselectableparties.Add(buttonpartylink[b]);
                    }

                }
            }
        }
        private Button CreateSelectableParty(Party p)
        {
            selectableparties.Add(p);
            visibleselectableparties.Add(p);
            Button b = new MultilineButton()
            {
                Text = p.partyname,
                FontSize = 14,
                TextColor = Color.FloralWhite,
                HeightRequest = 50,
                BackgroundColor = Color.FromHex("#282828"),
                FontFamily = "Menlo",
                CornerRadius = 10
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
        private void CheckSubtitles()
        {
            foreach(Label l in subtitleorder.Keys)
            {
                bool shouldbevisible = false;
                foreach(Button b in subtitleorder[l])
                {
                    if (selectableparties.Contains(buttonpartylink[b]))
                    {
                        shouldbevisible = true;
                    }
                }
                if(l.IsVisible != shouldbevisible)
                {
                    l.IsVisible = shouldbevisible;
                }
            }
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
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Start,
                Margin = new Thickness(0, -10, 0, 0),
                TextColor = Color.FloralWhite
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
                HorizontalOptions = LayoutOptions.Start,
                TextColor = Color.FloralWhite
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
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.Center
            };
            Stream imgstrm = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetManifestResourceNames().Single(str => str.Contains(p.partyname + ".png")))).BaseStream;
            ImageSource imgsrc = ImageSource.FromStream(() => imgstrm);
            Image logo = new Image()
            {
                Source = imgsrc,
                Aspect = Aspect.AspectFit,
                HeightRequest = 20,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Margin = new Thickness(0,-10,0,0)
            };
            Label listlabel = new Label()
            {
                FontSize = 12,
                TextColor = Color.FloralWhite
            }
            ;
            ScrollView sv = new ScrollView()
            {
                Content = listlabel
            };
            listpartylink.Add(p, listlabel);
            topstack.Children.Add(shorth);
            topstack.Children.Add(logo);
            topstack.Children.Add(CloseButton);
            s.Children.Add(topstack);
            s.Children.Add(l);
            ThreewaySwitch t = new ThreewaySwitch(new string[] { "Ja", "Neutral", "Nein" }, p, true);
            Frame Options = t.Switch;
            Options.HorizontalOptions = LayoutOptions.Start;
            s.Children.Add(Options);
            s.Children.Add(sv);
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
            framepartylink.Remove((Frame)((Button)sender).Parent.Parent.Parent);
            if (visibleselectableparties.Contains(buttonpartylink[(Button)b]))
            {
                ((Button)b).IsVisible = true;
            }
            CheckSubtitles();
        }
        private void SelectableButtonClicked(object sender, EventArgs args)
        {
            CreateActiveParty((Button)sender, buttonpartylink[(Button)sender]);
            CheckSubtitles();
        }
    }
}