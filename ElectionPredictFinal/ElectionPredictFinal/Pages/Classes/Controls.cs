using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using ElectionPredictFinal.Pages.Classes;

namespace ElectionPredictFinal.Pages.Classes
{
    public class ThreewaySwitch
    {
        private string myactivstate = "";
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
        public ThreewaySwitch(string[] options, Party party)
        {
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
                if (PartyInfluence.activevotes.ContainsKey(party))
                {
                    if (myactivstate == ((Button)sender).Text)
                    {
                        myactivstate = "";
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
                    
                        PartyInfluence.activevotes.Add(party, party.RequestVotes(myactivstate));
                    }
                }

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
        }
        void CheckActive()
        {
            foreach (object o in s.Children)
            {
                if (((Button)o).Text == myactivstate)
                {
                    ((Button)o).BackgroundColor = Color.Gray;
                }
                else
                {
                    ((Button)o).BackgroundColor = Color.FromHex("#282828");
                }
            }
        }
    }
    public class MultilineButton : Button
    {

    }
}

