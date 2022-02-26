using MathNet.Numerics.Distributions;
using System.Collections.Generic;
using Accord.Statistics.Distributions.Multivariate;
using System;
using System.Linq;

namespace ElectionPredictFinal.Pages.Classes
{
    class SimResults
    {
        private List<Canton> mybaselist = new List<Canton>();
        private double[] mymeanvector;
        private double[,] mycovariancematrix;
        private bool myStändemehr = false;
        private double mytotnationalvotes = 0.0;
        private MultivariateNormalDistribution mydist;
        private List<double[]> mysamples = new List<double[]>();
        private double mymeanpopvotes = 0.0;
        private double mymeanstände = 0.0;
        private double mypercentageyes = 0.0;
        private double mytotcantonweights = 0.0;
        private double myvariance = 0.0;
        private List<double> mypercentages = new List<double>();
        public SimResults(List<Canton> Cantons, bool stm)
        {
            mybaselist = Cantons;
            myStändemehr = stm;
            mymeanvector = new double[mybaselist.Count];
            mycovariancematrix = new double[mybaselist.Count, mybaselist.Count];
        }
        public void GenerateStructure()
        {
            Console.WriteLine("Generating Matrices");
            for (int i = 0; i < mybaselist.Count; i++)
            {
                mymeanvector[i] = mybaselist[i].meanvotes * mybaselist[i].distribution.Mean;
                mytotnationalvotes += mybaselist[i].meanvotes;
                mytotcantonweights += mybaselist[i].weight;
                for (int j = 0; j < mybaselist.Count; j++)
                {
                    mycovariancematrix[i, j] = mybaselist[i].CantonCovariance(mybaselist[j]);
                }
            }
            mydist = new MultivariateNormalDistribution(mymeanvector, mycovariancematrix);
        }
        public void AddSims(int i) {
            Console.WriteLine("Generating Samples");
            mysamples.AddRange(mydist.Generate(i).ToList<double[]>());
        }
        public void LoopAndCalc() {
            Console.WriteLine("Looping Samples");
            foreach(double[] d in mysamples)
            {
                double yesvotes = 0.0;
                double cantonyes = 0.0;
                for(int i = 0; i < d.Length; i++)
                {
                    yesvotes += d[i];
                    if(d[i] > mybaselist[i].meanvotes * 0.5)
                    {
                        cantonyes += mybaselist[i].weight;
                    }
                }
                mymeanpopvotes += yesvotes / mytotnationalvotes;
                mypercentages.Add(yesvotes / mytotnationalvotes);
                mymeanstände += cantonyes;
                if((!myStändemehr && yesvotes / mytotnationalvotes >= 0.5)||(yesvotes / mytotnationalvotes >= 0.5 && cantonyes >= mytotcantonweights * 0.5))
                {
                    mypercentageyes += 1;
                }
            }
            double variancetotal = 0.0;
            mymeanpopvotes /= (double)mysamples.Count;
            mymeanstände /= (double)mysamples.Count;
            mypercentageyes /= (double)mysamples.Count;
            foreach (double d in mypercentages)
            {
                variancetotal += Math.Pow(d - mymeanpopvotes, 2.0);

            }
            variancetotal /= (double)mypercentages.Count;
            myvariance = Math.Sqrt(variancetotal);
            Console.WriteLine("Finished Calc");
        }
        public double meanpopvote
        {
            get
            {
                return mymeanpopvotes;
            }
        }
        public double percentageyes
        {
            get
            {
                return mypercentageyes;
            }
        }
        public double meanstände
        {
            get
            {
                return mymeanstände;
            }
        }
        public Normal distribution
        {
            get
            {
                return new Normal(mymeanpopvotes, myvariance);
            }
        }
    }
}
