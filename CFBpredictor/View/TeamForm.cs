using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CFBpredictor
{
    public partial class TeamForm : Form
    {
        public TeamForm(Team t)
        {
            InitializeComponent();
            this.Text = t.getTeamName();
            Image logo = null;
            try
            {
                logo = Image.FromFile("C:/users/Danny/CFBpredictor/scores/Logos/" + t.getTeamName() + ".png");
            }

            catch { }
            TRecord.Text = "(" + t.getWins() + "-" + t.getLosses() + ")";
            TRating.Text = t.rating.ToString();
            TRank.Text = t.getFBSRank().ToString();
            Tppg.Text = t.getPPG().ToString();
            Tpapg.Text = t.getDefensePPG().ToString();
            Tppgvsavg.Text = t.getPPGvsOppAvg().ToString();
            Tpavsavg.Text = t.getDefensePPGvsOppAvg().ToString();
            Tsos.Text = t.getSOSRank().ToString();
            if (logo != null)
            {
                TeamLogo.Image = logo;
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
