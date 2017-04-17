using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFBpredictor
{
    public class Team
    {
        public double rating = 0;
        private string teamName;
        private string teamID;
        private double wins = 0;
        private double losses = 0;
        private double totalPoints = 0;
        private double totalPointsAllowed = 0;
        private double opponentWins = 0;
        private double opponentLosses = 0;
        private double opponentTotalPoints = 0;
        private double opponentTotalPointsAllowed = 0;
        private double oppAdjustedPPG;
        private double oppAdjustedDPPG;
        private string teamConference;
        private int rank;
        private int FBSRank;
        private int SOS;
        private double strength = 1.0;
        private double projectedPoints = 0;
        private List<Team> schedule = new List<Team>();

        /// <summary>
        /// Constructor for a Team
        /// </summary>
        /// <param name="name">name of the team</param>
        /// <param name="conference">conference the team belongs to</param>
        public Team(string name, string conference)
        {
            teamName = name;
            teamConference = conference;
        }

        /// <summary>
        /// Constructor for a Team
        /// </summary>
        /// <param name="name">name of the team</param>
        /// <param name="conference">conference the team belongs to</param>
        /// <param name="ID">ID of the team</param>
        public Team(string name, string conference, string ID)
        {
            teamName = name;
            teamConference = conference;
            teamID = ID;
        }

        /// <summary>
        /// Gets the name of the team
        /// </summary>
        /// <returns>the name of the team</returns>
        public string getTeamName()
        {
            return teamName;
        }

        /// <summary>
        /// Gets the ID of the team
        /// </summary>
        /// <returns>the team's ID</returns>
        public string getTeamID()
        {
            return teamID;
        }

        /// <summary>
        /// Gets the amount of wins the team has
        /// </summary>
        /// <returns>the team's number of wins</returns>
        public double getWins()
        {
            return wins;
        }

        /// <summary>
        /// Gets the amount of losses the team has
        /// </summary>
        /// <returns>the team's number of losses</returns>
        public double getLosses()
        {
            return losses;
        }

        /// <summary>
        /// Gets the points per game average of the team
        /// </summary>
        /// <returns>The team's points per game average</returns>
        public double getPPG()
        {
            if (wins + losses > 0)
            {
                return totalPoints / (wins + losses);
            }
            else return 0;
        }

        /// <summary>
        /// Gets the points allowed per game for the team
        /// </summary>
        /// <returns>The team's points allowed per game average</returns>
        public double getDefensePPG()
        {
            if (wins + losses > 0)
            {
                return totalPointsAllowed / (wins + losses);
            }
            else return 0;
        }

        /// <summary>
        /// Gets the a team's opponents' points per game average
        /// </summary>
        /// <returns>the team's opponents' points per game</returns>
        public double getOpponentPPG()
        {
            if (opponentWins + opponentLosses > 0)
            {
                return opponentTotalPoints / (opponentWins + opponentLosses);
            }
            else return 0;
        }

        /// <summary>
        /// Gets a team's opponents' points allowed per game average
        /// </summary>
        /// <returns>the team's opponents' points allowed per game</returns>
        public double getOpponentDefensePPG()
        {
            return opponentTotalPointsAllowed / (opponentWins + opponentLosses);
        }

        /// <summary>
        /// Gets the team's points per game average minus the average points per 
        /// game their opponents allow
        /// </summary>
        /// <returns>the team's points per game minus opponents points allowed per
        /// game</returns>
        public double getPPGvsOppAvg()
        {
            return getPPG() - getOpponentDefensePPG();
        }

        /// <summary>
        /// Gets the team's points allowed per game average minus the average points
        /// per game their opponents score
        /// </summary>
        /// <returns>the team's points allowed per game minus opponents points
        /// scored per game</returns>
        public double getDefensePPGvsOppAvg()
        {
            return getDefensePPG() - getOpponentPPG();
        }

        /// <summary>
        /// Gets the total number of points the team has scored
        /// </summary>
        /// <returns>The team's total points scored</returns>
        public double getTotalPoints()
        {
            return totalPoints;
        }

        /// <summary>
        /// Gets the total number of points the team has allowed
        /// </summary>
        /// <returns>The team's total points allowed</returns>
        public double getTotalPointsAllowed()
        {
            return totalPointsAllowed;
        }

        /// <summary>
        /// Gets the total number of wins the team's opponents have
        /// </summary>
        /// <returns>total number of wins by the team's opponents</returns>
        public double getOpponentWins()
        {
            return opponentWins;
        }

        /// <summary>
        /// Gets the total number of losses the team's opponents have
        /// </summary>
        /// <returns>total number of losses by the team's opponents</returns>
        public double getOpponentLosses()
        {
            return opponentLosses;
        }

        /// <summary>
        /// Gets the team's win percentage
        /// </summary>
        /// <returns>the team's winning percentage</returns>
        public double getWinPercentage()
        {
            if (wins + losses > 0)
            {
                return wins / (wins + losses);
            }
            else return 0;
        }

        /// <summary>
        /// Gets the win percentage for the team's opponents
        /// </summary>
        /// <returns>the win percentage for the team's opponents</returns>
        public double getOpponentWinPercentage()
        {
            if (opponentWins + opponentLosses > 0)
            {
                return opponentWins / (opponentWins + opponentLosses);
            }
            else return 0;
        }

        /// <summary>
        /// Get the total number of points the team's opponents have scored for
        /// the season
        /// </summary>
        /// <returns>the team's opponents' total points scored</returns>
        public double getOpponentTotalPoints()
        {
            return opponentTotalPoints;
        }

        /// <summary>
        /// Gets the total number of points the team's opponents have allowed for
        /// the season
        /// </summary>
        /// <returns>the team's opponents' total points allowed</returns>
        public double getOpponentTotalPointsAllowed()
        {
            return opponentTotalPointsAllowed;
        }

        /// <summary>
        /// Gets the team's opponents' points per game adjusted to their strength of
        /// schedule
        /// </summary>
        /// <returns>the team's opponents' adjusted points per game</returns>
        public double getOppAdjustedPPG()
        {
            return oppAdjustedPPG;
        }

        /// <summary>
        /// Gets the team's opponents' points per game allowed adjusted to their
        /// strength of schedule
        /// </summary>
        /// <returns>the team's opponents' adjusted points allowed per game</returns>
        public double getOppAdjustedDPPG()
        {
            return oppAdjustedDPPG;
        }

        /// <summary>
        /// Gets the team's conference
        /// </summary>
        /// <returns>the team's conference</returns>
        public String getTeamConference()
        {
            return teamConference;
        }

        /// <summary>
        /// Gets the team's overall rank
        /// </summary>
        /// <returns>the team's overall rank</returns>
        public int getRank()
        {
            return rank;
        }

        /// <summary>
        /// Gets the team's FBS rank
        /// </summary>
        /// <returns>the team's FBS rank</returns>
        public int getFBSRank()
        {
            return FBSRank;
        }

        /// <summary>
        /// Gets the team's strength of schedule rank
        /// </summary>
        /// <returns>the team's strength of schedule rank</returns>
        public int getSOSRank()
        {
            return SOS;
        }

        /// <summary>
        /// Gets the team's strength of schedule rating
        /// </summary>
        /// <returns>the team's strength of schedule rating</returns>
        public double getStrength()
        {
            return strength;
        }

        /// <summary>
        /// Gets the number points the team is expected to score
        /// </summary>
        /// <returns>the number of points the team is expected to
        /// score</returns>
        public double getPoints()
        {
            return projectedPoints;
        }

        /// <summary>
        /// Gets the team's schedule as a list of Teams
        /// </summary>
        /// <returns>the team's schedule</returns>
        public List<Team> getSchedule()
        {
            return schedule;
        }

        /// <summary>
        /// Gets the team's opponent at the location of the passed
        /// in index
        /// </summary>
        /// <param name="index">index of the opponent to get</param>
        /// <returns>the opponent at the passed in index</returns>
        public Team getOpponent(int index)
        {
            return schedule[index];
        }

        /// <summary>
        /// Increments the team's win count by 1
        /// </summary>
        public void increaseWins()
        {
            wins++;
        }

        /// <summary>
        /// Increments the team's loss count by 1
        /// </summary>
        public void increaseLosses()
        {
            losses++;
        }

        /// <summary>
        /// Increases the team's total points scored for the season
        /// </summary>
        /// <param name="points">the amount to increase the total points by</param>
        public void increaseTotalPoints(int points)
        {
            totalPoints += points;
        }

        /// <summary>
        /// Increases the team's total points allowed for the season
        /// </summary>
        /// <param name="points">the amount to increase the points allowed by</param>
        public void increaseTotalPointsAllowed(int points)
        {
            totalPointsAllowed += points;
        }

        /// <summary>
        /// Increases the team's opponents' total win count
        /// </summary>
        /// <param name="opponentWins">the amount of wins to increase by</param>
        public void increaseOpponentWins(double opponentWins)
        {
            this.opponentWins += opponentWins;
        }

        /// <summary>
        /// Increases the team's opponents' total loss count
        /// </summary>
        /// <param name="opponentLosses">the amount of losses to increase by</param>
        public void increaseOpponentLosses(double opponentLosses)
        {
            this.opponentLosses += opponentLosses;
        }

        /// <summary>
        /// Increases the team's opponents' total points scored
        /// </summary>
        /// <param name="points">the amount of points to increase by</param>
        public void increaseOpponentTotalPoints(int points)
        {
            opponentTotalPoints += points;
        }

        /// <summary>
        /// Increases the team's opponents' total points allowed
        /// </summary>
        /// <param name="points">The amount of points to increase by</param>
        public void increaseOpponentTotalPointsAllowed(int points)
        {
            opponentTotalPointsAllowed += points;
        }

        /// <summary>
        /// Sets the opponent adjusted points per game for the team
        /// </summary>
        /// <param name="low">the low value for any team's points per game
        /// compared to their opponents average points allowed</param>
        public void setOppAdjustedPPG(double low)
        {
            oppAdjustedPPG = 2 * getPPGvsOppAvg() - low - 8.5;
        }

        /// <summary>
        /// Sets the opponent adjusted points allowed per game for the team
        /// </summary>
        /// <param name="high">the high value for any team's points allowed
        /// per game compared to their opponents average points scored</param>
        public void setOppAdjustedDPPG(double high)
        {
            oppAdjustedDPPG = -((getDefensePPGvsOppAvg() - high - 1) * ((129 - Convert.ToDouble(getSOSRank())) / 100)
                              + getDefensePPGvsOppAvg() + 11.5);
        }

        /// <summary>
        /// Sets the rank for the team
        /// </summary>
        /// <param name="rank">the rank for the team</param>
        public void setRank(int rank)
        {
            this.rank = rank;
        }

        /// <summary>
        /// Sets the FBSRank for the team
        /// </summary>
        /// <param name="rank">the FBSRank for the team</param>
        public void setFBSRank(int rank)
        {
            FBSRank = rank;
        }

        /// <summary>
        /// Sets the strength of schedule rank for the team
        /// </summary>
        /// <param name="SOS">the strength of schedule for the team</param>
        public void setSOSRank(int SOS)
        {
            this.SOS = SOS;
        }

        /// <summary>
        /// Sets the strength of the team's schedule
        /// </summary>
        /// <param name="strength">the strength of the team's schedule</param>
        public void setStrength(double strength)
        {
            this.strength = strength;
        }

        /// <summary>
        /// Sets the number of points the team is projected to score
        /// </summary>
        /// <param name="projectedPoints">the points the team is projected to score</param>
        public void setPoints(double projectedPoints)
        {
            this.projectedPoints = projectedPoints;
        }

        /// <summary>
        /// Adds an opponent to the team's schedule
        /// </summary>
        /// <param name="team">the team to add to the schedule</param>
        public void addOpponent(Team team)
        {
            schedule.Add(team);
        }

        /// <summary>
        /// Gets the number of teams in the schedule
        /// </summary>
        /// <returns>the number of teams in the schedule</returns>
        public int getScheduleSize()
        {
            return schedule.Count;
        }

        /// <summary>
        /// Sets all statistical values to 0 and clears the team's schedule
        /// </summary>
        public void Clear()
        {
            rating = 0;
            FBSRank = 0;
            wins = 0;
            losses = 0;
            opponentWins = 0;
            opponentLosses = 0;
            totalPoints = 0;
            totalPointsAllowed = 0;
            projectedPoints = 0;
            opponentTotalPoints = 0;
            opponentTotalPointsAllowed = 0;
            rank = 0;
            strength = 0;
            SOS = 0;
            schedule.Clear();
        }

        /// <summary>
        /// Sets the value of points to 0
        /// </summary>
        public void ClearPoints()
        {
            projectedPoints = 0;
        }

        /// <summary>
        /// Sets the value of FBSRank to 0
        /// </summary>
        public void ClearFBSRank()
        {
            FBSRank = 0;
        }
    }
}
