using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using ElectionPredictFinal.Pages.Classes;
using System.IO;
using System.Reflection;
using System.Linq;

namespace ElectionPredictFinal.Pages.Classes
{
    public class Selection
    {
        private string myactivestring = "";
        private string[] myoptions;
        private List<Frame> myFrameList = new List<Frame>();
        private Frame myFrame = new Frame()
        {
            HasShadow = false,
            BackgroundColor = Color.FromHex("#282828"),
            HeightRequest = 100,
            Padding = new Thickness(20, 0),
            CornerRadius = 10
        };
        private StackLayout myMainStack = new StackLayout();
        private ScrollView myScrollView = new ScrollView();
        private StackLayout myStackLayout = new StackLayout()
        {
            Padding = new Thickness(0, 5)
        };
        public Selection(string source)
        {
            myMainStack.Children.Add(myFrame);
            myScrollView.Content = myStackLayout;
            myFrame.Content = myScrollView;
            List<string> ss = new List<string>();
            using (var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetManifestResourceNames().Single(str => str.Contains(source)))))
            {
                while (!reader.EndOfStream)
                {
                    string s = reader.ReadLine();
                    ss.Add(s);
                    Label text = new Label()
                    {
                        Text = s,
                        FontSize = 15,
                        HorizontalOptions = LayoutOptions.Start,
                        HorizontalTextAlignment = TextAlignment.Start
                    };
                    Frame TextStack = new Frame()
                    {
                        BackgroundColor = Color.FromHex("#282828"),
                        Padding = new Thickness(5),
                        HasShadow = false
                    };
                    myFrameList.Add(TextStack);
                    TextStack.Content = text;
                    var LabelTapRecognizer = new TapGestureRecognizer();
                    LabelTapRecognizer.Tapped += LabelTap;
                    TextStack.GestureRecognizers.Add(LabelTapRecognizer);
                    myStackLayout.Children.Add(TextStack);
                }
            }
            myoptions = ss.ToArray();
            void LabelTap(object sender, EventArgs e)
            {
                if(myactivestring != ((Label)((Frame)sender).Content).Text)
                {
                    foreach (Frame f in myFrameList)
                    {
                        f.BackgroundColor = Color.FromHex("#282828");
                    }
                    ((Frame)sender).BackgroundColor = Color.Gray;
                    myactivestring = ((Label)((Frame)sender).Content).Text;
                }

            }
        }
        public StackLayout Stack
        {
            get
            {
                return myMainStack;
            }
        }
        public string selected
        {
            get
            {
                return Convert.ToString(Array.IndexOf(myoptions, myactivestring) + 1);
            }
        }
    }
    public class ThreeLevelSelection
    {
        private string myselectedarea = "0";
        private Dictionary<Frame, string> LabelAreaLink = new Dictionary<Frame, string>();
        private Dictionary<string, Frame> AreaLabelLink = new Dictionary<string, Frame>();
        private StackLayout myMainStack = new StackLayout();
        private StackLayout myTopLevelStack = new StackLayout()
        {
            Padding = new Thickness(0,5)
        };
        private StackLayout mySecondLevelStack = new StackLayout()
        {
            Padding = new Thickness(0, 5)
        };
        private StackLayout myThirdLevelStack = new StackLayout()
        {
            Padding = new Thickness(0, 5)
        };
        private ScrollView myTopLevelScrollView = new ScrollView();
        private ScrollView mySecondLevelScrollView = new ScrollView();
        private ScrollView myThirdLevelScrollView = new ScrollView();
        private Frame myTopLevelFrame = new Frame()
        {
            HasShadow = false,
            BackgroundColor = Color.FromHex("#282828"),
            HeightRequest = 100,
            Padding = new Thickness(20,0),
            CornerRadius = 10
        };
        private Frame mySecondLevelFrame = new Frame()
        {
            HasShadow = false,
            BackgroundColor = Color.FromHex("#282828"),
            HeightRequest = 100,
            Padding = new Thickness(20, 0),
            CornerRadius = 10,
            IsVisible = false
        };
        private Frame myThirdLevelFrame = new Frame()
        {
            HasShadow = false,
            BackgroundColor = Color.FromHex("#282828"),
            HeightRequest = 100,
            Padding = new Thickness(20, 0),
            CornerRadius = 10,
            IsVisible = false
        };
        public ThreeLevelSelection(string source)
        {
            myTopLevelScrollView.Content = myTopLevelStack;
            myTopLevelFrame.Content = myTopLevelScrollView;
            mySecondLevelScrollView.Content = mySecondLevelStack;
            mySecondLevelFrame.Content = mySecondLevelScrollView;
            myThirdLevelScrollView.Content = myThirdLevelStack;
            myThirdLevelFrame.Content = myThirdLevelScrollView;
            myMainStack.Children.Add(myTopLevelFrame);
            myMainStack.Children.Add(mySecondLevelFrame);
            myMainStack.Children.Add(myThirdLevelFrame);
            using (var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetManifestResourceNames().Single(str => str.Contains(source)))))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    var index = line.Split(' ')[0];
                    Label text = new Label()
                    {
                        Text = line,
                        FontSize = 15,
                        HorizontalOptions = LayoutOptions.Start,
                        HorizontalTextAlignment = TextAlignment.Start
                    };
                    Frame TextStack = new Frame()
                    {
                        BackgroundColor = Color.FromHex("#282828"),
                        Padding = new Thickness(5),
                        HasShadow = false
                    };
                    TextStack.Content = text;
                    var LabelTapRecognizer = new TapGestureRecognizer();
                    LabelTapRecognizer.Tapped += LabelTap;
                    TextStack.GestureRecognizers.Add(LabelTapRecognizer);
                    LabelAreaLink.Add(TextStack, index);
                    AreaLabelLink.Add(index, TextStack);
                    if(index.Split('.').Length == 1)
                    {
                        myTopLevelStack.Children.Add(TextStack);
                    }
                }
            }
            void LabelTap(object sender, EventArgs e)
            {
                if(!(LabelAreaLink[(Frame)sender] == myselectedarea))
                {
                    string TappedIndex = LabelAreaLink[(Frame)sender];
                    string selarea = "";
                    foreach (string s in myselectedarea.Split('.'))
                    {
                        if (s == "0")
                        {
                            break;
                        }
                        else
                        {
                            if (selarea.Length != 0)
                            {
                                selarea += '.';
                            }
                            selarea += s;
                            AreaLabelLink[selarea].BackgroundColor = Color.FromHex("#282828");
                        }
                    }
                    myselectedarea = TappedIndex;
                    selarea = "";
                    foreach (string s in myselectedarea.Split('.'))
                    {
                        if(selarea.Length != 0)
                        {
                            selarea += '.';
                        }
                        selarea += s;
                        AreaLabelLink[selarea].BackgroundColor = Color.Gray;
                    }
                    //Implementierung der Rekursivität wird einer anderen Person als Aufgabe überlassen.
                    if (TappedIndex.Split('.').Length >= 1)
                    {
                        mySecondLevelStack.Children.Clear();
                        myThirdLevelStack.Children.Clear();
                        foreach (string s in AreaLabelLink.Keys)
                        {
                            if (s.Split('.')[0] == TappedIndex.Split('.')[0] && s.Split('.').Length == 2)
                            {
                                mySecondLevelStack.Children.Add(AreaLabelLink[s]);
                            }
                        }
                        if (TappedIndex.Split('.').Length >= 2)
                        {
                            foreach (string s in AreaLabelLink.Keys)
                            {
                                if(s.Split('.').Length == 3)
                                {
                                    if (s.Split('.')[0] == TappedIndex.Split('.')[0] && s.Split('.')[1] == TappedIndex.Split('.')[1])
                                    {
                                        myThirdLevelStack.Children.Add(AreaLabelLink[s]);
                                    }
                                }
                            }
                        }
                        mySecondLevelFrame.IsVisible = mySecondLevelStack.Children.Count > 0;
                        myThirdLevelFrame.IsVisible = myThirdLevelStack.Children.Count > 0;
                    }
                }
            }
        }
        private string PadSelection(string s)
        {
            if (s.Split('.').Length < 3)
            {
                s += ".0";
                s = PadSelection(s);
                return s;
            }
            else
            {
                return s;
            }
        }
        public string selectedarea
        {
            get
            {
                return PadSelection(myselectedarea);
            }
        }
        public StackLayout Stack
        {
            get { return myMainStack; }
        }
    }
    public class ThreewaySwitch
    {
        private string myactivstate = "";
        private bool myreport = false;
        private Color mybgcolor = Color.FromHex("#282828");
        private Color myactivecolor = Color.Gray;
        Frame f = new Frame
        {
            HorizontalOptions = LayoutOptions.Center,
            BackgroundColor = Color.FromHex("#282828"),
            HasShadow = false,
            HeightRequest = 30,
            CornerRadius = 5,
            Padding = new Thickness(8, 0, 8, 0)
        };
        StackLayout s = new StackLayout()
        {
            Orientation = StackOrientation.Horizontal,
            HorizontalOptions = LayoutOptions.Center,
            BackgroundColor = Color.FromHex("#282828")
        };
        public ThreewaySwitch(string[] options, Party party, bool report)
        {
            myreport = report;
            foreach (string str in options)
            {
                Button b = new Button()
                {
                    Text = str,
                    BackgroundColor = Color.FromHex("#282828"),
                    TextColor = Color.FloralWhite,
                    FontFamily = "Menlo",
                    FontSize = 14,
                    HeightRequest = 30,
                    HorizontalOptions = LayoutOptions.Center,
                    Padding = 5,
                    Margin = new Thickness(Convert.ToInt32((bool)(Array.IndexOf(options, str) == 0)) * -8, 0, Convert.ToInt32((bool)(Array.IndexOf(options, str) == options.Length - 1)) * -8, 0)
                };
                b.Clicked += OptionClicked;
                s.Children.Add(b);
            }
            f.Content = s;
            void OptionClicked(object sender, EventArgs args)
            {
                if (PartyInfluence.activevotes.ContainsKey(party) && myreport)
                {
                    if (myactivstate == ((Button)sender).Text)
                    {
                        myactivstate = "";
                        PartyInfluence.activevotes.Remove(party);
                    }
                    else
                    {
                        myactivstate = ((Button)sender).Text;
                        PartyInfluence.activevotes[party] = party.RequestVotes(myactivstate);
                    }
                }
                else
                {
                    if (myactivstate == ((Button)sender).Text)
                    {
                        myactivstate = "";
                    }
                    else
                    {
                        myactivstate = ((Button)sender).Text;
                        if (report)
                        {
                            PartyInfluence.activevotes.Add(party, party.RequestVotes(myactivstate));
                        }

                    }

                }
                CheckActive();
            }
        }
        public LayoutOptions HorizontalOption
        {
            set
            {
                f.HorizontalOptions = value;
            }
        }
        public Color BackgroundColor
        {
            set
            {
                mybgcolor = value;
                f.BackgroundColor = value;
                s.BackgroundColor = value;
                foreach(var b in s.Children)
                {
                    ((Button)b).BackgroundColor = value;
                }
                CheckActive();
            }

        }
        public Color ForegroundColor
        {
            set
            {
                myactivecolor = value;
                CheckActive();
            }

        }
        public Frame Switch
        {
            get { return f; }
        }
        public string activestate
        {
            get { return myactivstate; }
            set
            {
                myactivstate = value;
                CheckActive();
            }
        }
        void CheckActive()
        {
            foreach (object o in s.Children)
            {
                if (((Button)o).Text == myactivstate)
                {
                    ((Button)o).BackgroundColor = myactivecolor;
                }
                else
                {
                    ((Button)o).BackgroundColor = mybgcolor;
                }
            }
        }
    }
    public class MultilineButton : Button
    {

    }
}