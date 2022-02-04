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
        private int mymeanvotes = 0;
        private double myn = 0;
        private Dictionary<int, int> myyesvotes = new Dictionary<int,int>();
        private Dictionary<int, int> mynovotes = new Dictionary<int, int>();
        private Dictionary<int, double> myweights = new Dictionary<int, double>();
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
            myweights.Clear();
            foreach(Referendum r in refs)
            {
                if (myyesvotes.Keys.ToList().Contains(r.index))
                {
                    double weight = r.similarity;
                    myweights.Add(r.index, weight);
                    double votes = Convert.ToDouble(myyesvotes[r.index]) / Convert.ToDouble(myyesvotes[r.index] + mynovotes[r.index]);
                    n += weight;
                    probs += weight * votes;
                    totvotes += weight * Convert.ToDouble(myyesvotes[r.index] + mynovotes[r.index]);
                }
            }
            mymean = probs / n;
            foreach (Referendum r in refs)
            {
                if (myyesvotes.Keys.ToList().Contains(r.index))
                {
                    double weight = r.similarity;
                    double votes = Convert.ToDouble(myyesvotes[r.index]) / Convert.ToDouble(myyesvotes[r.index] + mynovotes[r.index]);
                    probssquare += weight * Math.Pow(votes - mymean, 2.0);
                }
            }
            mystdev = Math.Sqrt(probssquare / n /(refs.Count - 1)*refs.Count);
            mymeanvotes = Convert.ToInt32(totvotes / n);
            myn = n;
        }
        public double CantonCovariance(Canton otherc)
        {
            double xmean = mymean;
            double ymean = otherc.distribution.Mean;
            double runningsum = 0.0;
            double sumofweights = 0.0;
            foreach (int i in myyesvotes.Keys.ToList().Intersect(otherc.yesvotes.Keys.ToList()))
            {
                sumofweights += myweights[i];
                runningsum += myweights[i]*mymeanvotes*(((double)myyesvotes[i])/((double)(myyesvotes[i]+mynovotes[i])) - xmean) *otherc.meanvotes*(((double)otherc.yesvotes[i])/((double)(otherc.yesvotes[i]+otherc.novotes[i])) - ymean);
            }
            return runningsum / sumofweights;
        }
        public double CantonCovarianceNonWeighted(Canton otherc)
        {
            double runningsum = 0.0;
            double sumofweights = 0.0;
            foreach (int i in myyesvotes.Keys.ToList().Intersect(otherc.yesvotes.Keys.ToList()))
            {
                sumofweights += 1.0;
                runningsum += mymeanvotes * (((double)myyesvotes[i]) / ((double)(myyesvotes[i] + mynovotes[i])) - 0.5) * otherc.meanvotes * (((double)otherc.yesvotes[i]) / ((double)(otherc.yesvotes[i] + otherc.novotes[i])) - 0.5);
            }
            return runningsum / sumofweights;
        }
        public double weight
        {
            get { return myweight; }
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
            get {return new Normal(mymean, mystdev); }
        }
        public Dictionary<int, int> yesvotes
        {
            get
            {
                return myyesvotes;
            }
        }
        public Dictionary<int, int> novotes
        {
            get
            {
                return mynovotes;
            }
        }

    }
}
