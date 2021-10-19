using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms.Svg;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace ElectionPredictFinal.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataViewer : ContentPage
    {
        public DataViewer()
        {
            InitializeComponent();
            LoadCountries();
        }
        public List<View> GeneratedControls = new List<View>();
        public Stream Optionssource = Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetManifestResourceNames().Single(str => str.EndsWith("usoption.tsv")));
        public string countrysourcestring;
        public SkiaSharp.Extended.Svg.SKSvg sksvg = new SkiaSharp.Extended.Svg.SKSvg();
        private void MainLoadFunc()
        {
            ClearDisplay();
            string source = Convert.ToString(HistoricalListView.SelectedItem) + Convert.ToString(HistoricalYearsListView.SelectedItem);
            string sourcecompare = Convert.ToString(CompareListView.SelectedItem) + Convert.ToString(CompareYearsListView.SelectedItem);
            if (Convert.ToString(HistoricalYearsListView.SelectedItem).Length < 1)
            {
                ErrorLabel.Text = "Bitte Daten auswählen.";
                return;
            }
            if (CompareGroup.IsVisible && Convert.ToString(CompareYearsListView.SelectedItem).Length < 1)
            {
                ErrorLabel.Text = "Bitte Daten unter \"Vergleichen\" auswählen.";
                return;
            }
            int shift = 0;
            if (ShiftResultsHistoricalFrame.IsVisible == true)
            {
                shift = (int)Math.Round(ShiftResultsTrackBar.Value);
            }
            int shiftcompare = 0;
            if (ShiftResultsCompareFrame.IsVisible == true)
            {
                shiftcompare = (int)Math.Round(ShiftResultsTrackBarCompare.Value);
            }
            try
            {
                ErrorLabel.Text = "";
                if (CompareGroup.IsVisible && SourceFormatType(source).Contains("MapVis"))
                {
                    string voteslabel = MetaExtract(sourcecompare)[0] + " verglichen mit " + MetaExtract(source)[0];
                    bool popvote = false;
                    if (SourceFormatType(source).Contains("PopVote"))
                    {
                        voteslabel = "Stände";
                        popvote = true;
                    }
                    SortedDictionary<string, string[]> compareddict = CreateComparedDictionary(CreateDictionary(source, shift, popvote), CreateDictionary(sourcecompare, shiftcompare, popvote));
                    if (ErrorLabel.Text == "")
                    {
                        DrawMap(GetSvgFromDirectory(MetaExtract(source)[3]), compareddict, new List<Xamarin.Forms.Color>(GetGradient(MetaExtract(source)[4]).Concat(GetGradient("colorgradientrandom.png"))), ColorComparedCategories(source, sourcecompare, compareddict));
                        double[] splitvotes = SplitVote(source, shift);
                        DrawElectoralVotes(true, false, true, splitvotes, compareddict, ColorComparedCategories(source, sourcecompare, compareddict), new List<Xamarin.Forms.Color>(GetGradient(MetaExtract(source)[4]).Concat(GetGradient("colorgradientrandom.png"))), MainParties(source), voteslabel, "", "");
                        DrawKeyBox(compareddict, ColorComparedCategories(source, sourcecompare, compareddict), new List<Xamarin.Forms.Color>(GetGradient(MetaExtract(source)[4]).Concat(GetGradient("colorgradientrandom.png"))));
                        MetaLabeling(TitleShiftModifier(MetaExtract(sourcecompare)[2] + ", " + MetaExtract(sourcecompare)[1], sourcecompare, shiftcompare) + " -> " + TitleShiftModifier(MetaExtract(source)[2] + ", " + MetaExtract(source)[1], source, shift), "Vergleich, Balken oben zeigt Anzahl der Staaten in jeder Kategorie");
                    }
                }
                else if (SourceFormatType(source).Contains("MapVis"))
                {
                    string voteslabel = MetaExtract(source)[0];
                    bool popvote = false;
                    if (SourceFormatType(source).Contains("PopVote"))
                    {
                        voteslabel = "Stände";
                        popvote = true;
                    }
                    SortedDictionary<string, string[]> dict = CreateDictionary(source, shift, popvote);
                    if (ErrorLabel.Text == "")
                    {
                        MetaLabeling(TitleShiftModifier(MetaExtract(source)[2] + ", " + MetaExtract(source)[1], source, shift), MetaExtract(source)[5]);
                        DrawMap(GetSvgFromDirectory(MetaExtract(source)[3]), dict, GetGradient(MetaExtract(source)[4]), ColorCategories(source));
                        DrawElectoralVotes(true, false, false, new double[] { }, dict, ColorCategories(source), GetGradient(MetaExtract(source)[4]), MainParties(source), voteslabel, "", "");
                        if (popvote)
                        {
                            double[] splitvotes = SplitVote(source, shift);
                            DrawElectoralVotes(true, true, false, splitvotes, dict, ColorCategories(source), GetGradient(MetaExtract(source)[4]), MainParties(source), "Stimmen", "%", "%");
                        }
                        DrawKeyBox(dict, ColorCategories(source), GetGradient(MetaExtract(source)[4]));
                    }
                }
                else if (SourceFormatType(source).Contains("StatVis"))
                {
                    CreateMultipleBars(CreateStatDictionary(source), ColorCategories(source), GetGradient(MetaExtract(source)[4]), MainParties(source));
                    MetaLabeling(MetaExtract(source)[2] + ", " + MetaExtract(source)[1] + ", " + MetaExtract(source)[0], MetaExtract(source)[5]);
                    SortedDictionary<string, string[]> data = new SortedDictionary<string, string[]>();
                    foreach (string s in ColorCategories(source))
                    {
                        data.Add(s, new string[] { s });
                    }
                    DrawKeyBox(data, ColorCategories(source), GetGradient(MetaExtract(source)[4]));
                }
                else if (SourceFormatType(source).Contains("TableVis"))
                {
                    Dictionary<string, string[]> dict = CreateTableDicitonary(source);
                    DrawElectoralVotes(false, false, true, new double[] { }, new SortedDictionary<string, string[]>(dict), ColorCategories(source), GetGradient(MetaExtract(source)[4]), MainParties(source), IncumbentModifier(MetaExtract(source)[0], MainParties(source)), " Schlüssel wahr", " Schlüssel falsch");
                    DrawTable(dict, ColorCategories(source), GetGradient(MetaExtract(source)[4]));
                    MetaLabeling(MetaExtract(source)[2] + ", " + MetaExtract(source)[1], MetaExtract(source)[5]);
                    DrawKeyBox(new SortedDictionary<string, string[]>(dict), ColorCategories(source), GetGradient(MetaExtract(source)[4]));
                }
                if (ErrorLabel.Text != "")
                {
                    ClearDisplay();
                }
            }
            catch
            {
                if (ErrorLabel.Text == "")
                {
                    ErrorLabel.Text = "Fehler aufgetreten.";
                }
            }
        }
        //Loads Countries for Selection
        public void LoadCountries()
        {
            ObservableCollection<string> data = new ObservableCollection<string>();
            data.Add("Vereinigte Staaten");
            data.Add("Schweiz");
            CountryListView.ItemsSource = data;
        }
        //Loads the selectable Options for the Countires
        public void LoadOptions(Stream source)
        {
            ObservableCollection<string> data = new ObservableCollection<string>();
            ObservableCollection<string> datacompare = new ObservableCollection<string>();
            using (var reader = new StreamReader(source))
            {
                while (reader.EndOfStream == false)
                {
                    string line = reader.ReadLine();
                    data.Add(line);
                    if (line != "13 Keys")
                    {
                        datacompare.Add(line);
                    }
                    reader.ReadLine();
                }
            }
            CompareListView.ItemsSource = datacompare;
            HistoricalListView.ItemsSource = data;
        }
        //Clears all genereated Display-Info in the Righthand Section
        private void ClearDisplay()
        {
            foreach (View c in GeneratedControls)
            {
                c.IsVisible = false;
                DisplayStackLayout.Children.Remove(c);
            }
            sksvg = new SkiaSharp.Extended.Svg.SKSvg();
            mappanel.InvalidateSurface();
            mappanel.IsVisible = false;
            RemarkLabel.Text = "";
            TitleLabel.Text = "";
        }
        //Handles Shifts for Popular Votes
        private double[] SplitVote(string src, int shift)
        {
            double votetotal1 = 0;
            double votetotal2 = 0;
            using (var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetManifestResourceNames().Single(str => str.Contains(src)))))
            {
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                while (reader.EndOfStream == false)
                {
                    try
                    {
                        var line = reader.ReadLine();
                        if (line == null)
                        {
                            break;
                        }
                        var values = RemoveEmptyInfo(line.Split('\t'));
                        NumberFormatInfo format = new NumberFormatInfo
                        {
                            NumberDecimalSeparator = "."
                        };
                        double percentage1 = Convert.ToDouble(values[3], format);
                        double percentage2 = Convert.ToDouble(values[4], format);
                        double totalvotes = percentage1 + percentage2;
                        if (totalvotes == 0)
                        {
                            totalvotes = 1;
                        }
                        percentage1 = percentage1 / totalvotes * 100 + shift / 2;
                        percentage2 = percentage2 / totalvotes * 100 - shift / 2;
                        percentage1 = percentage1 / 100 * totalvotes;
                        percentage2 = percentage2 / 100 * totalvotes;
                        votetotal1 += percentage1;
                        votetotal2 += percentage2;

                    }
                    catch { }
                }
            }
            return new double[] { votetotal1, votetotal2 };
        }
        //Creates a Bar with given Numbers, Labels and Colours, with minimal adjustment
        private void DrawElectoralVotes(bool countvotes, bool popvote, bool displayvotescount, double[] SplitVote, SortedDictionary<string, string[]> dict, string[] Parties, List<Xamarin.Forms.Color> Colors, string[] MainParties, string candidates, string repvotetext, string demvotetext)
        {
            if (Parties[0] != MainParties[0])
            {
                Array.Reverse(Parties);
                Colors.Reverse();
            }
            Label CandidatesLabel = new Label
            {
                Text = (candidates),
                TextColor = Xamarin.Forms.Color.FloralWhite,
                FontAttributes = FontAttributes.Bold,
                FontSize = 25
            };
            BarStackLayout.Children.Add(CandidatesLabel);
            GeneratedControls.Add(CandidatesLabel);
            StackLayout BarDisplay = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Fill,
                Orientation = StackOrientation.Horizontal
            };
            StackLayout VotesDisplay = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Fill,
                Orientation = StackOrientation.Horizontal
            };
            BarStackLayout.Children.Add(BarDisplay);
            GeneratedControls.Add(BarDisplay);
            BarStackLayout.Children.Add(VotesDisplay);
            GeneratedControls.Add(VotesDisplay);
            List<double> Votes = new List<double>();
            for (int i = 0; i < Parties.Length; i++)
            {
                Votes.Add(0);
            }
            NumberFormatInfo format = new NumberFormatInfo
            {
                NumberDecimalSeparator = "."
            };
            if (popvote)
            {
                Votes[Array.IndexOf(Parties, MainParties[0])] = Math.Round(SplitVote[0] / (SplitVote[0] + SplitVote[1]) * 100, 2);
                Votes[Array.IndexOf(Parties, MainParties[1])] = Math.Round(SplitVote[1] / (SplitVote[0] + SplitVote[1]) * 100, 2);
            }
            else
            {
                foreach (var item in dict)
                {
                    foreach (string party in Parties)
                    {
                        if (item.Value[0] == party)
                        {
                            if (countvotes)
                            {
                                Votes[Array.IndexOf(Parties, party)] += Convert.ToDouble(item.Value[1], format);
                            }
                            else
                            {
                                Votes[Array.IndexOf(Parties, party)] += 1;
                            }
                        }
                    }
                }
            }

            int numoftobedisplayed = 0;
            foreach (int i in Votes)
            {
                if (i > 0)
                {
                    numoftobedisplayed++;
                }
            }
            for (int i = 0; i < Parties.Length; i++)
            {
                double width;
                width = Convert.ToDouble(Votes[i]) / Convert.ToDouble(Votes.Sum()) * Convert.ToDouble(BarStackLayout.Width - 5 * numoftobedisplayed);
                if (width > 0)
                {
                    Frame votespanel = new Frame
                    {
                        MinimumWidthRequest = Convert.ToInt32(width),
                        WidthRequest = Convert.ToInt32(width),
                        BackgroundColor = Colors[i],
                        Padding = new Thickness(0, 20, 0, 20),
                        Margin = 0,
                        MinimumHeightRequest = 25
                    };
                    BarDisplay.Children.Add(votespanel);
                    GeneratedControls.Add(votespanel);
                }
            }
            Label RepVotesLabel = new Label
            {
                Text = (candidates),
                TextColor = Xamarin.Forms.Color.FloralWhite,
                FontAttributes = FontAttributes.Bold,
                FontSize = 20
            };
            VotesDisplay.Children.Add(RepVotesLabel);
            GeneratedControls.Add(RepVotesLabel);
            Label DemVotesLabel = new Label
            {
                Text = (candidates),
                TextColor = Xamarin.Forms.Color.FloralWhite,
                FontAttributes = FontAttributes.Bold,
                FontSize = 20,
                HorizontalTextAlignment = TextAlignment.End,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };
            VotesDisplay.Children.Add(DemVotesLabel);
            GeneratedControls.Add(DemVotesLabel);
            RepVotesLabel.Text = repvotetext;
            DemVotesLabel.Text = demvotetext;
            if (countvotes || displayvotescount)
            {
                double REPvotes = 0;
                double DEMvotes = 0;
                for (int i = 0; i < Votes.Count; i++)
                {
                    if (Parties[i].Contains(MainParties[0]))
                    {
                        REPvotes += Votes[i];
                    }
                    if (Parties[i].Contains(MainParties[1]))
                    {
                        DEMvotes += Votes[i];
                    }
                }
                RepVotesLabel.Text = Convert.ToString(REPvotes) + repvotetext;
                DemVotesLabel.Text = Convert.ToString(DEMvotes) + demvotetext;
            }
            if (countvotes && displayvotescount)
            {
                RepVotesLabel.Text = "";
                DemVotesLabel.Text = "";
            }
        }
        //Sanitise Info from Lists so as to not cause Errors
        private string[] RemoveEmptyInfo(string[] s)
        {
            List<string> list = new List<string>();
            foreach (string st in s)
            {
                if (st != "")
                {
                    list.Add(st);
                }
            }
            return list.ToArray();
        }
        private string IncumbentModifier(string s, string[] MainParties)
        {
            string returnstring = "";
            string[] split = s.Split(' ');
            if (split[1][1] == MainParties[0][0])
            {
                returnstring = split[0] + " " + split[1][0] + split[1][1] + ", Amtsinhaber" + split[1][2] + " " + split[2] + " " + split[3] + " " + split[4];
            }
            else
            {
                for (int i = 0; i < split.Length - 1; i++)
                {
                    returnstring += split[i] + " ";
                }
                returnstring += Convert.ToString(split[(split.Length - 1)][0]) + Convert.ToString(split[(split.Length - 1)][1]) + ", Amtsinhaber" + split[(split.Length - 1)][2];
            }
            return returnstring;
        }
        //Gets the type of data to be displayed
        private string SourceFormatType(string src)
        {
            using (var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetManifestResourceNames().Single(str => str.Contains(src)))))
            {
                var data = reader.ReadLine();
                return data;
            }
        }
        //Gives Boolean for a certain type of Visualisation Category
        private bool IsMapVis(string publication, string year)
        {
            bool returnval = false;
            try
            {
                if (SourceFormatType(publication + year + ".tsv").Contains("MapVis"))
                {
                    returnval = true;
                }
            }
            catch { }
            return returnval;
        }
        //Draws Table data onto View
        private void DrawTable(Dictionary<string, string[]> dict, string[] Parties, List<Xamarin.Forms.Color> Colors)
        {
            StackLayout space = new StackLayout
            {
                MinimumHeightRequest = 10,
                HeightRequest = 10
            };
            BarStackLayout.Children.Add(space);
            GeneratedControls.Add(space);
            foreach (string s in dict.Keys)
            {
                Label CandidatesLabel = new Label
                {
                    Text = s,
                    TextColor = Xamarin.Forms.Color.FloralWhite,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 20
                };
                Frame votespanel = new Frame
                {
                    MinimumWidthRequest = 15,
                    WidthRequest = 15,
                    BackgroundColor = Colors[Array.IndexOf(Parties, dict[s][0])],
                    Padding = new Thickness(5),
                    MinimumHeightRequest = 15,
                    HeightRequest = 15
                };
                StackLayout stack = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal
                };
                GeneratedControls.Add(stack);
                GeneratedControls.Add(CandidatesLabel);
                GeneratedControls.Add(votespanel);
                stack.Children.Add(CandidatesLabel);
                stack.Children.Add(votespanel);
                BarStackLayout.Children.Add(stack);
            }
            StackLayout spacebottom = new StackLayout
            {
                MinimumHeightRequest = 10,
                HeightRequest = 10
            };
            BarStackLayout.Children.Add(spacebottom);
            GeneratedControls.Add(spacebottom);
        }
        //Creates List of Second Part of Data Selection
        private List<string> GetOptionYear(string src)
        {
            List<string> years = new List<string>();
            Stream source = Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetManifestResourceNames().Single(str => str.EndsWith(countrysourcestring)));
            using (var reader = new StreamReader(source))
            {
                while (reader.EndOfStream == false)
                {
                    string data = reader.ReadLine();
                    if (data == src)
                    {
                        var yearsstream = RemoveEmptyInfo(reader.ReadLine().Split('\t'));
                        foreach (string year in yearsstream)
                        {
                            years.Add(year);
                        }
                    }
                }
            }
            return years;
        }
        //Gives back a List with all Dictionary Values
        public List<string> DictionaryValueList(SortedDictionary<string, string[]> dict)
        {
            List<string> s = new List<string>();
            foreach (string[] sarray in dict.Values)
            {
                s.Add(sarray[0]);
            }
            return s;
        }
        //Gives back a String with all Dictionary Values
        public string DictionaryValueString(SortedDictionary<string, string[]> dict)
        {
            string s = "";
            foreach (string[] sarray in dict.Values)
            {
                s += sarray[0];
            }
            return s;
        }
        //Creates the Key for the Map and Bar
        private void DrawKeyBox(SortedDictionary<string, string[]> dict, string[] Parties, List<Xamarin.Forms.Color> Colors)
        {
            bool passedbool = false;
            if (DictionaryValueString(dict).Contains("->") && !DictionaryValueList(dict)[0].Contains("->"))
            {
                Label legendname = new Label
                {
                    Text = "Übereinstimmend:",
                    TextColor = Xamarin.Forms.Color.FloralWhite,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 12
                };
                KeyStackLayout.Children.Add(legendname);
                GeneratedControls.Add(legendname);
                passedbool = true;
            }
            foreach (string party in Parties)
            {
                if (DictionaryValueList(dict).Contains(party))
                {
                    if (party.Contains("->") && passedbool)
                    {
                        Label legendnotice = new Label
                        {
                            Text = "Verglichen mit \"Historisch\":",
                            TextColor = Xamarin.Forms.Color.FloralWhite,
                            FontAttributes = FontAttributes.Bold,
                            FontSize = 12
                        };
                        KeyStackLayout.Children.Add(legendnotice);
                        GeneratedControls.Add(legendnotice);
                        passedbool = false;
                    }
                    StackLayout VertEntryStackLayout = new StackLayout()
                    {
                        VerticalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Orientation = StackOrientation.Horizontal
                    };
                    KeyStackLayout.Children.Add(VertEntryStackLayout);
                    GeneratedControls.Add(VertEntryStackLayout);
                    Frame votespanel = new Frame
                    {
                        MinimumWidthRequest = 5,
                        WidthRequest = 5,
                        BackgroundColor = Colors[Array.IndexOf(Parties, party)],
                        Padding = new Thickness(5),
                        MinimumHeightRequest = 5,
                        HeightRequest = 5
                    };
                    VertEntryStackLayout.Children.Add(votespanel);
                    GeneratedControls.Add(votespanel);
                    Label TextsLabel = new Label
                    {
                        Text = party,
                        TextColor = Xamarin.Forms.Color.FloralWhite,
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 12,
                        HorizontalTextAlignment = TextAlignment.Start,
                        HorizontalOptions = LayoutOptions.StartAndExpand
                    };
                    VertEntryStackLayout.Children.Add(TextsLabel);
                    GeneratedControls.Add(TextsLabel);
                }
            }
        }
        //Auto-Function for Selection Purposes
        private void HistoricalListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (sender == CompareListView)
            {
                List<string> years = GetOptionYear(Convert.ToString(CompareListView.SelectedItem));
                ObservableCollection<string> data = new ObservableCollection<string>();
                data.Clear();
                foreach (string year in years)
                {
                    if (IsMapVis(Convert.ToString(CompareListView.SelectedItem), year))
                    {
                        data.Add(year);
                    }
                }
                CompareYearsListView.ItemsSource = data;
            }
            if (sender == HistoricalListView)
            {
                List<string> years = GetOptionYear(Convert.ToString(HistoricalListView.SelectedItem));
                ObservableCollection<string> data = new ObservableCollection<string>();
                data.Clear();
                foreach (string year in years)
                {
                    data.Add(year);
                }
                HistoricalYearsListView.ItemsSource = data;
            }
        }
        //Visual Function, adds Dropdown 1
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (CompareLabel.Text == "Vergleichen ▲")
            {
                CompareLabel.Text = "Vergleichen ▼";
                CompareGroup.IsVisible = true;
                if (ShiftResultLabel.Text == "Modifizieren ▲")
                {
                    ShiftResultsCompareFrame.IsVisible = false;
                }
                else
                {
                    ShiftResultsCompareFrame.IsVisible = true;
                }
            }
            else if (CompareLabel.Text == "Vergleichen ▼")
            {
                CompareLabel.Text = "Vergleichen ▲";
                CompareGroup.IsVisible = false;
                ShiftResultsCompareFrame.IsVisible = false;
            }
        }
        //Visual Function, adds Dropdown 2
        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            if (ShiftResultLabel.Text == "Modifizieren ▲")
            {
                ShiftResultLabel.Text = "Modifizieren ▼";
                ShiftResultsHistoricalFrame.IsVisible = true;
                if (CompareLabel.Text == "Vergleichen ▲")
                {
                    ShiftResultsCompareFrame.IsVisible = false;
                }
                else
                {
                    ShiftResultsCompareFrame.IsVisible = true;
                }
            }
            else if (ShiftResultLabel.Text == "Modifizieren ▼")
            {
                ShiftResultLabel.Text = "Modifizieren ▲";
                ShiftResultsHistoricalFrame.IsVisible = false;
                ShiftResultsCompareFrame.IsVisible = false;
            }
        }
        //Reads Source Document and get Parties/Color Categories
        private string[] ColorCategories(string src)
        {
            using (var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetManifestResourceNames().Single(str => str.Contains(src)))))
            {
                reader.ReadLine();
                reader.ReadLine();
                var data = RemoveEmptyInfo(reader.ReadLine().Split('\t'));
                return data;
            }
        }
        //Reads Source Document and gets the two Main Parties, which need to be specified for Shifting Results to work
        private string[] MainParties(string src)
        {
            using (var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetManifestResourceNames().Single(str => str.Contains(src)))))
            {
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                var data = RemoveEmptyInfo(reader.ReadLine().Split('\t'));
                return data;
            }
        }
        //Creates Dictionary with Table Data and according Parties for 13-Key Prediciton
        private Dictionary<string, string[]> CreateTableDicitonary(string src)
        {
            Dictionary<string, string[]> dict = new Dictionary<string, string[]>();
            using (var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetManifestResourceNames().Single(str => str.Contains(src)))))
            {
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                while (reader.EndOfStream == false)
                {
                    var line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    var values = RemoveEmptyInfo(line.Split('\t'));
                    string[] valarray = new string[] { "placeholder", values[1] };
                    if (values[1] == "True")
                    {
                        valarray[0] = MainParties(src)[0];
                    }
                    else
                    {
                        valarray[0] = MainParties(src)[1];
                    }
                    dict.Add(values[0], valarray);
                }
            }
            return dict;
        }
        //Creates the Main Dictionary from the File and applies a Shift if specified
        private SortedDictionary<string, string[]> CreateDictionary(string src, int shift, bool popvoteadjust)
        {
            SortedDictionary<string, string[]> dict = new SortedDictionary<string, string[]>();
            using (var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetManifestResourceNames().Single(str => str.Contains(src)))))
            {
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                if (shift != 0)
                {
                    while (reader.EndOfStream == false)
                    {
                        try
                        {
                            var line = reader.ReadLine();
                            if (line == null)
                            {
                                break;
                            }
                            var values = RemoveEmptyInfo(line.Split('\t'));
                            string[] mp = MainParties(src);
                            string[] parties = ColorCategories(src);
                            NumberFormatInfo format = new NumberFormatInfo
                            {
                                NumberDecimalSeparator = "."
                            };
                            double percentage1 = Convert.ToDouble(values[3], format);
                            double percentage2 = Convert.ToDouble(values[4], format);
                            if (popvoteadjust)
                            {
                                double totalvotes = percentage1 + percentage2;
                                percentage1 = percentage1 / totalvotes * 100;
                                percentage2 = percentage2 / totalvotes * 100;
                            }
                            double newvalr = percentage1 + (Convert.ToDouble((shift)) / Convert.ToDouble(2));
                            double newvald = percentage2 - (Convert.ToDouble((shift)) / Convert.ToDouble(2));
                            string party = "";
                            string[] arrayvals = new string[] { values[1], values[2] };
                            if (values[1].Contains(mp[0]) || values[1].Contains(mp[1]) || values[1].Contains("Unentschieden"))
                            {
                                if (newvalr - newvald > 0)
                                {
                                    party = mp[0];
                                }
                                else if (newvalr - newvald < 0)
                                {
                                    party = mp[1];
                                }
                                else if (newvalr - newvald == 0)
                                {
                                    party = "Unentschieden";
                                }
                                if (Math.Abs(newvalr - newvald) < 6 && newvalr - newvald != 0)
                                {
                                    foreach (string s in parties)
                                    {
                                        if (s.Contains(" "))
                                        {
                                            if (party == s.Split(' ')[1])
                                            {
                                                party = s;
                                                break;
                                            }
                                        }

                                    }
                                }
                                arrayvals = new string[] { party, values[2] };
                            }

                            dict.Add(values[0], arrayvals);
                        }
                        catch
                        {
                            ErrorLabel.Text = "Verschiebung nicht möglich für " + src.Replace('/', ' ').Replace((Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "/Resources/DataFiles/").Replace('/', ' '), "").Replace(".tsv", "");
                        }
                    }
                }
                else
                {
                    while (reader.EndOfStream == false)
                    {
                        var line = reader.ReadLine();
                        if (line == null)
                        {
                            break;
                        }
                        var values = RemoveEmptyInfo(line.Split('\t'));
                        string[] arrayvals = new string[] { values[1], values[2] };
                        dict.Add(values[0], arrayvals);
                    }
                }

            }
            return dict;
        }
        //Loads the Svg-File
        private Stream GetSvgFromDirectory(string src)
        {
            Stream ms = Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetManifestResourceNames().Single(str => str.Contains(src)));
            var svg = new SkiaSharp.Extended.Svg.SKSvg();
            return ms;
        }
        //Compares two Dictionaries and gives back one with compared Info
        private SortedDictionary<string, string[]> CreateComparedDictionary(SortedDictionary<string, string[]> dict1, SortedDictionary<string, string[]> dict2)
        {
            SortedDictionary<string, string[]> dict = new SortedDictionary<string, string[]>();
            foreach (string key in dict1.Keys)
            {
                if (dict2.Keys.Contains(key))
                {
                    if (dict1[key][0] == "NULL" || dict2[key][0] == "NULL")
                    {
                        dict.Add(key, new string[] { "NULL", dict1[key][1] });
                    }
                    else if (dict1[key][0] == dict2[key][0])
                    {
                        dict.Add(key, new string[] { dict1[key][0], dict1[key][1] });
                    }
                    else
                    {
                        dict.Add(key, new string[] { (dict2[key][0] + " -> " + dict1[key][0]), dict1[key][1] });
                    }
                }
            }
            return dict;
        }
        //Reads Source Document and Reads the Meta-Information
        private string[] MetaExtract(string src)
        {
            using (var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetManifestResourceNames().Single(str => str.Contains(src)))))
            {
                reader.ReadLine();
                var data = RemoveEmptyInfo(reader.ReadLine().Split('\t'));
                return data;
            }
        }
        //Gets all the Colours from a File
        private List<Xamarin.Forms.Color> ColorsExtract(SKBitmap src)
        {
            List<Xamarin.Forms.Color> Colors = new List<Xamarin.Forms.Color>();
            for (int x = 0; x < src.Width; x++)
            {
                Colors.Add(src.GetPixel(x, 0).ToFormsColor());
            }
            return Colors;
        }
        //Loads Bitmap from source string for passing to ColorsExtract Function
        public List<Xamarin.Forms.Color> GetGradient(string src)
        {
            List<Xamarin.Forms.Color> Colors = ColorsExtract(SKBitmap.Decode(Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetManifestResourceNames().Single(str => str.Contains(src)))));
            return Colors;
        }
        //Generates all possible Party compared Pairings and filters ones not used
        private string[] ColorComparedCategories(string src1, string src2, SortedDictionary<string, string[]> compareddict)
        {
            List<string> data = new List<string>(ColorCategories(src1));
            List<string> removedata = new List<string>();
            var data1 = ColorCategories(src1);
            var data2 = ColorCategories(src2);
            foreach (string val2 in data2)
            {
                foreach (string val1 in data1)
                {
                    if (val1 != val2)
                    {
                        data.Add(val2 + " -> " + val1);
                    }
                }
            }
            foreach (string[] value in compareddict.Values)
            {
                removedata.Add(value[0]);
            }
            foreach (string key in data.ToArray())
            {
                if (!removedata.Contains(key) && key.Contains("->"))
                {
                    data.Remove(key);
                }
            }
            return data.ToArray();
        }
        //Adds Information about applied shift to Title
        private string TitleShiftModifier(string title, string src, int shift)
        {
            if (ShiftResultsTrackBar.IsVisible && shift != 0)
            {
                string winningparty;
                if (shift > 0)
                {
                    winningparty = MainParties(src)[0];
                }
                else
                {
                    winningparty = MainParties(src)[1];
                }
                title += " (+" + Math.Abs(shift) + "% " + winningparty[0] + ")";
            }
            return title;
        }
        //Sets Text for Given Elements
        private void MetaLabeling(string title, string remarks)
        {
            TitleLabel.Text = (title);
            RemarkLabel.Text = remarks;
        }
        //Modifies Svg to contain correct Data and then renders it
        private void DrawMap(Stream map, SortedDictionary<string, string[]> dict, List<Xamarin.Forms.Color> Colors, string[] Parties)
        {
            List<List<string>> AssignedStates = new List<List<string>>();
            for (int i = 0; i < Parties.Length; i++)
            {
                AssignedStates.Add(new List<string>());
            }
            List<string> BLANKColors = new List<string>();
            foreach (var item in dict)
            {
                foreach (string party in Parties)
                {
                    if (item.Value[0] == party)
                    {
                        AssignedStates[Array.IndexOf(Parties, party)].Add(item.Key);
                    }
                    else if (item.Value[0] == "NULL")
                    {
                        BLANKColors.Add(item.Key);
                    }
                }
            }
            StreamReader reader = new StreamReader(map);
            string svgdata = reader.ReadToEnd();
            bool atleastonepainted = false;
            List<string> alreadyprinted = new List<string>();
            map.Position = 0;
            foreach (List<string> parties in AssignedStates)
            {
                foreach (string state in parties)
                {
                    try
                    {
                        if (!BLANKColors.Contains(state) & !alreadyprinted.Contains(state))
                        {
                            alreadyprinted.Add(state);
                            svgdata = svgdata.Replace(("id=\"" + state + "\""), ("id=\"" + state + "\"" + " style=\"fill:" + (Colors[AssignedStates.IndexOf(parties)]).ToHex().ToString().Remove(1, 2).ToLower() + "\""));
                            atleastonepainted = true;
                        }

                    }
                    catch { }
                }
            }
            foreach (string state in BLANKColors)
            {
                if (!alreadyprinted.Contains(state))
                {
                    alreadyprinted.Add(state);
                    svgdata = svgdata.Replace(("id=\"" + state + "\""), ("id=\"" + state + "\"" + " style=\"fill:#282828\""));
                    atleastonepainted = true;
                }
            }
            byte[] byteArray = Encoding.ASCII.GetBytes(svgdata);
            Stream output = new MemoryStream(byteArray);
            sksvg.Load(output);
            mappanel.InvalidateSurface();
            if (atleastonepainted)
            {
                mappanel.IsVisible = true;
            }
        }
        //Creates Multiple Bars all below each other
        private void CreateMultipleBars(Dictionary<string, SortedDictionary<string, string[]>> maindict, string[] parties, List<Xamarin.Forms.Color> Colors, string[] MainParties)
        {
            foreach (string title in maindict.Keys)
            {
                DrawElectoralVotes(true, false, false, new double[] { }, maindict[title], parties, Colors, MainParties, title, "%", "%");
            }
        }
        //Creates Dictionary for Statistics Visualisation Display Type
        private Dictionary<string, SortedDictionary<string, string[]>> CreateStatDictionary(string src)
        {
            Dictionary<string, SortedDictionary<string, string[]>> returnlist = new Dictionary<string, SortedDictionary<string, string[]>>();
            using (var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetManifestResourceNames().Single(str => str.Contains(src)))))
            {
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                while (reader.EndOfStream == false)
                {
                    SortedDictionary<string, string[]> dict = new SortedDictionary<string, string[]>();
                    string[] parties = ColorCategories(src);
                    var line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    var values = RemoveEmptyInfo(line.Split('\t'));
                    string title = values[0];
                    for (int i = 0; i < parties.Length; i++)
                    {
                        string[] arrayvals = new string[] { parties[i], values[i + 1] };
                        dict.Add(Convert.ToString(i), arrayvals);
                    }
                    returnlist.Add(title, dict);
                }
            }
            return returnlist;
        }
        //Main Function started on a Button Click
        private void Button_Clicked(object sender, EventArgs e)
        {
            MainLoadFunc();
        }
        //Necessary for UI Map Scaling
        private void mappanel_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            try
            {
                float scale = (float)mappanel.Width * (float)0.0015;
                mappanel.HeightRequest = mappanel.Width*0.75;
                var matrix = SKMatrix.CreateScale(scale, scale);
                e.Surface.Canvas.DrawPicture(sksvg.Picture, ref matrix);
            }
            catch { }
        }
        //Checks Logic of Dropdown Menus
        private void CheckVisibility()
        {
            if (!IsMapVis(Convert.ToString(HistoricalListView.SelectedItem), Convert.ToString(HistoricalYearsListView.SelectedItem)))
            {
                ShiftResultLabel.IsVisible = false;
                CompareLabel.IsVisible = false;
                CompareGroup.IsVisible = false;
                ShiftResultsHistoricalFrame.IsVisible = false;
                ShiftResultsCompareFrame.IsVisible = false;
                return;
            }
            ShiftResultLabel.IsVisible = true;
            CompareLabel.IsVisible = true;
            if (CompareLabel.Text == "Vergleichen ▼")
            {
                CompareGroup.IsVisible = true;
            }
            if (ShiftResultLabel.Text == "Modifizieren ▼")
            {
                ShiftResultsHistoricalFrame.IsVisible = true;
            }
            if ((CompareLabel.Text == "Vergleichen ▼") & (CompareLabel.Text == "Modifizieren ▼"))
            {
                ShiftResultsCompareFrame.IsVisible = true;
            }
        }
        //Decides on Visibility of Compare and Shift Options
        private void CountryListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            HistoricalStackLayout.IsVisible = true;
            HistoricalLabel.IsVisible = true;
            CompareLabel.Text = "Vergleichen ▲";
            ShiftResultLabel.Text = "Modifizieren ▲";
            CheckVisibility();
            CompareLabel.IsVisible = false;
            CompareGroup.IsVisible = false;
            ShiftResultLabel.IsVisible = false;
            ShiftResultsCompareFrame.IsVisible = false;
            ShiftResultsHistoricalFrame.IsVisible = false;
            HistoricalYearsListView.ItemsSource = new ObservableCollection<string>();
            CompareYearsListView.ItemsSource = new ObservableCollection<string>();
            if (CountryListView.SelectedItem.ToString().Contains("Vereinigte Staaten"))
            {
                Optionssource = Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetManifestResourceNames().Single(str => str.EndsWith("usoption.tsv")));
                countrysourcestring = "usoption.tsv";
            }
            else if (CountryListView.SelectedItem.ToString().Contains("Schweiz"))
            {
                Optionssource = Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetManifestResourceNames().Single(str => str.EndsWith("swissoption.tsv")));
                countrysourcestring = "swissoption.tsv";
            }
            HistoricalYearsListView.SelectedItem = null;
            HistoricalListView.SelectedItem = null;
            CompareYearsListView.SelectedItem = null;
            CompareListView.SelectedItem = null;
            LoadOptions(Optionssource);
        }
        //Functions for Grey Background of tapped Cells
        TextCell lastCellHistorical;
        int timestappedbefore = 0;
        private void TextCell_Tapped(object sender, EventArgs e)
        {
            if (lastCellHistorical != null)
                lastCellHistorical.TextColor = Xamarin.Forms.Color.FloralWhite;
            var viewCell = (TextCell)sender;
            viewCell.TextColor = Xamarin.Forms.Color.Gray;
            lastCellHistorical = viewCell;
            timestappedbefore++;
            try
            {
                if (timestappedbefore > 1)
                {
                    if (HistoricalYearsListView.SelectedItem.ToString().Length > 0)
                    {
                        CheckVisibility();
                    }
                }

            }
            catch { }
        }
        TextCell lastCellHistoricalYears;
        private void TextCell_Tapped_1(object sender, EventArgs e)
        {
            if (lastCellHistoricalYears != null)
            {
                lastCellHistoricalYears.TextColor = Xamarin.Forms.Color.FloralWhite;
            }
            var viewCell = (TextCell)sender;
            viewCell.TextColor = Xamarin.Forms.Color.Gray;
            lastCellHistoricalYears = viewCell;
            CheckVisibility();
        }
        TextCell lastCellCompare;
        private void TextCell_Tapped_2(object sender, EventArgs e)
        {
            if (lastCellCompare != null)
            {
                lastCellCompare.TextColor = Xamarin.Forms.Color.FloralWhite;
            }
            var viewCell = (TextCell)sender;
            viewCell.TextColor = Xamarin.Forms.Color.Gray;
            lastCellCompare = viewCell;
        }
        TextCell lastCellCompareYears;
        private void TextCell_Tapped_3(object sender, EventArgs e)
        {
            if (lastCellCompareYears != null)
            {
                lastCellCompareYears.TextColor = Xamarin.Forms.Color.FloralWhite;
            }
            var viewCell = (TextCell)sender;
            viewCell.TextColor = Xamarin.Forms.Color.Gray;
            lastCellCompareYears = viewCell;
        }
        TextCell lastCountryCell;
        private void TextCell_Tapped_4(object sender, EventArgs e)
        {
            if (lastCountryCell != null)
            {
                lastCountryCell.TextColor = Xamarin.Forms.Color.FloralWhite;
            }
            var viewCell = (TextCell)sender;
            viewCell.TextColor = Xamarin.Forms.Color.Gray;
            lastCountryCell = viewCell;
        }
    }
}