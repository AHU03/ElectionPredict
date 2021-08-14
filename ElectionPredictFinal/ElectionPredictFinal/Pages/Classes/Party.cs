using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ElectionPredictFinal.Pages.Classes
{
    class Party
    {
        private Dictionary<int, Vote> mymaindict = new Dictionary<int, Vote>();
        private string mypartyname;
        public Party(string source)
        {
            mypartyname = source;
            source += ".tsv";
            using (var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetManifestResourceNames().Single(str => str.Contains(source)))))
            {
                var data = reader.ReadToEnd().Split('\n');
                foreach (var line in data)
                {
                    if (line != "")
                    {
                        Vote v = new Vote(line);
                        mymaindict.Add(v.index, v);
                    }
                }
            }
        }
        public List<int> ContainedVotes
        {
            get { return mymaindict.Keys.ToList(); }
        }
        public string partyname
        {
            get { return mypartyname; }
        }
        public List<Vote> RequestVotes(List<int> requested)
        {
            List<Vote> returnlist = new List<Vote>();
            foreach(int i in requested)
            {
                returnlist.Add(mymaindict[i]);
            }
            return returnlist;
        }
    }
}
