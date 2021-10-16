using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MathNet.Numerics.Distributions;

namespace ElectionPredictFinal.Pages.Classes
{
    class Canton
    {
        private string myshorthand = "";
        private string myname = "";
        private double mymean = 0.0;
        private double mystdev = 0.0;
        private double mylastval = 0.0;
        private int mymeanvotes = 0;
        private double myn = 0;
        private Dictionary<int, int> myyesvotes = new Dictionary<int,int>();
        private Dictionary<int, int> mynovotes = new Dictionary<int, int>();
        private Dictionary<Canton, double> mycantoncorrelations = new Dictionary<Canton, double>();
        private double myweight = 0.0;
        public Canton(string shorthand, string name, double weight)
        {
            myshorthand = shorthand;
            myname = name;
            myweight = weight;
        }
        public void AddReferendum(Referendum r)
        {
            if (r.cantonvotes.Keys.ToList().Contains(myshorthand))
            {
                myyesvotes.Add(r.index, r.cantonvotes[myshorthand][0]);
                mynovotes.Add(r.index, r.cantonvotes[myshorthand][1]);
            }
        }
        public void ReferendumDistribution(List<Referendum> refs)
        {
            double n = 0.0;
            double probs = 0.0;
            double probssquare = 0.0;
            double totvotes = 0.0;
            foreach(Referendum r in refs)
            {
                if (myyesvotes.Keys.ToList().Contains(r.index) && r.direction >= 0)
                {
                    double weight = Math.Pow(r.similarity * r.direction * r.yeardifference, 3.0);
                    double votes = Convert.ToDouble(myyesvotes[r.index]) / Convert.ToDouble(myyesvotes[r.index] + mynovotes[r.index]);
                    n += weight;
                    probs += weight * votes;
                    probssquare += weight * Math.Pow(votes,2.0);
                    totvotes += weight * Convert.ToDouble(myyesvotes[r.index] + mynovotes[r.index]);
                }
            }
            mymean = probs / n;
            mystdev = Math.Sqrt(probssquare / n - Math.Pow(mymean, 2.0)) * 3 / Math.Max(n, 0.5);
            mymeanvotes = Convert.ToInt32(totvotes / n);
            myn = n;
            GetRandResult(0.0);
        }
        public void CantonsCorrelations(List<Canton> cantons)
        {
            double x = 0.0;
            double y = 0.0;
            foreach(Canton c in cantons)
            {
                foreach (int i in myyesvotes.Keys.ToList().Intersect(c.myyesvotes.Keys.ToList()))
                {
                    x += myyesvotes[i] / (myyesvotes[i] + mynovotes[i]);
                    y += c.myyesvotes[i] / (c.myyesvotes[i] + c.mynovotes[i]);
                };
                x /= Convert.ToDouble(myyesvotes.Keys.Count);
                y /= Convert.ToDouble(myyesvotes.Keys.Count);
                double top = 0.0;
                double bot1 = 0.0;
                double bot2 = 0.0;
                foreach (int i in myyesvotes.Keys.ToList().Intersect(c.myyesvotes.Keys.ToList()))
                {
                    top += (myyesvotes[i] - x) * (c.myyesvotes[i] - y);
                    bot1 += (myyesvotes[i] - x) * (myyesvotes[i] - x);
                    bot2 += (c.myyesvotes[i] - y) * (c.myyesvotes[i] - y);
                }
                double r = top / Math.Sqrt(bot1 * bot2);
                mycantoncorrelations.Add(c, r);
            }
        }
        public double weight
        {
            get { return myweight; }
        }
        public double randval
        {
            get { return mylastval; }
        }
        public Dictionary<Canton, double> correlations
        {
            get
            {
                Dictionary<Canton, double> returndict = new Dictionary<Canton, double>();
                foreach (Canton c in mycantoncorrelations.Keys.ToList())
                {
                    returndict.Add(c, (mycantoncorrelations[c] - 0.9) *(mylastval - mymean));
                }
                return returndict;
            }
        }
        public void GetRandResult(double weight)
        {
            Normal dist = new Normal(mymean + weight, mystdev);
            mylastval = Math.Min(1.0, Math.Max(0.0, dist.Sample()));
        }
        public int meanvotes
        {
            get { return mymeanvotes; }
        }
        public string confidence
        {
            get
            {
                if(myn < 1.5)
                {
                    return "Tief";
                }
                else if(myn < 2.5)
                {
                    return "Mässig";
                }
                else
                {
                    return "Gut";
                }
            }
        }
        public string shorthand
        {
            get { return myshorthand; }
        }
        public string name
        {
            get { return myname; }
        }
        public Normal distribution
        {
            get { return new Normal(mymean, mystdev); }
        }
    }
}
