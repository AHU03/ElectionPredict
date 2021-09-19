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
        private string myarea = "";
        private Dictionary<string, int[]> mycantonvotes = new Dictionary<string, int[]>();
        private Dictionary<string, int> mypartydecisions = new Dictionary<string, int>();
        private Dictionary<string, double> mypartyvotes = new Dictionary<string, double>();
        private double mysimilarity = 0.0;
        private double mydirection = 0.0;
        public Referendum(string source)
        {
            var data = source.Split('\t');
            myindex = Convert.ToInt32(Convert.ToDouble(data[0]) * 10);
            myyear = Convert.ToInt32(data[1]);
            mytitle = data[2];
            mytype = Convert.ToInt32(data[3]);
            myarea = data[4];
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
        public void CalculateSimilarties(int type, string area, Dictionary<string, int> partydecision)
        {
            mysimilarity = 0;
            if(mytype == type)
            {
                mysimilarity += 0.4;
            }
            if(area.Split('.')[0] == myarea.Split('.')[0])
            {
                mysimilarity += 0.2;
                if(area.Split('.')[1] == myarea.Split('.')[1])
                {
                    mysimilarity += 0.2;
                    if (area == myarea)
                    {
                        mysimilarity += 0.2;
                    }
                }
            }
            mydirection = 0;
            foreach(string s in partydecision.Keys)
            {
                if (mysimilarity > 0.7)
                {
                    Console.WriteLine(s);
                }
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
            if(mysimilarity > 0.7)
            {
                Console.WriteLine(mysimilarity + ", " + mydirection + ", " + myindex + ": " + mytitle);
            }
        }
    }
}
