using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ElectionPredictFinal.Pages.Classes
{
    class Referendum
    {
        private int myindex = 0;
        private int myyear = 0;
        private string mytitle = "";
        private int mytype = 0;
        private string[] myarea;
        private Dictionary<string, int[]> mycantonvotes = new Dictionary<string, int[]>();
        private Dictionary<string, int> mypartydecisions = new Dictionary<string, int>();
        private Dictionary<string, double> mypartyvotes = new Dictionary<string, double>();
        private double mysimilarity = 0.0;
        private double mydirection = 0.0;
        private double myyearcloseness = 0.0;
        public Referendum(string source)
        {
            var data = source.Split('\t');
            myindex = Convert.ToInt32(Convert.ToDouble(data[0]) * 10);
            myyear = Convert.ToInt32(data[1]);
            mytitle = data[2];
            mytype = Convert.ToInt32(data[3]);
            myarea = data[4].Split(';');
            foreach (var datapoint in data[5].Split(';'))
            {
                if(datapoint.Length > 0)
                {
                    var splitdatapoint = datapoint.Split(',');
                    mycantonvotes.Add(splitdatapoint[0], new int[] { Convert.ToInt32(splitdatapoint[1]), Convert.ToInt32(splitdatapoint[2]) });
                }

            }
            foreach (var datapoint in data[6].Split(';'))
            {
                if (datapoint.Length > 0)
                {
                    var splitdatapoint = datapoint.Split(',');
                    mypartydecisions.Add(splitdatapoint[0], Convert.ToInt32(splitdatapoint[1]));
                }

            }
            using (var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetManifestResourceNames().Single(str => str.Contains("modelpartyvotes.tsv")))))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    mypartyvotes.Add(line.Split('\t')[0], Convert.ToDouble(line.Split('\t')[1]));
                }
            }
        }
        public void CalculateSimilarties(int type, string area, string year, Dictionary<string, int> partydecision)
        {
            mysimilarity = 0;
            foreach(string s in myarea)
            {
                double sim = 0.0;
                if (mytype == type)
                {
                    sim += 0.3;
                }
                if (area.Split('.')[0] == s.Split('.')[0])
                {
                    sim += 0.1;
                    if (area.Split('.')[1] == s.Split('.')[1])
                    {
                        sim += 0.2;
                        if (area == s)
                        {
                            sim += 0.4;
                        }
                    }
                }
                if(sim > mysimilarity)
                {
                    mysimilarity = sim;
                }
            }
            mydirection = 0;
            foreach(string s in partydecision.Keys)
            {
                if (mypartydecisions.Keys.ToList().Contains(s.ToLower()))
                {
                    if(mypartydecisions[s.ToLower()] == partydecision[s])
                    {
                        mydirection += mypartyvotes[s.ToLower()];
                    }
                    else
                    {
                        mydirection -= mypartyvotes[s.ToLower()];
                    }
                }
            }
            myyearcloseness = 0.0;
            myyearcloseness = 1.0 - (Convert.ToDouble(Math.Abs(myyear - Convert.ToInt32(year))) / Convert.ToDouble(Math.Abs(DateTime.Now.Year - 1866)));
        }
        public double similarity
        {
            get { return mysimilarity; }
        }
        public Dictionary<string, int[]> cantonvotes
        {
            get { return mycantonvotes; }
        }
        public string title
        {
            get { return mytitle; }
        }
        public int index
        {
            get { return myindex; }
        }
        public int year
        {
            get { return myyear; }
        }
        public double direction
        {
            get { return mydirection; }
        }
        public double yeardifference
        {
            get { return myyearcloseness; }
        }
    }
}
