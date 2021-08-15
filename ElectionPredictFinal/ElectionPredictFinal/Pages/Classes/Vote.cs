﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ElectionPredictFinal.Pages.Classes
{
    public class Vote
    {
        private int myindex;
        private string mytitle;
        private int myyear;
        private string mydomain;
        private string myadopted;
        private double mypercentageyes;
        private double mypartystrength;
        private string myendorsement;
        private int mynumsections;
        private string[] mysections;
        public Vote(string line)
        {
            NumberFormatInfo comma = new NumberFormatInfo();
            comma.NumberDecimalSeparator = ",";
            NumberFormatInfo dot = new NumberFormatInfo();
            dot.NumberDecimalSeparator = ".";
            var data = line.Split('\t');
            myindex = Convert.ToInt32(Convert.ToDouble(data[0])*10);
            mytitle = data[1];
            myyear = Convert.ToInt32(data[2]);
            mydomain = data[3];
            myadopted = data[4];
            mypercentageyes = Convert.ToDouble(data[5], dot);
            mypartystrength = Convert.ToDouble(data[6], comma);
            myendorsement = data[7];
            mynumsections = Convert.ToInt32(data[8]);
            mysections = data[9].Split(';');
        }
        public int index
        {
            get { return myindex; }
        }
        public string title { get { return mytitle; } }
        public int year { get { return myyear; } }
        public string domain { get { return mydomain; } }
        public string adopted { get { return myadopted; } }
        public double percentageyes { get { return mypercentageyes; } }
        public double partystrength { get { return mypartystrength; } }
        public string endorsement { get { return myendorsement; } }
        public int numsections { get { return mynumsections; } }
        public string[] sections { get { return mysections; } }
    }
}
