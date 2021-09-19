using System;
using System.Collections.Generic;
using System.Text;

namespace ElectionPredictFinal.Pages.Classes
{
    class Canton
    {
        private string myshorthand = "";
        private Dictionary<int, int> myyesvotes = new Dictionary<int,int>();
        private Dictionary<int, int> mynovotes = new Dictionary<int, int>();
        
        public Canton(string shorthand)
        {
            myshorthand = shorthand;
        }

        public void AddReferendum(int index, string inputstring)
        {
            var input = inputstring.Split(';');
            foreach(string s in input)
            {
                if(s.Split(',')[0] == myshorthand)
                {
                    myyesvotes.Add(index, Convert.ToInt32(s.Split(',')[1]));
                    mynovotes.Add(index, Convert.ToInt32(s.Split(',')[2]));
                }
            }
        }
        /*public List<Referendum> ReferendumDistribution()
        {

        }*/
    }
}
