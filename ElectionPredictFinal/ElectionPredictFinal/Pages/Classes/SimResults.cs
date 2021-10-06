using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectionPredictFinal.Pages.Classes
{
    class SimResults
    {
        private List<Canton> mybaselist = new List<Canton>();
        private List<SingleResult> myresultlist = new List<SingleResult>();
        private bool myStändemehr = false;
        public SimResults(List<Canton> Cantons, bool stm)
        {
            mybaselist = Cantons;
            myStändemehr = stm;
        }
        public void RunSim(int numruns)
        {
            for(int i = 0; i < numruns; i++)
            {
                SingleResult st = new SingleResult(mybaselist);
                myresultlist.Add(st);
            }
        }
        public double meanpopvote
        {
            get
            {
                double tot = 0.0;
                foreach(SingleResult st in myresultlist)
                {
                    tot += st.nationalresult;
                }
                tot /= myresultlist.Count;
                return tot;
            }
        }
        public double percentageyes
        {
            get
            {
                double yesvotes = 0.0;
                foreach(SingleResult st in myresultlist)
                {
                    if((myStändemehr && st.Ständem && st.nationalresult >= 0.5)||(!myStändemehr && st.nationalresult >= 0.5))
                    {
                        yesvotes += 1.0;
                    }
                }
                yesvotes /= myresultlist.Count;
                return yesvotes;
            }
        }
        public double meanstände
        {
            get
            {
                List<double> allsts = new List<double>();
                foreach(SingleResult st in myresultlist)
                {
                    allsts.Add(st.Ständey);
                }
                allsts.Sort();
                return allsts[Convert.ToInt32(allsts.Count/2.0)];
            }
        }
        public Normal distribution
        {
            get
            {
                double tot = 0.0;
                double sqtot = 0.0;
                foreach (SingleResult st in myresultlist)
                {
                    tot += st.nationalresult;
                    sqtot += Math.Pow(st.nationalresult, 2.0);
                }
                tot /= myresultlist.Count;
                sqtot /= myresultlist.Count;
                return new Normal(tot, Math.Sqrt(sqtot - Math.Pow(tot, 2.0)));
            }
        }
    }
    class SingleResult
    {
        private Dictionary<Canton, double> mycantonsweight = new Dictionary<Canton, double>();
        private Dictionary<Canton, double> mycantonresults = new Dictionary<Canton, double>();
        private Random myr = new Random();
        private int myyesvotes = 0;
        private int mynovotes = 0;
        private double Styes = 0;
        private double Stno = 0;
        public SingleResult(List<Canton> Cantons)
        {
            foreach(Canton c in Cantons)
            {
                mycantonsweight.Add(c, 0.0);
            }
            GenerateResult(Cantons);
        }
        private void GenerateResult(List<Canton> Cantons)
        {
            List<Canton> CantonCopy = new List<Canton>();
            foreach(Canton c in Cantons)
            {
                CantonCopy.Add(c);
            }
            for(double i = 0; CantonCopy.Count > 0;i++)
            {
                Canton selcanton = CantonCopy[myr.Next(0, CantonCopy.Count - 1)];
                selcanton.GetRandResult(mycantonsweight[selcanton]);
                mycantonresults.Add(selcanton, selcanton.randval);
                myyesvotes += Convert.ToInt32(Convert.ToDouble(selcanton.meanvotes) * selcanton.randval);
                mynovotes += Convert.ToInt32(Convert.ToDouble(selcanton.meanvotes) * (1.0 - selcanton.randval));
                foreach(Canton c in CantonCopy)
                {
                    mycantonsweight[c] = (mycantonsweight[c] * Convert.ToDouble(i) + selcanton.correlations[c]) / Convert.ToDouble(i + 1);
                }
                if(selcanton.randval >= 0.5)
                {
                    Styes += selcanton.weight;
                }
                else
                {
                    Stno += selcanton.weight;
                }
                CantonCopy.Remove(selcanton);
            }
        }
        public double nationalresult
        {
            get { return Convert.ToDouble(myyesvotes) / Convert.ToDouble(myyesvotes + mynovotes); }
        }
        public double Ständey
        {
            get { return Styes; }
        }
        public bool Ständem
        {
            get { return Styes > Stno; }
        }
        public Dictionary<Canton, double> cantonresults
        {
            get { return mycantonresults; }
        }
    }
}
