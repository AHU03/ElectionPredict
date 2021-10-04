using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.IO;
using System.Reflection;
using System.Linq;
using MathNet.Numerics.Distributions;

namespace ElectionPredictFinal.Pages.Classes
{
    public class Graph
    {
        private Normal mydist = new Normal();
        private Dictionary<double, double> mymaindict = new Dictionary<double, double>();
        private double mystep = 0.0;
        private Color nocolor = Color.FromHex("#f19a9c");
        private Color yescolor = Color.FromHex("#6bcffe");
        private string mytitle = "";
        public Graph(Normal dist, double steps, string title)
        {
            mydist = dist;
            mystep = steps;
            mytitle = title;
            for(double i = steps; i< 1+steps; i += steps)
            {
                mymaindict.Add(i, dist.CumulativeDistribution(i) - dist.CumulativeDistribution(i-steps));
            }
        }
        public StackLayout Stack
        {
            get
            {
                StackLayout returnstack = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal
                };
                double biggestval = mymaindict.Values.ToList().Max();
                StackLayout legendstack = new StackLayout();
                Label title = new Label()
                {
                    Text = mytitle,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 20
                };
                Label spacer = new Label()
                {
                    Text = "\n\n",
                    FontSize = 10,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    VerticalOptions = LayoutOptions.End,
                    TextColor = Color.FromHex("#151515")
                };
                Label topl = new Label()
                {
                    Text = Convert.ToString(Convert.ToInt32(biggestval * 2500.0)),
                    FontSize = 10,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    VerticalOptions = LayoutOptions.Start
                };
                Label midl = new Label()
                {
                    Text = Convert.ToString(Convert.ToInt32(biggestval * 2500.0/2.0)),
                    FontSize = 10,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    VerticalOptions = LayoutOptions.CenterAndExpand
                };
                Label zerol = new Label()
                {
                    Text = "0",
                    FontSize = 10,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    VerticalOptions = LayoutOptions.End
                };
                legendstack.Children.Add(topl);
                legendstack.Children.Add(midl);
                legendstack.Children.Add(zerol);
                legendstack.Children.Add(spacer);
                returnstack.Children.Add(legendstack);
                foreach (double i in mymaindict.Keys)
                {
                    Color bgcolor = yescolor;
                    if(i < 0.5)
                    {
                        bgcolor = nocolor;
                    }
                    double height = 0;
                    if(mymaindict[i] > biggestval / 200)
                    {
                        Console.WriteLine(mymaindict[i] / biggestval * 200);
                        height = mymaindict[i] / biggestval * 200.0;
                    }
                    StackLayout linestack = new StackLayout();
                    Frame f = new Frame()
                    {
                        HasShadow = false,
                        Padding = new Thickness (0),
                        WidthRequest = 20,
                        HeightRequest = Convert.ToInt32(height),
                        VerticalOptions = LayoutOptions.EndAndExpand,
                        HorizontalOptions = LayoutOptions.Center,
                        BackgroundColor = bgcolor
                    };
                    Label l = new Label()
                    {
                        Text = 100*(i - mystep) + "%\n-\n" + 100*i +"%",
                        FontSize = 10,
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                        VerticalOptions = LayoutOptions.End
                    };
                    linestack.Children.Add(f);
                    linestack.Children.Add(l);
                    returnstack.Children.Add(linestack);
                }
                StackLayout wrapStack = new StackLayout();
                StackLayout ContentStack = new StackLayout();
                ContentStack.Children.Add(title);
                ContentStack.Children.Add(returnstack);
                Frame wrapFrame = new Frame()
                {
                    BackgroundColor = Color.FromHex("#151515"),
                    Content = ContentStack,
                    HasShadow = false,
                    CornerRadius = 20,
                    WidthRequest = 600
                };
                wrapStack.Children.Add(wrapFrame);
                return wrapStack;
            }
        }
    }
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
        public Selection(List<string> source)
        {
            myMainStack.Children.Add(myFrame);
            myScrollView.Content = myStackLayout;
            myFrame.Content = myScrollView;
            List<string> ss = new List<string>();
            foreach(string s in source)
            {
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
            myoptions = source.ToArray();
            void LabelTap(object sender, EventArgs e)
            {
                if (myactivestring != ((Label)((Frame)sender).Content).Text)
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
        public string selectedindex
        {
            get
            {
                return Convert.ToString(Array.IndexOf(myoptions, myactivestring) + 1);
            }
        }
        public string selectedstring
        {
            get
            {
                return myactivestring;
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