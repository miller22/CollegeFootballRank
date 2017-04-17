﻿using System;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace CFBpredictor
{
    public partial class Ranker : Form
    {
        NCAA ncaa = new NCAA();
        List<Team> SOS = new List<Team>();
        List<Game> Week = new List<Game>();
        List<Team> DivisionI = new List<Team>();

        const int WEEKS_ADDED = 19; //Keeps track of how many weeks have been added to prevent duplicate score additions
        string scoresPath = "C:/users/Danny/CFBPredictor/scores/";
        string excelPath = "C:/Users/Danny/Documents/";
        enum Filter
        {
            NCAADivisionI,
            FBS,
            FCS,
            AAC,
            ACC,
            Big12,
            BigTen,
            CUSA,
            Independent,
            MAC,
            MWC,
            Pac12,
            SEC,
            SunBelt,
            ConferenceRank,
            SOS,
            OffenseRank,
            DefenseRank
        }

        Filter filter;
        const int NUM_RERANKS = 1000; //Number of times it iterates through with different SOS.
        int j = 0;
       
        public Ranker()
        {
            InitializeComponent();
            this.Text = "College Football Rankings";
            uxRankings.DoubleClick += new EventHandler(uxRankings_DoubleClick);
        }

        /// <summary>
        /// Adds proper list of rankings to textbox based on user selected filter.
        /// </summary>
        /// <param name="Rankings"></param>
        private void AddRankings(List<Team> Rankings)
        {
            //Adds the teams and ratings to listbox
            AddToConference();
            ConferenceRankings();
            StrengthOfSchedule();
            int i = 1;
            List<Conference> confRankings = ncaa.DivisionI.OrderBy(o => o.rating).ToList();
            int k = 1;
            foreach (Conference conference in confRankings)
            {
                conference.rank = i;
                k++;
            }

            k = 1;

            foreach (Team team in Rankings)
            {
                team.rating *= 100;
            
                try
                {
                    k = 1; //used to rank conferences

                    if (filter == Filter.NCAADivisionI)
                    {
                        uxRankings.Items.Add(i + ". " + team.getTeamName() + " " + team.rating);
                        i++;
                    }

                    else if (filter == Filter.FBS)
                    {
                        if (!team.getTeamConference().Equals("FCS"))
                        {
                            uxRankings.Items.Add(i + ". " + team.getTeamName() + " " + team.rating);
                            i++;
                        }
                    }

                    else if (filter == Filter.FCS)
                    {
                        if (team.getTeamConference().Equals("FCS"))
                        {
                            uxRankings.Items.Add(i + ". " + team.getTeamName() + " " + team.rating);
                            i++;
                        }
                    }

                    else if (filter == Filter.AAC)
                    {
                        if (team.getTeamConference().Equals("AAC"))
                        {
                            uxRankings.Items.Add(i + ".(" + team.getFBSRank() + ") " + team.getTeamName() + " " + team.rating);
                            i++;
                        }
                    }

                    else if (filter == Filter.ACC)
                    {
                        if (team.getTeamConference().Equals("ACC"))
                        {
                            uxRankings.Items.Add(i + ".(" + team.getFBSRank() + ") " + team.getTeamName() + " " + team.rating);
                            i++;
                        }
                    }

                    else if (filter == Filter.Big12)
                    {
                        if (team.getTeamConference().Equals("Big 12"))
                        {
                            uxRankings.Items.Add(i + ".(" + team.getFBSRank() + ") " + team.getTeamName() + " " + team.rating);
                            i++;
                        }
                    }

                    else if (filter == Filter.BigTen)
                    {
                        if (team.getTeamConference().Equals("Big Ten"))
                        {
                            uxRankings.Items.Add(i + ".(" + team.getFBSRank() + ") " + team.getTeamName() + " " + team.rating);
                            i++;
                        }
                    }

                    else if (filter == Filter.CUSA)
                    {
                        if (team.getTeamConference().Equals("CUSA"))
                        {
                            uxRankings.Items.Add(i + ".(" + team.getFBSRank() + ") " + team.getTeamName() + " " + team.rating);
                            i++;
                        }
                    }

                    else if (filter == Filter.Independent)
                    {
                        if (team.getTeamConference().Equals("Independent"))
                        {
                            uxRankings.Items.Add(i + ".(" + team.getFBSRank() + ") " + team.getTeamName() + " " + team.rating);
                            i++;
                        }
                    }

                    else if (filter == Filter.MAC)
                    {
                        if (team.getTeamConference().Equals("MAC"))
                        {
                            uxRankings.Items.Add(i + ".(" + team.getFBSRank() + ") " + team.getTeamName() + " " + team.rating);
                            i++;
                        }
                    }

                    else if (filter == Filter.MWC)
                    {
                        if (team.getTeamConference().Equals("MWC"))
                        {
                            uxRankings.Items.Add(i + ".(" + team.getFBSRank() + ") " + team.getTeamName() + " " + team.rating);
                            i++;
                        }
                    }

                    else if (filter == Filter.Pac12)
                    {
                        if (team.getTeamConference().Equals("Pac 12"))
                        {
                            uxRankings.Items.Add(i + ".(" + team.getFBSRank() + ") " + team.getTeamName() + " " + team.rating);
                            i++;
                        }
                    }

                    else if (filter == Filter.SEC)
                    {
                        if (team.getTeamConference().Equals("SEC"))
                        {
                            uxRankings.Items.Add(i + ".(" + team.getFBSRank() + ") " + team.getTeamName() + " " + team.rating);
                            i++;
                        }
                    }

                    else if (filter == Filter.SunBelt)
                    {
                        if (team.getTeamConference().Equals("Sun Belt"))
                        {
                            uxRankings.Items.Add(i + ".(" + team.getFBSRank() + ") " + team.getTeamName() + " " + team.rating);
                            i++;
                        }
                    }

                    else if (filter == Filter.ConferenceRank)
                    {
                        uxRankings.Items.Clear();
                        foreach (Conference conference in confRankings)
                        {
                            uxRankings.Items.Add(k + ". " + conference.conferenceName + " (" + conference.rating + ")");
                            k++;
                        }
                    }

                    else if (filter == Filter.SOS)
                    {
                        if (uxRankings.Items.Count == 0)
                        {
                            uxRankings.Items.Clear();
                            foreach (Team t in SOS)
                            {
                                uxRankings.Items.Add(k + ". " + t.getTeamName());
                                k++;
                            }
                        }
                    }

                    else if (filter == Filter.OffenseRank)
                    {
                        if (uxRankings.Items.Count == 0)
                        {
                            uxRankings.Items.Clear();
                            //List<Team> Offense = ncaa.FBS.OrderBy(o => o.PPGvsOppAvg).Reverse().ToList();
                            double low = 100;
                            foreach (Team t in ncaa.FBS)
                            {
                                if (t.getPPGvsOppAvg() < low)
                                    low = t.getPPGvsOppAvg();
                            }

                            foreach (Team t in ncaa.FBS)
                            {
                                t.setOppAdjustedPPG(low);
                            }

                            List<Team> Offense = ncaa.FBS.OrderBy(o => o.getOppAdjustedPPG()).Reverse().ToList();
                            foreach (Team t in Offense)
                            {
                                uxRankings.Items.Add(k + ". " + t.getTeamName() + " (" + t.getOppAdjustedPPG() + ")");
                                k++;
                            }
                        }
                    }

                    else if (filter == Filter.DefenseRank)
                    {
                        if (uxRankings.Items.Count == 0)
                        {
                            uxRankings.Items.Clear();
                            double high = -100;
                            foreach (Team t in ncaa.FBS)
                            {
                                if (t.getDefensePPGvsOppAvg() > high)
                                    high = t.getDefensePPGvsOppAvg();
                            }

                            foreach (Team t in ncaa.FBS)
                            {
                                t.setOppAdjustedDPPG(high);
                            }

                            List<Team> Defense = ncaa.FBS.OrderBy(o => o.getOppAdjustedDPPG()).Reverse().ToList();
                            foreach (Team t in Defense)
                            {
                                uxRankings.Items.Add(k + ". " + t.getTeamName() + " (" + t.getOppAdjustedDPPG() + ")");
                                k++;
                            }
                        }
                    }
                }
                
                catch
                {
                    ClearRankings();
                    return;
                }
            }
        }

        private void AddScorelines(string[] scoreline)
        {
            foreach (Team team in ncaa.FBS)
            {
                if (scoreline[0].Equals(team.getTeamName()))
                {
                    team.increaseTotalPoints(Convert.ToInt32(scoreline[1]));
                    team.increaseTotalPointsAllowed(Convert.ToInt32(scoreline[3]));
                    if (Convert.ToInt32(scoreline[1]) > Convert.ToInt32(scoreline[3]))
                        team.increaseWins();
                    else
                        team.increaseLosses();
                    foreach (Team opponent in ncaa.FBS)
                    {
                        if (scoreline[2].Equals(opponent.getTeamName()))
                            team.addOpponent(opponent);
                    }

                    foreach (Team opponent in ncaa.FCS)
                    {
                        if (scoreline[2].Equals(opponent.getTeamName()))
                            team.addOpponent(opponent);
                    }
                }

                else if (scoreline[2].Equals(team.getTeamName()))
                {
                    team.increaseTotalPoints(Convert.ToInt32(scoreline[3]));
                    team.increaseTotalPointsAllowed(Convert.ToInt32(scoreline[1]));
                    if (Convert.ToInt32(scoreline[3]) > Convert.ToInt32(scoreline[1]))
                        team.increaseWins();
                    else
                        team.increaseLosses();
                    foreach (Team opponent in ncaa.FBS)
                    {
                        if (scoreline[0].Equals(opponent.getTeamName()))
                            team.addOpponent(opponent);
                    }

                    foreach (Team opponent in ncaa.FCS)
                    {
                        if (scoreline[0].Equals(opponent.getTeamName()))
                            team.addOpponent(opponent);
                    }
                }
            }

            foreach (Team team in ncaa.FCS)
            {
                if (scoreline[0].Equals(team.getTeamName()))
                {
                    team.increaseTotalPoints(Convert.ToInt32(scoreline[1]));
                    team.increaseTotalPointsAllowed(Convert.ToInt32(scoreline[3]));
                    if (Convert.ToInt32(scoreline[1]) > Convert.ToInt32(scoreline[3]))
                        team.increaseWins();
                    else
                        team.increaseLosses();
                    foreach (Team opponent in ncaa.FBS)
                    {
                        if (scoreline[2].Equals(opponent.getTeamName()))
                            team.addOpponent(opponent);
                    }

                    foreach (Team opponent in ncaa.FCS)
                    {
                        if (scoreline[2].Equals(opponent.getTeamName()))
                            team.addOpponent(opponent);
                    }
                }

                else if (scoreline[2].Equals(team.getTeamName()))
                {
                    team.increaseTotalPoints(Convert.ToInt32(scoreline[3]));
                    team.increaseTotalPointsAllowed(Convert.ToInt32(scoreline[1]));
                    if (Convert.ToInt32(scoreline[3]) > Convert.ToInt32(scoreline[1]))
                        team.increaseWins();
                    else
                        team.increaseLosses();
                    foreach (Team opponent in ncaa.FBS)
                    {
                        if (scoreline[0].Equals(opponent.getTeamName()))
                            team.addOpponent(opponent);
                    }

                    foreach (Team opponent in ncaa.FCS)
                    {
                        if (scoreline[0].Equals(opponent.getTeamName()))
                            team.addOpponent(opponent);
                    }
                }
            }

        }

        public void AddToConference()
        {
            foreach (Conference conference in ncaa.DivisionI)
            {
                foreach (Team team in ncaa.FBS)
                {
                    if (team.getTeamConference().Equals(conference.conferenceName))
                        conference.members.Add(team);
                }

                if (conference.conferenceName == "FCS")
                {
                    foreach (Team team in ncaa.FCS)
                        conference.members.Add(team);
                }
            }
        }

        private bool CheckNames()
        {
            bool home = false;
            bool away = false;

            foreach (Team team in ncaa.FBS)
            {
                if (team.getTeamName().Equals(uxAwayTeam.Text))
                    away = true;
                else if (team.getTeamName().Equals(uxHomeTeam.Text))
                    home = true;
            }

            return (home && away);
        }

        public void ClearRankings()
        {
            foreach (Team team in ncaa.FBS)
            {
                team.Clear();
                team.ClearPoints();
            }

            foreach (Team team in ncaa.FCS)
            {
                team.Clear();
            }

            j = 0;

            foreach (Conference conference in ncaa.DivisionI)
            {
                conference.rating = 0;
                conference.numTeams = 0;
                conference.members.Clear();
            }

            uxRankings.Items.Clear();
        }

        public void ConferenceRankings()
        {
            foreach (Conference conference in ncaa.DivisionI)
            {
                if (conference.conferenceName == "FCS")
                    foreach (Team team in conference.members)
                    {
                        conference.rating += 129;
                    }
                else
                {
                    foreach (Team team in conference.members)
                    {
                        conference.rating += team.getFBSRank();
                    }
                }
                conference.rating /= conference.members.Count;
            }
        }

        private void DisplayScore(Team awayTeam, Team homeTeam)
        {
            awayTeam.setPoints(Math.Round(awayTeam.getPoints()));
            homeTeam.setPoints(Math.Round(homeTeam.getPoints()));
            MessageBox.Show(awayTeam.getTeamName() + ": " + awayTeam.getPoints() + "\n" + homeTeam.getTeamName() + ": " + homeTeam.getPoints());
            homeTeam.ClearPoints();
            awayTeam.ClearPoints();
        }

        private void ExportRankings()
        {
            StringBuilder text = new StringBuilder();
            string year = uxYear.SelectedItem.ToString();
            int i = 1;
            List<Team> FBSRankings = ncaa.FBS.OrderBy(o => o.rating).Reverse().ToList();
            List<Team> FCSRankings = ncaa.FCS.OrderBy(o => o.rating).Reverse().ToList();

            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Workbook xlWorkBook = xlApp.Workbooks.Add();
            Worksheet FBSRankSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
            Worksheet FCSRankSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(2);
            Worksheet SOSRankSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(3);
            FBSRankSheet.Columns[1].ColumnWidth = 5;
            FBSRankSheet.Columns[2].ColumnWidth = 20;
            FBSRankSheet.Cells[1, 2] = "Rankings (" + DateTime.Today.Date.Month + "/" + DateTime.Today.Date.Day + "/" + DateTime.Today.Date.Year + ")";
            int rank_index = 1;
            foreach (Team team in FBSRankings)
            {
                FBSRankSheet.Cells[rank_index+1, 1] = rank_index;
                FBSRankSheet.Cells[rank_index + 1, 2] = team.getTeamName();
                FBSRankSheet.Cells[rank_index+1, 3] = String.Format("{0:0.000}", team.rating);
                rank_index++;
            }

            FCSRankSheet.Columns[1].ColumnWidth = 5;
            FCSRankSheet.Columns[2].ColumnWidth = 20;
            FCSRankSheet.Cells[1, 2] = "Rankings (" + DateTime.Today.Date.Month + "/" + DateTime.Today.Date.Day + "/" + DateTime.Today.Date.Year + ")";
            rank_index = 1;
            foreach (Team team in FCSRankings)
            {
                FCSRankSheet.Cells[rank_index + 1, 1] = rank_index;
                FCSRankSheet.Cells[rank_index + 1, 2] = team.getTeamName();
                FCSRankSheet.Cells[rank_index + 1, 3] = String.Format("{0:0.000}", team.rating);
                rank_index++;
            }

            SOSRankSheet.Columns[1].ColumnWidth = 5;
            SOSRankSheet.Columns[2].ColumnWidth = 20;
            SOSRankSheet.Cells[1, 2] = "Strength of Schedule (" + DateTime.Today.Date.Month + "/" + DateTime.Today.Date.Day + "/" + DateTime.Today.Date.Year + ")";
            rank_index = 1;
            foreach (Team team in SOS)
            {
                SOSRankSheet.Cells[rank_index + 1, 1] = rank_index;
                SOSRankSheet.Cells[rank_index + 1, 2] = team.getTeamName();
                rank_index++;
            }

            xlApp.DisplayAlerts = false;
            FBSRankSheet.Name = "FBS Rankings";
            FCSRankSheet.Name = "FCS Rankings";
            SOSRankSheet.Name = "SOS Rank";
            xlWorkBook.SaveAs("Rankings.xlsx");
            xlWorkBook.Close();

            foreach (Team team in FBSRankings)
            {

                if (team.getFBSRank() < 100 && team.getTeamName().Length < 4)
                {
                    text.Append(i + ". " + team.getTeamName() + "\t\t\t\t" + team.rating + System.Environment.NewLine);
                    i++;
                }

                else if (team.getFBSRank() < 100 && team.getTeamName().Length < 12)
                {
                    text.Append(i + ". " + team.getTeamName() + "\t\t\t" + team.rating + System.Environment.NewLine);
                    i++;
                }

                else if (team.getFBSRank() < 100 && team.getTeamName().Length < 20)
                {
                    text.Append(i + ". " + team.getTeamName() + "\t\t" + team.rating + System.Environment.NewLine);
                    i++;
                }

                else if (team.getFBSRank() < 10 && team.getTeamName().Length < 13)
                {
                    text.Append(i + ". " + team.getTeamName() + "\t\t\t" + team.rating + System.Environment.NewLine);
                    i++;
                }

                else if (team.getFBSRank() < 100 && team.getTeamName().Length < 12)
                {
                    text.Append(i + ". " + team.getTeamName() + "\t\t\t" + team.rating + System.Environment.NewLine);
                    i++;
                }

                else if (team.getFBSRank() > 99 && team.getTeamName().Length < 11)
                {
                    text.Append(i + ". " + team.getTeamName() + "\t\t\t" + team.rating + System.Environment.NewLine);
                    i++;
                }

                else if (team.getFBSRank() > 99 && team.getTeamName().Length < 20)
                {
                    text.Append(i + ". " + team.getTeamName() + "\t\t" + team.rating + System.Environment.NewLine);
                    i++;
                }

                else
                {
                    text.Append(i + ". " + team.getTeamName() + "\t" + team.rating + System.Environment.NewLine);
                    i++;
                }
            }

            using (StreamWriter sw = new StreamWriter(scoresPath + "Rankings" + year + ".txt"))
            {
                sw.Write(text);
            }

            string mess = "KStAteFaN1993";
            mess = mess.ToLower();
            mess = mess.Remove(10, 3);
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("dmiller22kc@gmail.com");
            mail.To.Add("miller22@ksu.edu");
            mail.To.Add("ksufaninkc@gmail.com");
            mail.Subject = "Rankings";
            mail.Body = "This weeks CFB rankings";

            Attachment txtfile, xlfile;
            txtfile = new Attachment(scoresPath + "Rankings" + year + ".txt");
            xlfile = new Attachment(excelPath + "Rankings.xlsx");
            mail.Attachments.Add(txtfile);
            mail.Attachments.Add(xlfile);

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("dmiller22kc@gmail.com", mess),
                Timeout = 2000000,
            };
            smtp.Send(mail);
            MessageBox.Show("Export Complete");
        }

        private void FinalizeRankings(double low)
        {
            foreach (Team team in ncaa.FBS)
            {
                team.rating += (0 - (low - 1));
                if (team.getWinPercentage() > 0)
                {
                    team.rating *= Math.Pow(team.getWinPercentage(), 2);
                }
                else
                {
                    team.rating *= Math.Pow(.001, 2);
                }
                team.rating += 5;

                if (team.getOpponentWinPercentage() > 0)
                    team.rating *= team.getOpponentWinPercentage();
                else team.rating *= .001;
            }

            foreach (Team team in ncaa.FCS)
            {
                team.rating += (0 - (low - 1));
                if (team.getWinPercentage() > 0)
                {
                    team.rating *= Math.Pow(team.getWinPercentage(), 2);
                }
                else
                {
                    team.rating *= Math.Pow(.001, 2);
                }
                team.rating += 5;

                if (team.getOpponentWinPercentage() > 0)
                    team.rating *= team.getOpponentWinPercentage();
                else team.rating *= .001;
            }

            if (DivisionI.Count == 0)
            {
                foreach (Team team in ncaa.FBS)
                {
                    DivisionI.Add(team);
                }

                foreach (Team team in ncaa.FCS)
                {
                    DivisionI.Add(team);
                }
            }
            SortRankings(DivisionI, low);
        }

        private void GenerateRankings()
        {
            double low = 0;

            foreach (Team team in ncaa.FBS)
            {
                //Gives teams initial rating
                team.rating += (team.getPPGvsOppAvg() - team.getDefensePPGvsOppAvg()) * 5;
                if (team.rating < low)
                    low = team.rating;
            }

            foreach (Team team in ncaa.FCS)
            {
                //Gives teams initial rating
                team.rating += (team.getPPGvsOppAvg() - team.getDefensePPGvsOppAvg()) * (5/2);
                if (team.rating < low)
                    low = team.rating;
            }

            FinalizeRankings(low);
        }

        private void GetInfo(bool DoubleClick)
        {
            if (uxTeamName.Text != "" && !DoubleClick)
            {
                foreach (Team team in ncaa.FBS)
                {
                    if (team.getTeamName().Equals(uxTeamName.Text))
                    {
                        TeamForm tf = new TeamForm(team);
                        tf.Show();
                    }
                }

                foreach (Team team in ncaa.FCS)
                {
                    if (team.getTeamName().Equals(uxTeamName.Text))
                    {
                        TeamForm tf = new TeamForm(team);
                        tf.Show();
                    }
                }               
            }

            else if (uxRankings.SelectedItem != null)
            {
                if (!uxFilter.SelectedItem.Equals("Strength of Schedule"))
                {
                    StringBuilder teamstring = new StringBuilder();
                    string line = uxRankings.SelectedItem.ToString();
                    string[] n = line.Split(' ');
                    for (int i = 0; i < n.Length; i++)
                    {
                        if (Char.IsLetter(n[i][0]))
                        {
                            if (i == 1)
                                teamstring.Append(n[i]);
                            else
                                teamstring.Append(" " + n[i]);
                        }
                    }
                    string teamName = teamstring.ToString();

                    foreach (Team team in ncaa.FBS)
                    {
                        if (team.getTeamName().Equals(teamName))
                        {
                            TeamForm tf = new TeamForm(team);
                            tf.Show();
                        }
                    }

                    foreach (Team team in ncaa.FCS)
                    {
                        if (team.getTeamName().Equals(teamName))
                        {
                            TeamForm tf = new TeamForm(team);
                            tf.Show();
                        }
                    }
                }
            }
        }

        private void OpponentRecords()
        {
            //Calculates SOS for each team among other team stats
            foreach (Team team in ncaa.FBS)
            {
                foreach (Team opponent in team.getSchedule())
                {
                    team.increaseOpponentWins(opponent.getWins());
                    team.increaseOpponentLosses(opponent.getLosses());
                    team.increaseOpponentTotalPoints(Convert.ToInt32(opponent.getTotalPoints()));
                    team.increaseOpponentTotalPointsAllowed(Convert.ToInt32(opponent.getTotalPointsAllowed()));
                }
           }

            foreach (Team team in ncaa.FCS)
            {
                foreach (Team opponent in team.getSchedule())
                {
                    team.increaseOpponentWins(opponent.getWins());
                    team.increaseOpponentLosses(opponent.getLosses());
                    team.increaseOpponentTotalPoints(Convert.ToInt32(opponent.getTotalPoints()));
                    team.increaseOpponentTotalPointsAllowed(Convert.ToInt32(opponent.getTotalPointsAllowed()));
                }
            }
        }

        private bool ReadScores()
        {
            string[] scoreline;

            try
            {
                openFileDialog1.FileName = scoresPath + "collegefootballscores" + uxYear.SelectedItem.ToString() + ".txt";
            }

            catch
            {
                MessageBox.Show("Select a year to load.");
                return false;
            }
            string file = openFileDialog1.FileName;

            try
            {
                StreamReader sr = new StreamReader(file);
                while (!sr.EndOfStream)
                {
                    string score = sr.ReadLine();
                    scoreline = score.Split('-');
                    AddScorelines(scoreline);
                }

                sr.Close();
                return true;
            }


            catch (IOException)
            {
                MessageBox.Show("File does not exist.");
                return false;
            }
        }

        private void ReevaluateSchedule(List<Team> Rankings)
        {
            //Reevaluates SOS based on previous rankings generated
            foreach (Team team in Rankings)
            {
                foreach (Team name in team.getSchedule())
                {
                    team.setStrength(team.getStrength() + name.getRank());
                }
                team.rating += 5;
                team.rating /= (team.getStrength() / (team.getWins() + team.getLosses()));
            }
        }

        private void ReRank(List<Team> Rankings, double low)
        {
            foreach (Team team in ncaa.FBS)
            {
                team.rating += (team.getPPGvsOppAvg() - team.getDefensePPGvsOppAvg()) * 5;
                if (team.rating < low)
                {
                    low = team.rating;
                }
                
            }

            foreach (Team team in ncaa.FCS)
            {
                team.rating += (team.getPPGvsOppAvg() - team.getDefensePPGvsOppAvg()) * (5/2);
                if (team.rating < low)
                {
                    low = team.rating;
                }
            }

            foreach (Team team in ncaa.FBS)
            {
                team.rating += (0 - (low - 1));
                if (team.getWinPercentage() > 0)
                {
                    team.rating *= Math.Pow(team.getWinPercentage(), 2);
                }
                else
                {
                    team.rating *= Math.Pow(.0001, 2);
                }
            }

            foreach (Team team in ncaa.FCS)
            {
                team.rating += (0 - (low - 1));
                if (team.getWinPercentage() > 0)
                {
                    team.rating *= Math.Pow(team.getWinPercentage(), 2);
                }
                else
                {
                    team.rating *= Math.Pow(.0001, 2);
                }
            }

            ReevaluateSchedule(Rankings);
            j++;
            SortRankings(Rankings, low);
        }

        private void SortRankings(List<Team> Division1, double low)
        {
            //Sorts teams in order of rankings
            //List<Team> Rankings = Division1;
            List<Team> Rankings = Division1.OrderBy(o => o.rating).Reverse().ToList();
            int i = 1;
            foreach (Team team in Rankings)
            {
                team.setRank(i);
                i++;
            }

            if (j < NUM_RERANKS)
            {
                ReRank(Rankings, low);
            }

            else
            {
                foreach (Team team in Division1)
                {
                    if (team.getTeamConference() != "FCS")
                        team.rating *= 100;
                }
                List<Team> FBSRankings = ncaa.FBS.OrderBy(o => o.rating).Reverse().ToList();
                int k = 1;
                foreach (Team team in FBSRankings)
                {
                    team.setFBSRank(k);
                    k++;
                }
                foreach (Team team in ncaa.FCS)
                {
                    team.ClearFBSRank();
                }
                AddRankings(Rankings);
            }
        }

        public void StrengthOfSchedule()
        {
            foreach (Team t in ncaa.FBS)
            {
                if ((t.getWins() + t.getLosses()) != 0)
                    t.setStrength(t.getStrength() / (Convert.ToInt16(t.getWins() + t.getLosses())));

                else t.setStrength(int.MaxValue);
            }
            SOS = ncaa.FBS.OrderBy(o => o.getStrength()).ToList();
            int p = 1;
            foreach (Team team in SOS)
            {
                team.setSOSRank(p);
                p++;
            }
        }

        private void Predict(Team awayTeam, Team homeTeam)
        {
            awayTeam.setPoints(awayTeam.getPoints() + (awayTeam.getPPG() + homeTeam.getDefensePPGvsOppAvg()));
            awayTeam.setPoints(awayTeam.getPoints() + homeTeam.getDefensePPG() + awayTeam.getPPGvsOppAvg());
            awayTeam.setPoints(awayTeam.getPoints() / 2);
            homeTeam.setPoints(homeTeam.getPoints() + (homeTeam.getPPG() + awayTeam.getDefensePPGvsOppAvg()));
            homeTeam.setPoints(homeTeam.getPoints() + (awayTeam.getDefensePPG() + homeTeam.getPPGvsOppAvg()));
            homeTeam.setPoints(homeTeam.getPoints() / 2);
            DisplayScore(awayTeam, homeTeam);
        }

        private bool SetFilter()
        {
            try
            {
                if (uxFilter.SelectedItem.Equals("NCAA Division I"))
                    filter = Filter.NCAADivisionI;
                else if (uxFilter.SelectedItem.Equals("FBS"))
                    filter = Filter.FBS;
                else if (uxFilter.SelectedItem.Equals("FCS"))
                    filter = Filter.FCS;
                else if (uxFilter.SelectedItem.Equals("AAC"))
                    filter = Filter.AAC;
                else if (uxFilter.SelectedItem.Equals("ACC"))
                    filter = Filter.ACC;
                else if (uxFilter.SelectedItem.Equals("Big 12"))
                    filter = Filter.Big12;
                else if (uxFilter.SelectedItem.Equals("Big Ten"))
                    filter = Filter.BigTen;
                else if (uxFilter.SelectedItem.Equals("CUSA"))
                    filter = Filter.CUSA;
                else if (uxFilter.SelectedItem.Equals("Independent"))
                    filter = Filter.Independent;
                else if (uxFilter.SelectedItem.Equals("MAC"))
                    filter = Filter.MAC;
                else if (uxFilter.SelectedItem.Equals("MWC"))
                    filter = Filter.MWC;
                else if (uxFilter.SelectedItem.Equals("Pac 12"))
                    filter = Filter.Pac12;
                else if (uxFilter.SelectedItem.Equals("SEC"))
                    filter = Filter.SEC;
                else if (uxFilter.SelectedItem.Equals("Sun Belt"))
                    filter = Filter.SunBelt;
                else if (uxFilter.SelectedItem.Equals("Conference Rank"))
                    filter = Filter.ConferenceRank;
                else if (uxFilter.SelectedItem.Equals("Strength of Schedule"))
                    filter = Filter.SOS;
                else if (uxFilter.SelectedItem.Equals("Offense Rank"))
                    filter = Filter.OffenseRank;
                else if (uxFilter.SelectedItem.Equals("Defense Rank"))
                    filter = Filter.DefenseRank;

                return true;
            }

            catch
            {
                MessageBox.Show("Select a filter.");
                return false;
            }
        }

        private void SimGame(Team away, Team home)
        {
            //if (away.teamConference != "FCS" && home.teamConference != "FCS")
            //{
                away.setPoints(away.getPoints() + (away.getPPG() + home.getDefensePPGvsOppAvg()));
                away.setPoints(away.getPoints() + (home.getDefensePPG() + away.getPPGvsOppAvg()));
                away.setPoints(away.getPoints() / 2);
                home.setPoints(home.getPoints() + (home.getPPG() + away.getDefensePPGvsOppAvg()));
                home.setPoints(away.getPoints() + (away.getDefensePPG() + home.getPPGvsOppAvg()));
                home.setPoints(away.getPoints() / 2);

                Game g = new Game(home, away, home.getPoints(), away.getPoints());
                Week.Add(g);
            //}
        }

        private void WriteSimmedScores()
        {
            string fileName = scoresPath + "SimmedWeek.txt";
            string xlfileName = excelPath + "SimmedWeek.xlsx";
            StringBuilder scores = new StringBuilder();
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Workbook xlWorkBook = xlApp.Workbooks.Add();
            Worksheet xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Columns[1].ColumnWidth = 23;
            xlWorkSheet.Columns[2].ColumnWidth = 4;
            xlWorkSheet.Columns[3].ColumnWidth = 4;
            xlWorkSheet.Columns[4].ColumnWidth = 23;
            xlWorkSheet.Cells[1, 1] = "Week " + Convert.ToString(Convert.ToInt16(uxWeek.SelectedItem) - 1);

            List<Team> FBSRankings = ncaa.FBS.OrderBy(o => o.rating).Reverse().ToList();
            List<Team> FCSRankings = ncaa.FCS.OrderBy(o => o.rating).Reverse().ToList();
            int k = 1;
            foreach (Team team in FBSRankings)
            {
                team.setFBSRank(k);
                k++;
            }

            foreach (Team team in FCSRankings)
            {
                //TODO: Check this out.
                team.setFBSRank(k);
                k++;
            }

            int i = 2;
            foreach (Game g in Week)
            {
                scores.Append(g.awayTeam.getTeamName() + ": " + g.awayScore + "   ");
                scores.AppendLine(g.homeTeam.getTeamName() + ": " + g.homeScore);
                if (g.awayTeam.getFBSRank() <= 25)
                {
                    xlWorkSheet.Cells[i, 1] = "#" + g.awayTeam.getFBSRank() + " " + g.awayTeam.getTeamName();
                }
                else
                {
                    xlWorkSheet.Cells[i, 1] = g.awayTeam.getTeamName();
                }

                xlWorkSheet.Cells[i, 2] = g.awayScore;
                xlWorkSheet.Cells[i, 3] = g.homeScore;
                if (g.homeTeam.getFBSRank() <= 25)
                {
                    xlWorkSheet.Cells[i, 4] = "#" + g.homeTeam.getFBSRank() + " " + g.homeTeam.getTeamName();
                }
                else
                {
                    xlWorkSheet.Cells[i, 4] = g.homeTeam.getTeamName();
                }
                i++;
            }

            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.Write(scores.ToString());
            }

            xlApp.DisplayAlerts = false;
            xlWorkBook.SaveAs("SimmedWeek.xlsx");
            xlWorkBook.Close();

            System.Diagnostics.Process.Start(xlfileName);
            scores.Clear();
            Week.Clear();
            foreach (Team t in ncaa.FBS)
                t.ClearPoints();
        }

        private void uxClearRankings_Click(object sender, EventArgs e)
        {
            ClearRankings();
        }

        private void uxCompile_Click(object sender, EventArgs e)
        {
            if (SetFilter())
            {
                ClearRankings();

                if (ReadScores())
                {
                    OpponentRecords();
                    GenerateRankings();
                }
            }
        }

        private void uxExport_Click(object sender, EventArgs e)
        {
            if (uxRankings.Items.Count != 0)
                ExportRankings();

            else MessageBox.Show("No Rankings Listed to Export.");
        }

        private void uxGetSchedule_Click(object sender, EventArgs e)
        {
            if (uxTeamName.Text != "")
            {
                foreach (Team team in ncaa.FBS)
                {
                    if (team.getTeamName().Equals(uxTeamName.Text))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(team.getFBSRank() + ". " + team.getTeamName() + " (" + team.getWins() + "-" + team.getLosses() + ")  SOS: " + team.getSOSRank() + "\n_____________________\n\n");
                        for (int i = 0; i < team.getScheduleSize(); i++)
                        {
                            if (team.getOpponent(i).getFBSRank() != 0)
                                sb.Append(team.getOpponent(i).getFBSRank() + ". " + team.getOpponent(i).getTeamName() + " (" + team.getOpponent(i).getWins() + "-" + team.getOpponent(i).getLosses() + ")\n");
                            else sb.Append(team.getOpponent(i).getTeamName() + " (" + team.getOpponent(i).getWins() + "-" + team.getOpponent(i).getLosses() + ")\n");
                        }

                        MessageBox.Show(sb.ToString());
                        return;
                    }
                }

                foreach (Team team in ncaa.FCS)
                {
                    if (team.getTeamName().Equals(uxTeamName.Text))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(team.getFBSRank() + ". " + team.getTeamName() + " (" + team.getWins() + "-" + team.getLosses() + ")  SOS: " + team.getSOSRank() + "\n_____________________\n\n");
                        for (int i = 0; i < team.getScheduleSize(); i++)
                        {
                            sb.Append(team.getOpponent(i).getTeamName() + " (" + team.getOpponent(i).getWins() + "-" + team.getOpponent(i).getLosses() + ")\n");
                        }

                        MessageBox.Show(sb.ToString());
                        return;
                    }
                }
                
            }

            else if (uxRankings.SelectedItem != null)
            {
                StringBuilder teamstring = new StringBuilder();
                string line = uxRankings.SelectedItem.ToString();
                string[] n = line.Split(' ');
                for (int i = 0; i < n.Length; i++)
                {
                    if (Char.IsLetter(n[i][0]))
                    {
                        if (i == 1)
                            teamstring.Append(n[i]);
                        else
                            teamstring.Append(" " + n[i]);
                    }
                }
                string teamName = teamstring.ToString();

                foreach (Team team in ncaa.FBS)
                {
                    if (team.getTeamName().Equals(teamName))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(team.getFBSRank() + ". " + team.getTeamName() + " (" + team.getWins() + "-" + team.getLosses() + ")  SOS: " + team.getSOSRank() + "\n_____________________\n\n");
                        for (int i = 0; i < team.getScheduleSize(); i++)
                        {
                            if (team.getOpponent(i).getFBSRank() != 0)
                                sb.Append(team.getOpponent(i).getFBSRank() + ". " + team.getOpponent(i).getTeamName() + " (" + team.getOpponent(i).getWins() + "-" + team.getOpponent(i).getLosses() + ")\n");
                            else sb.Append(team.getOpponent(i).getTeamName() + " (" + team.getOpponent(i).getWins() + "-" + team.getOpponent(i).getLosses() + ")\n");
                        }

                        MessageBox.Show(sb.ToString());
                        return;
                    }
                }

                foreach (Team team in ncaa.FCS)
                {
                    if (team.getTeamName().Equals(teamName))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(team.getFBSRank() + ". " + team.getTeamName() + " (" + team.getWins() + "-" + team.getLosses() + ")  SOS: " + team.getSOSRank() + "\n_____________________\n\n");
                        for (int i = 0; i < team.getScheduleSize(); i++)
                        {
                            sb.Append(team.getOpponent(i).getTeamName() + " (" + team.getOpponent(i).getWins() + "-" + team.getOpponent(i).getLosses() + ")\n");
                        }

                        MessageBox.Show(sb.ToString());
                        return;
                    }
                }
            }
        }

        private void uxPredict_Click(object sender, EventArgs e)
        {
            Team homeTeam = null;
            Team awayTeam = null;
            bool home = false;
            bool away = false;

            foreach (Team team in ncaa.FBS)
            {
                if (team.getTeamName().Equals(uxAwayTeam.Text))
                    away = true;
                else if (team.getTeamName().Equals(uxHomeTeam.Text))
                    home = true;
            }

            if (away == true && home == true)
            {
                foreach (Team team in ncaa.FBS)
                {

                    if (uxAwayTeam.Text.Equals(team.getTeamName()))
                    {
                        awayTeam = team;
                    }

                    else if (uxHomeTeam.Text.Equals(team.getTeamName()))
                    {
                        homeTeam = team;
                    }
                }
                Predict(awayTeam, homeTeam);
            }
            else
            {
                MessageBox.Show("Team Name is incorrect");
            }
        }

        private void uxSimWeek_Click(object sender, EventArgs e)
        {
            string week;
            File.Delete(scoresPath + "SimmedWeek.txt");
            try
            {
                week = uxWeek.SelectedItem.ToString();
            }
            catch
            {
                MessageBox.Show("Select a week");
                return;
            }

            WebClient client = new WebClient();
            Team team1 = null, team2 = null;
            string ID, team1ID, team2ID;
            string fcsdstring = client.DownloadString("http://stats.washingtonpost.com/cfb/scoreboard.asp?conf=fcs%3A-2&week=" + week);
            string dstring = client.DownloadString("http://stats.washingtonpost.com/cfb/scoreboard.asp?conf=-1&week=" + week);
            string[] sep = new string[] { "team=" };
            string[] firstFCSSep = new string[] { "GAMEZONELINKSTART" };
            string[] parsed = dstring.Split(sep, StringSplitOptions.None);
            string[] fcsGames = fcsdstring.Split(firstFCSSep, StringSplitOptions.None);
            string[] teams = null;
            string[,] fcsTeams = new string[200,3];

            for (int i = 1; i < parsed.Length; i++)
            {
                int p = parsed[i].IndexOf(@">");
                ID = parsed[i].Substring(0, p);
                int index = 0;
                foreach (char c in ID)
                {
                    if (!Char.IsNumber(c))
                    {
                        ID = ID.Remove(index);
                    }
                    index++;
                }

                if (i % 2 != 0)
                {
                    foreach (Team fbsteam in ncaa.FBS)
                    {
                        if (fbsteam.getTeamID().Equals(ID))
                        {
                            team1 = fbsteam;
                        }
                    }

                    if (team1 == null)
                    {
                        foreach (Team fcsteam in ncaa.FCS)
                        {
                            if (fcsteam.getTeamID().Equals(ID))
                            {
                                team1 = fcsteam;
                            }

                        }
                    }

                }

                else
                {
                    foreach (Team team in ncaa.FBS)
                    {
                        if (team.getTeamID().Equals(ID))
                        {
                            team2 = team;
                            SimGame(team1, team2);
                            team1 = null;
                            team2 = null;
                        }
                    }
                }
            }


            //begin fcs
            try
            {
                for (int i = 0; i < fcsGames.Length; i++)
                {
                    teams = fcsGames[i].Split(sep, StringSplitOptions.None);
                    for (int j = 0; j < teams.Length; j++)
                    {
                        fcsTeams[i, j] = teams[j];
                    }
                }
            }

            catch
            {
                MessageBox.Show("Week has already finished or is in progress.");
                Week.Clear();
                return;
            }

            for (int i = 0; i < fcsGames.Length; i++)
            {
                if (fcsTeams[i, 2] != null)
                {
                    int p = fcsTeams[i, 1].IndexOf(@">");
                    team1ID = fcsTeams[i, 1].Substring(0, p);
                    int index = 0;
                    foreach (char c in team1ID)
                    {
                        if (!Char.IsNumber(c))
                        {
                            team1ID = team1ID.Remove(index);
                        }
                        index++;
                    }
                    p = fcsTeams[i, 2].IndexOf(@">");
                    team2ID = fcsTeams[i, 2].Substring(0, p);
                    index = 0;
                    foreach (char c in team2ID)
                    {
                        if (!Char.IsNumber(c))
                        {
                            team2ID = team2ID.Remove(index);
                        }
                        index++;
                    }

                    foreach (Team fcsTeam in ncaa.FCS)
                    {
                        if (fcsTeam.getTeamID().Equals(team1ID))
                        {
                            team1 = fcsTeam;
                        }

                        else if (fcsTeam.getTeamID().Equals(team2ID))
                        {
                            team2 = fcsTeam;
                        }
                    }
                    if (team1 != null && team2 != null)
                    {
                        SimGame(team1, team2);
                        team1 = null;
                        team2 = null;
                    }
                }
            }
            //end fcs code
            WriteSimmedScores();
        }

        private void uxSubmit_Click(object sender, EventArgs e)
        {
            string week;
            try
            {
                week = uxWeek.SelectedItem.ToString();
            }
            catch
            {
                MessageBox.Show("Select a week");
                return;
            }

            if (Convert.ToInt32(week) > WEEKS_ADDED)
            {
                WebClient client = new WebClient();
                Team team1 = null, team2 = null;
                string ID, team1ID, team2ID, team1Score = null, team2Score = null;
                string fcsdstring = client.DownloadString("http://stats.washingtonpost.com/cfb/scoreboard.asp?conf=fcs%3A-2&week=" + week);
                string dstring = client.DownloadString("http://stats.washingtonpost.com/cfb/scoreboard.asp?conf=-1&week=" + week);
                string[] sep = new string[] { "team=" };
                string[] sep2 = new string[] { "10%" };
                string[] ScoreString, ScoreString1, ScoreString2;
                string[] firstFCSSep = new string[] { "GAMEZONELINKSTART" };
                string[] parsed = dstring.Split(sep, StringSplitOptions.None);
                string[] fcsGames = fcsdstring.Split(firstFCSSep, StringSplitOptions.None);
                string[,] fcsTeams = new string[200, 3];
                string[] teams;
                List<string> games = new List<string>();

                StreamReader sr = new StreamReader(scoresPath + "collegefootballscores2016.txt");
                using (sr)
                {
                    while (!sr.EndOfStream)
                    {
                        games.Add(sr.ReadLine());
                    }
                }

                string ID1 = "", ID2 = "";

                for (int i = 1; i < parsed.Length; i++)
                {
                    int p = parsed[i].IndexOf(@">");
                    ID = parsed[i].Substring(0, p);
                    string checkID = ID;
                    checkID = checkID.Remove(checkID.Length - 1);
                    ScoreString = parsed[i].Split(sep2, StringSplitOptions.None);
                    string score;
                    if (checkID != ID1 && checkID != ID2)
                    {
                        try
                        {
                            score = ScoreString[ScoreString.Length - 1].Remove(0, 2);
                        }

                        catch
                        {
                            MessageBox.Show("Week has not concluded");
                            return;
                        }

                        if (Char.IsNumber(score[2]))
                        {
                            score = score.Remove(3, score.Length - 3);
                        }

                        else if (Char.IsNumber(score[1]))
                        {
                            score = score.Remove(2, score.Length - 2);
                        }

                        else
                        {
                            score = score.Remove(1, score.Length - 1);
                        }

                        int index = 0;
                        foreach (char c in ID)
                        {
                            if (!Char.IsNumber(c))
                            {
                                ID = ID.Remove(index);
                            }
                            index++;
                        }

                        if (team1 == null)
                        {
                            foreach (Team fbsteam in ncaa.FBS)
                            {
                                if (fbsteam.getTeamID().Equals(ID))
                                {
                                    ID1 = ID;
                                    team1 = fbsteam;
                                    team1Score = score;
                                }
                            }

                            if (team1 == null)
                            {
                                foreach (Team fcsteam in ncaa.FCS)
                                {
                                    if (fcsteam.getTeamID().Equals(ID))
                                    {
                                        ID1 = ID;
                                        team1 = fcsteam;
                                        team1Score = score;
                                    }

                                }
                            }

                        }

                        else
                        {
                            foreach (Team team in ncaa.FBS)
                            {
                                if (team.getTeamID().Equals(ID))
                                {
                                    ID2 = ID;
                                    team2 = team;
                                    team2Score = score;
                                    games.Add(team1.getTeamName() + "-" + team1Score + "-" + team2.getTeamName() + "-" + team2Score);
                                    team1 = null;
                                    team2 = null;
                                    //i += 6;
                                }
                            }
                        }
                    }
                }

                //begin fcs code
                if (week != "18" && week != "19")
                {
                    try
                    {
                        int ind = 0;
                        string checkID1 = "", checkID2 = "";
                        for (int i = 0; i < fcsGames.Length; i++)
                        {

                            teams = fcsGames[i].Split(sep, StringSplitOptions.None);
                            int p = teams[teams.Length - 2].IndexOf(@">");
                            team1ID = teams[teams.Length - 2].Substring(0, p);
                            int index = 0;
                            foreach (char c in team1ID)
                            {
                                if (!Char.IsNumber(c))
                                {
                                    team1ID = team1ID.Remove(index);
                                }
                                index++;
                            }
                            p = teams[teams.Length - 1].IndexOf(@">");
                            team2ID = teams[teams.Length - 1].Substring(0, p);
                            team2ID = team2ID.Remove(team2ID.Length - 1);
                            if (checkID1 != team1ID && checkID2 != team2ID)
                                if (checkID1 != team2ID && checkID2 != team1ID)
                                {
                                    fcsTeams[ind, 0] = teams[0];
                                    fcsTeams[ind, 1] = teams[teams.Length - 2];
                                    fcsTeams[ind, 2] = teams[teams.Length - 1];
                                    checkID1 = team1ID;
                                    checkID2 = team2ID;
                                    ind++;
                                }
                            //i += 6;
                        }
                    }

                    catch
                    {
                        MessageBox.Show("Week has not concluded yet.");
                        return;
                    }

                    for (int i = 0; i < fcsGames.Length; i++)
                    {
                        if (fcsTeams[i, 2] != null)
                        {
                            int p = fcsTeams[i, 1].IndexOf(@">");
                            team1ID = fcsTeams[i, 1].Substring(0, p);
                            int index = 0;
                            foreach (char c in team1ID)
                            {
                                if (!Char.IsNumber(c))
                                {
                                    team1ID = team1ID.Remove(index);
                                }
                                index++;
                            }
                            p = fcsTeams[i, 2].IndexOf(@">");
                            team2ID = fcsTeams[i, 2].Substring(0, p);
                            index = 0;
                            foreach (char c in team2ID)
                            {
                                if (!Char.IsNumber(c))
                                {
                                    team2ID = team2ID.Remove(index);
                                }
                                index++;
                            }

                            ScoreString1 = fcsTeams[i, 1].Split(sep2, StringSplitOptions.None);
                            ScoreString2 = fcsTeams[i, 2].Split(sep2, StringSplitOptions.None);
                            string score1 = ScoreString1[ScoreString1.Length - 1].Remove(0, 2);
                            string score2 = ScoreString2[ScoreString2.Length - 1].Remove(0, 2);

                            if (Char.IsNumber(score1[2]))
                            {
                                score1 = score1.Remove(3, score1.Length - 3);
                            }

                            else if (Char.IsNumber(score1[1]))
                            {
                                score1 = score1.Remove(2, score1.Length - 2);
                            }

                            else
                            {
                                score1 = score1.Remove(1, score1.Length - 1);
                            }

                            if (Char.IsNumber(score2[2]))
                            {
                                score2 = score2.Remove(3, score2.Length - 3);
                            }

                            else if (Char.IsNumber(score2[1]))
                            {
                                score2 = score2.Remove(2, score2.Length - 2);
                            }

                            else
                            {
                                score2 = score2.Remove(1, score2.Length - 1);
                            }

                            foreach (Team fcsTeam in ncaa.FCS)
                            {
                                if (fcsTeam.getTeamID().Equals(team1ID))
                                {
                                    team1 = fcsTeam;
                                }

                                else if (fcsTeam.getTeamID().Equals(team2ID))
                                {
                                    team2 = fcsTeam;
                                }
                            }
                            if (team1 != null && team2 != null)
                            {
                                games.Add(team1.getTeamName() + "-" + score1 + "-" + team2.getTeamName() + "-" + score2);
                                //SimGame(team1, team2);
                                team1 = null;
                                team2 = null;
                            }

                            else if (team1 == null || team2 == null)
                            {
                                team1 = null;
                                team2 = null;
                            }
                        }
                    }
                }
                //end fcs code

                //write to file here
                StreamWriter sw = new StreamWriter(scoresPath + "collegefootballscores2016.txt");
                using (sw)
                {
                    foreach (string s in games)
                    {
                        sw.WriteLine(s);
                    }
                }
                MessageBox.Show("Scores Added");
            }

            else MessageBox.Show("Week has already been added.");
        }

        private void uxGetInfo_Click(object sender, EventArgs e)
        {
            GetInfo(false);
        }

        private void uxYear_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void uxRankings_DoubleClick(object sender, EventArgs e)
        {
            GetInfo(true);
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void uxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
