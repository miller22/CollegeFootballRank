using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFBpredictor
{
    public class Game
    {
        public Team homeTeam;
        public Team awayTeam;
        public int homeScore;
        public int awayScore;

        public Game(Team home, Team away)
        {
            homeTeam = home;
            awayTeam = away;
        }

        public Game(Team home, Team away, double hScore, double aScore)
        {
            homeTeam = home;
            awayTeam = away;
            try
            {
                homeScore = Convert.ToInt32(Math.Round(hScore));
                awayScore = Convert.ToInt32(Math.Round(aScore));
            }

            catch
            {
                
            }
        }
    }
}
