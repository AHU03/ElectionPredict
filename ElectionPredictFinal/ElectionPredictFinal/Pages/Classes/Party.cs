using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ElectionPredictFinal.Pages.Classes
{
    public class Party
    {
        private Dictionary<int, Vote> mymaindict = new Dictionary<int, Vote>();
        private string mypartyname;
        private string mypartyshorthand;
        public Party(string source)
        {
            mypartyname = source;
            source += ".tsv";
            using (var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetManifestResourceNames().Single(str => str.Contains(source)))))
            {
                mypartyshorthand = reader.ReadLine();
                while(!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if(line.Length > 5)
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
        public string partyshorthand
        {
            get { return mypartyshorthand; }
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
        public List<Vote> RequestVotes(string s)
        {
            List<Vote> returnlist = new List<Vote>();
            foreach (Vote v in mymaindict.Values)
            {
                if(v.endorsement == s)
                {
                    returnlist.Add(v);
                }
            }
            return returnlist;
        }
    }
}
