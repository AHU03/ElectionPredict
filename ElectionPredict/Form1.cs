﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
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
        }
        //Window Functionality
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
        //Extracts Metadata
        public string SourceFormatType(string src)
        {
            using (var reader = new StreamReader(src))
            {
                var data = reader.ReadLine();
                return data;
            }
        }
        public string[] MetaExtract(string src)
        {
            using (var reader = new StreamReader(src))
            {
                reader.ReadLine();
                var data = reader.ReadLine().Split(';');
                return data;
            }
        }
        //Assigns Metadata Infolabels
        private void MetaLabeling(string src)
        {
            TitleLabel.Text = (MetaExtract(src)[3] + ", " + MetaExtract(src)[2]);
            CandidatesLabel.Text = (MetaExtract(src)[0] + " v. " + MetaExtract(src)[1]);
            RemarkLabel.Text = MetaExtract(src)[6];
        }
        public string[] ColorCategories(string src)
        {
            using (var reader = new StreamReader(src))
            {
                reader.ReadLine();
                reader.ReadLine();
                var data = reader.ReadLine().Split(';');
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
        private Dictionary<string, string[]> CreateDictionary(string src)
        {
            Dictionary<string, string[]> dict = new Dictionary<string, string[]>();
            using (var reader = new StreamReader(src))
            {
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                while (reader.EndOfStream == false)
                {
                    var line = reader.ReadLine();
                    if(line == null)
                    {
                        break;
                    }
                    var values = line.Split(';');
                    string[] arrayvals = new string[] { values[1], values[2] };
                    dict.Add(values[0], arrayvals);
                }
            }
            return dict;
        }
        public List<Color> GetGradient(string src)
        {
            string[] Parties = ColorCategories(src);
            List<Color> Colors = ColorsExtract(Image.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "/Resources/"+MetaExtract(src)[5]));
            return Colors;
        }
        //Creates Bar at Top showing Electoral Votes
        private void DrawElectoralVotes(string src)
        {
            Dictionary<string, string[]> dict = CreateDictionary(src);
            List<int> Votes = new List<int>();
            string[] Parties = ColorCategories(src);
            List<Color> Colors = GetGradient(src);
            for (int i = 0; i < Parties.Length; i++)
            {
                Votes.Add(0);
            }
            foreach (var item in dict)
            {
                foreach (string party in Parties)
                {
                    if(item.Value[0] == party)
                    {
                        Votes[Array.IndexOf(Parties,party)] = Votes[Array.IndexOf(Parties, party)] + Convert.ToInt32(item.Value[1]);
                    }
                }
            }
            int curxpos = 226;
            for(int i = 0; i < Parties.Length; i++)
            {
                Panel votespanel = new Panel
                {
                    Width = Convert.ToInt32(Convert.ToDouble(Votes[i]) / Convert.ToDouble(Votes.Sum()) * Convert.ToDouble(883)),
                    BackColor = Colors[i],
                    Top = 167,
                    Height = 34,
                    Left = curxpos
                };
                curxpos = curxpos + votespanel.Width;
                this.Controls.Add(votespanel);
                votespanel.BringToFront();
            }
            int REPvotes = 0;
            int DEMvotes = 0;
            for(int i = 0; i < Votes.Count; i++)
            {
                if (Convert.ToDouble(i)+1 < Convert.ToDouble(Votes.Count) / Convert.ToDouble(2))
                {
                    REPvotes = REPvotes + Votes[i];
                }
                if (Convert.ToDouble(i)+1 > Convert.ToDouble(Votes.Count) / Convert.ToDouble(2))
                {
                    DEMvotes = DEMvotes + Votes[i];
                }
            }
            RepVotesLabel.Text = Convert.ToString(REPvotes);
            DemVotesLabel.Text = Convert.ToString(DEMvotes);
            foreach (Control c in KeyGroupBox.Controls)
            {
                c.Visible = false;
            }
            int startypos = 23;
            foreach (string party in Parties)
            {
                Panel legendcolour = new Panel();
                Label legendname = new Label();
                legendcolour.Visible = false;
                legendname.Visible = false;
                legendname.Font = RemarkLabel.Font;
                legendname.Text = party;
                legendcolour.Width = 15;
                legendcolour.Height = 15;
                legendname.Width = 140;
                legendcolour.BackColor = Colors[Array.IndexOf(Parties, party)];
                KeyGroupBox.Controls.Add(legendcolour);
                KeyGroupBox.Controls.Add(legendname);
                legendcolour.Location = new Point(7,startypos);
                legendname.Location = new Point(26, startypos);
                legendcolour.Visible = true;
                legendname.Visible = true;
                startypos = startypos + 20;
            }
            KeyGroupBox.Visible = true;
            this.Invalidate();
        }
        private void DrawMap(string src)
        {
            var map = SvgDocument.Open(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName+"/Resources/"+MetaExtract(src)[4]);
            Dictionary<string, string[]> dict = CreateDictionary(src);
            List<Color> Colors = GetGradient(src);
            string[] Parties = ColorCategories(src);
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
            foreach(List<string> parties in AssignedStates)
            {
                foreach (string state in parties)
                {
                    try { map.GetElementById(state).Fill = new SvgColourServer(Colors[AssignedStates.IndexOf(parties)]); }
                    catch { }
                }
            }
            foreach(string state in BLANKColors)
            {
                map.GetElementById(state).Fill = new SvgColourServer(panel2.BackColor);
            }
            mappanel.BackgroundImage = map.Draw();
            mappanel.Visible = true;
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

        private void button1_Click(object sender, EventArgs e)
        {
            string source = (Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName+"/Resources/DataFiles/" +Convert.ToString(listPublications.SelectedItem) + "/" + Convert.ToString(listYears.SelectedItem)+".csv");
            mappanel.Tag = source;
            try
            {
                if (SourceFormatType(source) == "MapVis")
                {
                    DrawMap(source);
                    DrawElectoralVotes(source);
                    MetaLabeling(source);
                }
            }
            catch { }
        }

        private void listPublications_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToString(listPublications.SelectedItem) == "Literary Digest")
            {
                listYears.Items.Clear();
                for (int i = 1916; i < 1937; i = i + 4)
                {
                    listYears.Items.Add(i);
                }
            }
            else if (Convert.ToString(listPublications.SelectedItem) == "Gallup")
            {
                listYears.Items.Clear();
                for (int i = 1936; i < 2013; i = i + 4)
                {
                    listYears.Items.Add(i);
                }
            }
            else if (Convert.ToString(listPublications.SelectedItem) == "Real Results")
            {
                listYears.Items.Clear();
                for (int i = 1916; i < 2021; i = i + 4)
                {
                    listYears.Items.Add(i);
                }
            }
            else
            {
                listYears.Items.Clear();
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
    }
}
