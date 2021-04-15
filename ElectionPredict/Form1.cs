using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Svg;

namespace ElectionPredict
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadOptions();
        }
        protected string Optionssource = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "/Resources/usoptions.tsv";
        public List<Control> GeneratedControls = new List<Control>();
        //Windows Functionality (Totally not copied from StackOverflow)
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
            }

            base.WndProc(ref m);
        }
        //Loads csv with different Options
        private void LoadOptions()
        {
            using (var reader = new StreamReader(Optionssource))
            {
                while (reader.EndOfStream == false)
                {
                    string data = reader.ReadLine();
                    listPublicationsHistorical.Items.Add(data);
                    listPublicationsCompare.Items.Add(data);
                    reader.ReadLine();
                }
            }
        }
        //Create List with needed Years
        private List<string> GetOptionYear(string src)
        {
            List<string> years = new List<string>();
            using (var reader = new StreamReader(Optionssource))
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
        //Extracts Type of Visualization required
        private string SourceFormatType(string src)
        {
            using (var reader = new StreamReader(src))
            {
                var data = reader.ReadLine();
                return data;
            }
        }
        //Extracts Data for Meta Infolabels
        private string[] MetaExtract(string src)
        {
            using (var reader = new StreamReader(src))
            {
                reader.ReadLine();
                var data = RemoveEmptyInfo(reader.ReadLine().Split('\t'));
                return data;
            }
        }
        //Assigns Metadata Infolabels
        private void MetaLabeling(string title, string remarks)
        {
            TitleLabel.Text = (title);
            RemarkLabel.Text = remarks;
        }
        //Returns all Categories of Parties
        private string[] ColorCategories(string src)
        {
            using (var reader = new StreamReader(src))
            {
                reader.ReadLine();
                reader.ReadLine();
                var data = RemoveEmptyInfo(reader.ReadLine().Split('\t'));
                return data;
            }
        }
        //Returns all possible Combinations of Categories of Parties, Filters by those that are actually contained in Main Dictionary
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
                    if(val1 != val2)
                    {
                        data.Add(val2 + " -> " + val1);
                    }
                }
            }
            foreach(string[] value in compareddict.Values)
            {
                removedata.Add(value[0]);
            }
            foreach(string key in data.ToArray())
            {
                if(!removedata.Contains(key) && key.Contains("->"))
                {
                    data.Remove(key);
                }
            }
            return data.ToArray();
        }
        //Find two main parties
        private string[] MainParties(string src)
        {
            using (var reader = new StreamReader(src))
            {
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                var data = RemoveEmptyInfo(reader.ReadLine().Split('\t'));
                return data;
            }
        }
        //Extracts Colors from Gradient Image
        private List<Color> ColorsExtract(Image src)
        {
            List<Color> Colors = new List<Color>();
            Bitmap gradient = new Bitmap(src);
            for(int x = 0; x < gradient.Width; x++)
            {
                Colors.Add(gradient.GetPixel(x, 0));
            }
            return Colors;
        }
        //Puts Vote Results into Dictionary
        private SortedDictionary<string, string[]> CreateDictionary(string src, int shift)
        {
            SortedDictionary<string, string[]> dict = new SortedDictionary<string, string[]>();
            using (var reader = new StreamReader(src))
            {
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                if(shift != 0)
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
                            double newvalr = Convert.ToDouble(values[3], format)+ (Convert.ToDouble((shift)) / Convert.ToDouble(2));
                            double newvald = Convert.ToDouble(values[4], format)- (Convert.ToDouble((shift)) / Convert.ToDouble(2));
                            string party = "";
                            string[] arrayvals = new string[] { values[1], values[2] };
                            if (values[1].Contains(mp[0]) || values[1].Contains(mp[1]) || values[1].Contains("Tie"))
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
                                    party = "Tie";
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
                            ErrorLabel.Text = "No %-Data for "+ src.Replace('/', ' ').Replace((Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "/Resources/DataFiles/").Replace('/', ' '), "").Replace(".tsv","");
                        }
                    }
                }
                else
                {
                    while (reader.EndOfStream == false)
                    {
                        var line = reader.ReadLine();
                        if(line == null)
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
        //Puts Statistics into List of Dictionaries
        private Dictionary<string, SortedDictionary<string, string[]>> CreateStatDictionary(string src)
        {
            Dictionary<string, SortedDictionary<string, string[]>> returnlist = new Dictionary<string, SortedDictionary<string, string[]>>();
            using (var reader = new StreamReader(src))
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
                    for( int i = 0; i < parties.Length; i++)
                    {
                        string[] arrayvals = new string[] { parties[i], values[i + 1] };
                        dict.Add(Convert.ToString(i), arrayvals);
                    }
                    returnlist.Add(title, dict);
                }
            }
            return returnlist;
        }
        //Iterates the Dictionaries and creates a bar for each one
        private void CreateMultipleBars(Dictionary<string, SortedDictionary<string, string[]>> maindict, string[] parties, List<Color> Colors, string[] MainParties)
        {
            int height = 100;
            foreach(string title in maindict.Keys)
            {
                DrawElectoralVotes(true, false, maindict[title], parties, Colors, MainParties, height, title, "%", "%");
                height += 70;
            }
        }
        //Removes Empty Array Values
        private string[] RemoveEmptyInfo(string[] s)
        {
            List<string> list = new List<string>();
            foreach(string st in s)
            {
                if(st != "")
                {
                    list.Add(st);
                }
            }
            return list.ToArray();
        }
        //Compares two dicitonaries and creates a new one
        private SortedDictionary<string, string[]> CreateComparedDictionary(SortedDictionary<string, string[]> dict1, SortedDictionary<string, string[]> dict2)
        {
            SortedDictionary<string, string[]> dict = new SortedDictionary<string, string[]>();
            foreach(string key in dict1.Keys)
            {
                if (dict2.Keys.Contains(key))
                {
                    if(dict1[key][0] == "NULL"  || dict2[key][0] == "NULL")
                    {
                        dict.Add(key,new string[] { "NULL", dict1[key][1]});
                    }
                    else if(dict1[key][0] == dict2[key][0])
                    {
                        dict.Add(key, new string[] { dict1[key][0], dict1[key][1]});
                    }
                    else
                    {
                        dict.Add(key, new string[] { (dict2[key][0]+" -> "+dict1[key][0]), dict1[key][1] });
                    }
                }
            }
            return dict;
        }
        //Puts all Party Names which actually appear into List
        public List<string> DictionaryValueList(SortedDictionary<string, string[]> dict)
        {
            List<string> s = new List<string>();
            foreach(string[] sarray in dict.Values)
            {
                s.Add(sarray[0]);
            }
            return s;
        }
        //Puts all Party Names whch actually appear into String
        public string DictionaryValueString(SortedDictionary<string, string[]> dict)
        {
            string s = "";
            foreach (string[] sarray in dict.Values)
            {
                s += sarray[0];
            }
            return s;
        }
        //Extract the Colors for Painting the Map from Image
        public List<Color> GetGradient(string src)
        {
            List<Color> Colors = ColorsExtract(Image.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "/Resources/colorgradients/"+src));
            return Colors;
        }
        //Creates Bar at Top showing Electoral Votes
        private void DrawElectoralVotes(bool countvotes, bool shiftdown, SortedDictionary<string, string[]> dict, string[] Parties, List<Color> Colors, string[] MainParties, int height, string candidates, string repvotetext, string demvotetext)
        {
            int curxpos = TitleLabel.Left;
            Label CandidatesLabel = new Label
            {
                Font = new Font("Consolas", 15.75F, FontStyle.Bold),
                Text = (candidates),
                AutoSize = true,
                ForeColor = Color.FloralWhite
            };
            this.Controls.Add(CandidatesLabel);
            GeneratedControls.Add(CandidatesLabel);
            CandidatesLabel.Location = new Point(curxpos, height - 30);
            CandidatesLabel.BringToFront();
            Label RepVotesLabel = new Label
            {
                Font = new Font("Consolas", 15.75F, FontStyle.Bold),
                Text = (candidates),
                AutoSize = true,
                ForeColor = Color.FloralWhite
            };
            this.Controls.Add(RepVotesLabel);
            GeneratedControls.Add(RepVotesLabel);
            RepVotesLabel.Location = new Point(curxpos, height + 5 + 30 * Convert.ToInt32(shiftdown));
            Label DemVotesLabel = new Label
            {
                Font = new Font("Consolas", 15.75F, FontStyle.Bold),
                Text = (candidates),
                AutoSize = true,
                ForeColor = Color.FloralWhite
            };
            this.Controls.Add(DemVotesLabel);
            GeneratedControls.Add(DemVotesLabel);
            if(CandidatesLabel.Top - TitleLabel.Height < TitleLabel.Top)
            {
                TitleLabel.Location = new Point(TitleLabel.Left, CandidatesLabel.Top - TitleLabel.Height);
            }
            List<double> Votes = new List<double>();
            for (int i = 0; i < Parties.Length; i++)
            {
                Votes.Add(0);
            }
            NumberFormatInfo format = new NumberFormatInfo
            {
                NumberDecimalSeparator = "."
            };
            foreach (var item in dict)
            {
                foreach (string party in Parties)
                {
                    if(item.Value[0] == party)
                    {
                        if (countvotes)
                        {
                            Votes[Array.IndexOf(Parties,party)] += Convert.ToDouble(item.Value[1], format);
                        }
                        else
                        {
                            Votes[Array.IndexOf(Parties, party)] += 1;
                        }

                    }
                }
            }
            for(int i = 0; i < Parties.Length; i++)
            {
                double width;
                if (countvotes)
                {
                    width = Convert.ToDouble(Votes[i]) / Convert.ToDouble(Votes.Sum()) * Convert.ToDouble(this.Width - TitleLabel.Left - 10);
                }
                else
                {
                    width = Convert.ToDouble(Votes[i]) / Convert.ToDouble(Votes.Sum()) * Convert.ToDouble(this.Width - TitleLabel.Left - 10);
                }
                Panel votespanel = new Panel
                {
                    Width = Convert.ToInt32(width),
                    BackColor = Colors[i],
                    Top = height,
                    Height = 34,
                    Left = curxpos
                };
                curxpos += votespanel.Width;
                this.Controls.Add(votespanel);
                GeneratedControls.Add(votespanel);
                votespanel.BringToFront();
            }
            RepVotesLabel.Text = repvotetext;
            DemVotesLabel.Text = demvotetext;
            if (countvotes)
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
            DemVotesLabel.Location = new Point(curxpos - DemVotesLabel.Width, height + 5 + 30 * Convert.ToInt32(shiftdown));
            if (!shiftdown)
            {
                RepVotesLabel.BackColor = Colors[Array.IndexOf(Parties, MainParties[0])];
                DemVotesLabel.BackColor = Colors[Array.IndexOf(Parties, MainParties[1])];
                RepVotesLabel.ForeColor = Color.FromArgb(40, 40, 40);
                DemVotesLabel.ForeColor = Color.FromArgb(40, 40, 40);
            }
            RepVotesLabel.BringToFront();
            DemVotesLabel.BringToFront();
        }
        //Draws Key
        private void DrawKeyBox(SortedDictionary<string, string[]> dict, string[] Parties, List<Color> Colors)
        {
            GroupBox KeyGroupBox = new GroupBox
            {
                ForeColor = Color.FloralWhite,
                Font = RemarkLabel.Font,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Text = "Key:",
                AutoSize = false,
                Visible = false
            };
            foreach (Control c in KeyGroupBox.Controls)
            {
                c.Visible = false;
            }
            int startypos = 20;
            bool passedbool = false;
            if (DictionaryValueString(dict).Contains("->") && !DictionaryValueList(dict)[0].Contains("->"))
            {
                Label legendname = new Label
                {
                    Font = RemarkLabel.Font,
                    Text = "Matching:",
                    AutoSize = true
                };
                KeyGroupBox.Controls.Add(legendname);
                legendname.Location = new Point(7, startypos);
                legendname.Visible = true;
                startypos += 15;
                passedbool = true;
            }
            foreach (string party in Parties)
            {
                if (((DictionaryValueList(dict).Contains(party))))
                {
                    if (party.Contains("->")&&passedbool)
                    {
                        Label legendnotice = new Label
                        {
                            Font = RemarkLabel.Font,
                            Text = "Compared to Historical:",
                            AutoSize = true
                        };
                        KeyGroupBox.Controls.Add(legendnotice);
                        legendnotice.Location = new Point(7, startypos);
                        legendnotice.Visible = true;
                        startypos += 15;
                        passedbool = false;
                    }
                    Panel legendcolour = new Panel();
                    Label legendname = new Label();
                    legendcolour.Visible = false;
                    legendname.Visible = false;
                    legendname.Font = RemarkLabel.Font;
                    legendname.Text = party;
                    legendcolour.Width = 10;
                    legendcolour.Height = 10;
                    legendname.AutoSize = true;
                    legendcolour.BackColor = Colors[Array.IndexOf(Parties, party)];
                    KeyGroupBox.Controls.Add(legendcolour);
                    KeyGroupBox.Controls.Add(legendname);
                    legendcolour.Location = new Point(7, startypos);
                    legendname.Location = new Point(21, startypos);
                    legendcolour.Visible = true;
                    legendname.Visible = true;
                    startypos += 15;
                }
            }
            this.Controls.Add(KeyGroupBox);
            GeneratedControls.Add(KeyGroupBox);
            KeyGroupBox.BringToFront();
            KeyGroupBox.AutoSize = true;
            KeyGroupBox.MaximumSize = new Size(int.MaxValue, startypos + 5);
            KeyGroupBox.Location = new Point(this.Size.Width - 10 - KeyGroupBox.Size.Width, this.Size.Height - 40 - KeyGroupBox.Size.Height);
            KeyGroupBox.Visible = true;
        }
        //Loads the Svg-Document
        private SvgDocument GetSvgFromDirectory(string src)
        {
            return SvgDocument.Open(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "/Resources/" + MetaExtract(src)[3]);
        }
        //Draws the Map
        private void DrawMap(SvgDocument map, SortedDictionary<string, string[]> dict, List<Color> Colors, string[] Parties)
        {
            List<List<string>> AssignedStates = new List<List<string>>();
            for(int i = 0; i < Parties.Length; i++)
            {
                AssignedStates.Add(new List<string>());
            }
            List<string> BLANKColors = new List<string>();
            //Groups different States Colors into List beforehand
            foreach (var item in dict)
            {
                foreach(string party in Parties)
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
            //Creates Map
            bool atleastonepainted = false;
            foreach(List<string> parties in AssignedStates)
            {
                foreach (string state in parties)
                {
                    try { map.GetElementById(state).Fill = new SvgColourServer(Colors[AssignedStates.IndexOf(parties)]); atleastonepainted = true; }
                    catch { }
                }
            }
            foreach(string state in BLANKColors)
            {
                map.GetElementById(state).Fill = new SvgColourServer(panel2.BackColor);
                atleastonepainted = true;
            }
            if (atleastonepainted)
            {
                mappanel.BackgroundImage = map.Draw();
                mappanel.Visible = true;
            }

        }
        //Creates Title for Shifting
        private string TitleShiftModifier(string title, string src, int shift)
        {
            if(ShiftResultsTrackBar.Visible && shift != 0)
            {
                string winningparty;
                if(shift > 0)
                {
                    winningparty = MainParties(src)[0];
                }
                else
                {
                    winningparty = MainParties(src)[1];
                }
                title += " (+" + Math.Abs(shift) + "% " + winningparty[0]+")";
            }
            return title;
        }
        //Clears all generated elements
        private void ClearDisplay()
        {
            foreach(Control c in GeneratedControls)
            {
                c.Visible = false;
                this.Controls.Remove(c);
                c.Dispose();
            }
            mappanel.Visible = false;
            RemarkLabel.Text = "";
            TitleLabel.Text = "";
        }
        //Main Function when button is clicked
        private void Button1_Click(object sender, EventArgs e)
        {
            ClearDisplay();
            string source = (Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName+"/Resources/DataFiles/" +Convert.ToString(listPublicationsHistorical.SelectedItem) + "/" + Convert.ToString(listYearsHistorical.SelectedItem)+".tsv");
            string sourcecompare = (Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "/Resources/DataFiles/" + Convert.ToString(listPublicationsCompare.SelectedItem) + "/" + Convert.ToString(listYearsCompare.SelectedItem) + ".tsv");
            int shift = 0;
            if (ShiftResultsTrackBar.Visible)
            {
                shift = ShiftResultsTrackBar.Value;
            }
            int shiftcompare = 0;
            if (ShiftResultsTrackBar.Visible)
            {
                shiftcompare = ShiftResultsTrackBarCompare.Value;
            }
            //try
            //{
                ErrorLabel.Text = "";
                if (listPublicationsCompare.Visible && SourceFormatType(source).Contains("MapVis") && SourceFormatType(sourcecompare).Contains("MapVis"))
                {
                    SortedDictionary<string, string[]> compareddict = CreateComparedDictionary(CreateDictionary(source, shift), CreateDictionary(sourcecompare, shiftcompare));
                    if(ErrorLabel.Text == "")
                    {
                        DrawMap(GetSvgFromDirectory(source), compareddict, new List<Color>(GetGradient(MetaExtract(source)[4]).Concat(GetGradient("colorgradientrandom.png"))), ColorComparedCategories(source, sourcecompare, compareddict));
                        DrawElectoralVotes(false, true, compareddict, ColorComparedCategories(source, sourcecompare, compareddict), new List<Color>(GetGradient(MetaExtract(source)[4]).Concat(GetGradient("colorgradientrandom.png"))), new string[] { "*", "*" }, 100, MetaExtract(sourcecompare)[0] + " compared to " + MetaExtract(source)[0],"","");
                        DrawKeyBox(compareddict, ColorComparedCategories(source, sourcecompare, compareddict), new List<Color>(GetGradient(MetaExtract(source)[4]).Concat(GetGradient("colorgradientrandom.png"))));
                        MetaLabeling((TitleShiftModifier(MetaExtract(sourcecompare)[2] + ", " + MetaExtract(sourcecompare)[1], sourcecompare, shiftcompare) + " -> " + TitleShiftModifier(MetaExtract(source)[2] + ", " + MetaExtract(source)[1], source, shift)), "Comparison, bar at top shows number of states in each category");
                    }
                }
                else if (SourceFormatType(source).Contains("MapVis"))
                {
                    SortedDictionary<string, string[]> dict = CreateDictionary(source, shift);
                    if(ErrorLabel.Text == "")
                    {
                        DrawMap(GetSvgFromDirectory(source), dict, GetGradient(MetaExtract(source)[4]), ColorCategories(source));
                        DrawElectoralVotes(true, true, dict, ColorCategories(source), GetGradient(MetaExtract(source)[4]), MainParties(source), 100, MetaExtract(source)[0],"","");
                        DrawKeyBox(dict, ColorCategories(source), GetGradient(MetaExtract(source)[4]));
                        MetaLabeling(TitleShiftModifier(MetaExtract(source)[2] + ", " + MetaExtract(source)[1], source, shift), MetaExtract(source)[5]);
                    }
                }
                else if(SourceFormatType(source).Contains("StatVis"))
                {
                    CreateMultipleBars(CreateStatDictionary(source), ColorCategories(source), GetGradient(MetaExtract(source)[4]), MainParties(source));
                    MetaLabeling(MetaExtract(source)[2] + ", " + MetaExtract(source)[1] + ", " + MetaExtract(source)[0], MetaExtract(source)[5]);
                }
                if(ErrorLabel.Text != "")
                {
                    ClearDisplay();
                }
            //}
            //catch
            //{
                //if(ErrorLabel.Text == "")
                //{
                    //ErrorLabel.Text = "Error occured.";
                //}
            //}
        }
        //UI Functionality, just don't look at anything below this line.
        private bool IsMapVis(string publication, string year)
        {
            bool returnval = false;
            try
            {
                if (SourceFormatType((Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "/Resources/DataFiles/" + publication + "/" + year + ".tsv")).Contains("MapVis"))
                {
                    returnval = true;
                }
            }
            catch { }
            return returnval; 
        }
        private void ExitLabel_MouseClick(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }
        private void ExitLabel_MouseEnter(object sender, EventArgs e)
        {
            ExitLabel.ForeColor = Color.LightGray;
        }
        private void ExitLabel_MouseLeave(object sender, EventArgs e)
        {
            ExitLabel.ForeColor = Color.FloralWhite;
        }
        private void ListPublications_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender == listPublicationsCompare)
            {
                List<string> years = GetOptionYear(Convert.ToString(listPublicationsCompare.SelectedItem));
                listYearsCompare.Items.Clear();
                foreach (string year in years)
                {
                    if (IsMapVis(Convert.ToString(listPublicationsCompare.SelectedItem), year))
                    {
                        listYearsCompare.Items.Add(year);
                    }
                }
            }
            if (sender == listPublicationsHistorical)
            {
                List<string> years = GetOptionYear(Convert.ToString(listPublicationsHistorical.SelectedItem));
                listYearsHistorical.Items.Clear();
                foreach (string year in years)
                {
                    listYearsHistorical.Items.Add(year);
                }
            }
        }
        private void MinimizeLabel_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void MinimizeLabel_MouseEnter(object sender, EventArgs e)
        {
            MinimizeLabel.ForeColor = Color.LightGray;
        }
        private void MinimizeLabel_MouseLeave(object sender, EventArgs e)
        {
            MinimizeLabel.ForeColor = Color.FloralWhite;
        }
        //I warned you...
        private void CompareLabel_Click(object sender, EventArgs e)
        {        
            if(CompareLabel.Text == "Compare ▲")
            {
                if (ShiftResultLabel.Text == "Shift Results ▼")
                {
                    ShiftResultsTrackBarCompare.Visible = true;
                    TrackBarTrackerCompare.Visible = true;
                    ShiftResultPanelCompare.Visible = true;
                    ZeroLabelCompare.Visible = true;
                    DLabelCompare.Visible = true;
                    RLabelCompare.Visible = true;
                }
                CompareLabel.Text = "Compare ▼";
                listYearsCompare.Visible = true;
                listPublicationsCompare.Visible = true;
                ShiftResultLabel.Top += listPublicationsCompare.Height;
                ShiftResultsTrackBar.Top += listPublicationsCompare.Height;
                TrackBarTracker.Top += listPublicationsCompare.Height;
                ShiftResultPanel.Top += listPublicationsCompare.Height;
                ZeroLabel.Top += listPublicationsCompare.Height;
                DLabel.Top += listPublicationsCompare.Height;
                RLabel.Top += listPublicationsCompare.Height;
                ShiftResultsTrackBarCompare.Top += listPublicationsCompare.Height;
                TrackBarTrackerCompare.Top += listPublicationsCompare.Height;
                ShiftResultPanelCompare.Top += listPublicationsCompare.Height;
                ZeroLabelCompare.Top += listPublicationsCompare.Height;
                DLabelCompare.Top += listPublicationsCompare.Height;
                RLabelCompare.Top += listPublicationsCompare.Height;
            }
            else if (CompareLabel.Text == "Compare ▼")
            {
                CompareLabel.Text = "Compare ▲";
                listYearsCompare.Visible = false;
                listPublicationsCompare.Visible = false;
                ShiftResultLabel.Top -= listPublicationsCompare.Height;
                ShiftResultsTrackBar.Top -= listPublicationsCompare.Height;
                TrackBarTracker.Top -= listPublicationsCompare.Height;
                ShiftResultPanel.Top -= listPublicationsCompare.Height;
                ZeroLabel.Top -= listPublicationsCompare.Height;
                DLabel.Top -= listPublicationsCompare.Height;
                RLabel.Top -= listPublicationsCompare.Height;
                ShiftResultsTrackBarCompare.Top -= listPublicationsCompare.Height;
                TrackBarTrackerCompare.Top -= listPublicationsCompare.Height;
                ShiftResultPanelCompare.Top -= listPublicationsCompare.Height;
                ZeroLabelCompare.Top -= listPublicationsCompare.Height;
                DLabelCompare.Top -= listPublicationsCompare.Height;
                RLabelCompare.Top -= listPublicationsCompare.Height;
                ShiftResultsTrackBarCompare.Visible = false;
                TrackBarTrackerCompare.Visible = false;
                ShiftResultPanelCompare.Visible = false;
                ZeroLabelCompare.Visible = false;
                DLabelCompare.Visible = false;
                RLabelCompare.Visible = false;
            }
        }

        private void ShiftResultLabel_Click(object sender, EventArgs e)
        {
            if (ShiftResultLabel.Text == "Shift Results ▲")
            {
                if (CompareLabel.Text == "Compare ▼")
                {
                    ShiftResultsTrackBarCompare.Visible = true;
                    TrackBarTrackerCompare.Visible = true;
                    ShiftResultPanelCompare.Visible = true;
                    ZeroLabelCompare.Visible = true;
                    DLabelCompare.Visible = true;
                    RLabelCompare.Visible = true;
                }
                ShiftResultLabel.Text = "Shift Results ▼";
                ShiftResultsTrackBar.Visible = true;
                TrackBarTracker.Visible = true;
                ShiftResultPanel.Visible = true;
                ZeroLabel.Visible = true;
                DLabel.Visible = true;
                RLabel.Visible = true;
            }
            else if (ShiftResultLabel.Text == "Shift Results ▼")
            {
                ShiftResultLabel.Text = "Shift Results ▲";
                ShiftResultsTrackBar.Visible = false;
                TrackBarTracker.Visible = false;
                ShiftResultPanel.Visible = false;
                ZeroLabel.Visible = false;
                DLabel.Visible = false;
                RLabel.Visible = false ;
                ShiftResultsTrackBarCompare.Visible = false;
                TrackBarTrackerCompare.Visible = false;
                ShiftResultPanelCompare.Visible = false;
                ZeroLabelCompare.Visible = false;
                DLabelCompare.Visible = false;
                RLabelCompare.Visible = false;
            }
        }
        private Point MouseDownLocation;
        private Point MouseDownLocationCompare;
        private void TrackBarTracker_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender.Equals(TrackBarTracker))
            {
                TrackBarTracker.ForeColor = Color.FloralWhite;
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    MouseDownLocation = e.Location;
                    TrackBarTracker.ForeColor = Color.LightGray;
                }
            }
            else if (sender.Equals(TrackBarTrackerCompare))
            {
                TrackBarTrackerCompare.ForeColor = Color.FloralWhite;
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    MouseDownLocationCompare = e.Location;
                    TrackBarTrackerCompare.ForeColor = Color.LightGray;
                }
            }
        }

        private void TrackBarTracker_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (sender.Equals(TrackBarTracker))
                {
                    if (e.X + TrackBarTracker.Left - MouseDownLocation.X >= 5 && e.X + TrackBarTracker.Left - MouseDownLocation.X < ShiftResultsTrackBar.Width - 15)
                    {
                        TrackBarTracker.Left = e.X + TrackBarTracker.Left - MouseDownLocation.X;
                        int newbarval = Convert.ToInt32(Math.Round(((Convert.ToDouble(TrackBarTracker.Left) - Convert.ToDouble(5)) / (Convert.ToDouble(ShiftResultsTrackBar.Width) - Convert.ToDouble(20)) * (Convert.ToDouble(ShiftResultsTrackBar.Maximum) - Convert.ToDouble(ShiftResultsTrackBar.Minimum)) - Convert.ToDouble(20))));
                        if (newbarval > 20)
                        {
                            newbarval = 20;
                        }
                        if (newbarval < -20)
                        {
                            newbarval = -20;
                        }
                        ShiftResultsTrackBar.Value = newbarval;
                    }
                }
                else if (sender.Equals(TrackBarTrackerCompare))
                {
                    if (e.X + TrackBarTrackerCompare.Left - MouseDownLocationCompare.X >= 5 && e.X + TrackBarTrackerCompare.Left - MouseDownLocationCompare.X < ShiftResultsTrackBarCompare.Width - 15)
                    {
                        TrackBarTrackerCompare.Left = e.X + TrackBarTrackerCompare.Left - MouseDownLocationCompare.X;
                        int newbarval = Convert.ToInt32(Math.Round(((Convert.ToDouble(TrackBarTrackerCompare.Left) - Convert.ToDouble(5)) / (Convert.ToDouble(ShiftResultsTrackBarCompare.Width) - Convert.ToDouble(20)) * (Convert.ToDouble(ShiftResultsTrackBarCompare.Maximum) - Convert.ToDouble(ShiftResultsTrackBarCompare.Minimum)) - Convert.ToDouble(20))));
                        if (newbarval > 20)
                        {
                            newbarval = 20;
                        }
                        if (newbarval < -20)
                        {
                            newbarval = -20;
                        }
                        ShiftResultsTrackBarCompare.Value = newbarval;
                    }
                }
            }
        }

        private void TrackBarTracker_MouseUp(object sender, MouseEventArgs e)
        {
            if (sender.Equals(TrackBarTracker))
            {
                TrackBarTracker.ForeColor = Color.FloralWhite;
            }
            else if (sender.Equals(TrackBarTrackerCompare))
            {
                TrackBarTrackerCompare.ForeColor = Color.FloralWhite;
            }
        }

        private void listYearsHistorical_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(IsMapVis(Convert.ToString(listPublicationsHistorical.SelectedItem), Convert.ToString(listYearsHistorical.SelectedItem)))
            {
                CompareLabel.Visible = true;
                ShiftResultLabel.Visible = true;
                if (CompareLabel.Text == "Compare ▼" && ShiftResultLabel.Text == "Shift Results ▼")
                {
                    ShiftResultsTrackBarCompare.Visible = true;
                    TrackBarTrackerCompare.Visible = true;
                    ShiftResultPanelCompare.Visible = true;
                    ZeroLabelCompare.Visible = true;
                    DLabelCompare.Visible = true;
                    RLabelCompare.Visible = true;
                }
                if(ShiftResultLabel.Text == "Shift Results ▼")
                {
                    ShiftResultsTrackBar.Visible = true;
                    TrackBarTracker.Visible = true;
                    ShiftResultPanel.Visible = true;
                    ZeroLabel.Visible = true;
                    DLabel.Visible = true;
                    RLabel.Visible = true;
                }
                if(CompareLabel.Text == "Compare ▼")
                {
                    listYearsCompare.Visible = true;
                    listPublicationsCompare.Visible = true;
                }
            }
            else
            {
                CompareLabel.Visible = false;
                ShiftResultLabel.Visible = false;
                ShiftResultsTrackBar.Visible = false;
                TrackBarTracker.Visible = false;
                ShiftResultPanel.Visible = false;
                ZeroLabel.Visible = false;
                DLabel.Visible = false;
                RLabel.Visible = false;
                ShiftResultsTrackBarCompare.Visible = false;
                TrackBarTrackerCompare.Visible = false;
                ShiftResultPanelCompare.Visible = false;
                ZeroLabelCompare.Visible = false;
                DLabelCompare.Visible = false;
                RLabelCompare.Visible = false;
                ShiftResultsTrackBarCompare.Visible = false;
                TrackBarTrackerCompare.Visible = false;
                ShiftResultPanelCompare.Visible = false;
                ZeroLabelCompare.Visible = false;
                DLabelCompare.Visible = false;
                RLabelCompare.Visible = false;
                listYearsCompare.Visible = false;
                listPublicationsCompare.Visible = false;
            }
        }
    }
}