using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFBpredictor
{
    public class Conference
    {
        public string conferenceName;
        public double rating = 0;
        public int rank;
        public double numTeams;
        public List<Team> members = new List<Team>();

        public Conference(string name)
        {
            conferenceName = name;
            numTeams = members.Count;
        }
    }
}
