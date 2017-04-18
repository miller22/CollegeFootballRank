using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CFBPredictor_Test
{
    [TestFixture]
    public class Team_Test
    {
        CFBpredictor.Team sut;

        [SetUp]
        public void Before()
        {
            sut = new CFBpredictor.Team("testTeam", "testConference", "222");
        }

        /// <summary>
        /// Tests that the 2 argument constructor properly creates a Team
        /// </summary>
        [Test]
        public void TestTwoArgumentConstructor()
        {
            CFBpredictor.Team team = new CFBpredictor.Team("testName", "testConference");

            Assert.AreEqual("testName", team.GetTeamName());
            Assert.AreEqual("testConference", team.GetTeamConference());
        }

        /// <summary>
        /// Tests that the 3 argument constructor properly creates a Team
        /// </summary>
        [Test]
        public void TestThreeArgumentConstructor()
        {
            Assert.AreEqual("testTeam", sut.GetTeamName());
            Assert.AreEqual("testConference", sut.GetTeamConference());
            Assert.AreEqual("222", sut.GetTeamID());
        }

        /// <summary>
        /// Tests that GetWinPercentage() returns the proper win percentage
        /// when the team has games played
        /// </summary>
        [Test]
        public void TestGetWinPctWithGamesPlayed()
        {
            sut.IncreaseWins();
            sut.IncreaseLosses();

            Assert.AreEqual(0.500, sut.GetWinPercentage());
        }

        /// <summary>
        /// Tests that GetWinPercentage() returns 0 when the team has not
        /// played any games
        /// </summary>
        [Test]
        public void TestGetWinPctWithNoGamesPlayed()
        {
            Assert.AreEqual(0.000, sut.GetWinPercentage());
        }

        /// <summary>
        /// Tests that GetPPG() returns the proper value when the team has
        /// games played
        /// </summary>
        [Test]
        public void TestGetPPGWithGamesPlayed()
        {
            sut.IncreaseWins();
            sut.IncreaseWins();
            sut.IncreaseTotalPoints(84);

            Assert.AreEqual(42, sut.GetPPG());
        }

        /// <summary>
        /// Tests that GetPPG() returns 0 when the team has not played any
        /// games
        /// </summary>
        [Test]
        public void TestGetPPGWithNoGamesPlayed()
        {
            Assert.AreEqual(0, sut.GetPPG());
        }

        /// <summary>
        /// Tests that GetDefensePPG() returns the proper value when the team
        /// has played games
        /// </summary>
        [Test]
        public void TestGetDPPGWithGamesPlayed()
        {
            sut.IncreaseWins();
            sut.IncreaseWins();
            sut.IncreaseTotalPointsAllowed(35);
            Assert.AreEqual(17.5, sut.GetDefensePPG());
        }

        /// <summary>
        /// Tests that GetDefensePPG() returns 0 when the team has not
        /// played any games
        /// </summary>
        [Test]
        public void TestGetDPPGWithNoGamesPlayed()
        {
            Assert.AreEqual(0, sut.GetDefensePPG());
        }

        /// <summary>
        /// Tests that GetOpponentPPG() returns the proper value when the
        /// team's opponents have played games
        /// </summary>
        [Test]
        public void TestGetOpponentPPGWithGames()
        {
            sut.IncreaseOpponentWins(2);
            sut.IncreaseOpponentLosses(1);
            sut.IncreaseOpponentTotalPoints(33);

            Assert.AreEqual(11, sut.GetOpponentPPG());
        }

        /// <summary>
        /// Tests that GetOpponentPPG() returns 0 when the team's opponents
        /// have not played any games
        /// </summary>
        [Test]
        public void TestGetOpponentPPGWithNoGames()
        {
            Assert.AreEqual(0, sut.GetOpponentPPG());
        }

        /// <summary>
        /// Tests that GetOpponentDefensePPG() returns the proper value when
        /// the team's opponents have played games
        /// </summary>
        [Test]
        public void TestGetOpponentDPPGWithGames()
        {
            sut.IncreaseOpponentWins(2);
            sut.IncreaseOpponentLosses(1);
            sut.IncreaseOpponentTotalPointsAllowed(33);

            Assert.AreEqual(11, sut.GetOpponentDefensePPG());
        }

        /// <summary>
        /// Tests that GetOpponentDefensePPG() returns 0 when the team's opponents
        /// have not played any games
        /// </summary>
        [Test]
        public void TestGetOpponentDPPGWithNoGames()
        {
            Assert.AreEqual(0, sut.GetOpponentDefensePPG());
        }

        /// <summary>
        /// Tests that GetPPGvsOppAvg() returns the proper value
        /// </summary>
        [Test]
        public void TestGetPPGvsOppAvg()
        {
            sut.IncreaseTotalPoints(30);
            sut.IncreaseWins();
            sut.IncreaseOpponentTotalPointsAllowed(20);
            sut.IncreaseOpponentWins(1);

            Assert.AreEqual(10, sut.GetPPGvsOppAvg());
        }

        /// <summary>
        /// Tests that GetDefensePPGvsOppAvg() returns the proper value
        /// </summary>
        [Test]
        public void TestGetDPPGvsOppAvg()
        {
            sut.IncreaseTotalPointsAllowed(14);
            sut.IncreaseWins();
            sut.IncreaseOpponentTotalPoints(35);
            sut.IncreaseOpponentWins(1);

            Assert.AreEqual(-21, sut.GetDefensePPGvsOppAvg());
        }

        /// <summary>
        /// Tests that GetOpponentWinPercentage() returns the proper value when
        /// the opponents have games played
        /// </summary>
        [Test]
        public void TestGetOpponentWinPctWithGamesPlayed()
        {
            sut.IncreaseOpponentWins(3);
            sut.IncreaseOpponentLosses(1);

            Assert.AreEqual(.750, sut.GetOpponentWinPercentage());
        }

        /// <summary>
        /// Tests that GetOpponentWinPercentage() returns 0 when the opponents have
        /// no games played
        /// </summary>
        [Test]
        public void TestGetOpponentWinPctWithNoGamesPlayed()
        {
            Assert.AreEqual(0, sut.GetOpponentWinPercentage());
        }

        /// <summary>
        /// Tests that IncreaseWins() increases the team's win count by 1
        /// </summary>
        [Test]
        public void TestIncreaseWins()
        {
            sut.IncreaseWins();

            Assert.AreEqual(1, sut.GetWins());
        }

        /// <summary>
        /// Tests that IncreaseLosses() increases the team's loss count by 1
        /// </summary>
        [Test]
        public void TestIncreaseLosses()
        {
            sut.IncreaseLosses();

            Assert.AreEqual(1, sut.GetLosses());
        }

        /// <summary>
        /// Tests that IncreaseTotalPoints() increases the team's total points
        /// by the correct amount
        /// </summary>
        [Test]
        public void TestIncreaseTotalPoints()
        {
            sut.IncreaseTotalPoints(45);

            Assert.AreEqual(45, sut.GetTotalPoints());
        }

        /// <summary>
        /// Tests that IncreaseTotalPointsAllowed() increases the team's points
        /// allowed by the proper amount
        /// </summary>
        [Test]
        public void TestIncreaseTotalPointsAllowed()
        {
            sut.IncreaseTotalPointsAllowed(21);

            Assert.AreEqual(21, sut.GetTotalPointsAllowed());
        }

        /// <summary>
        /// Tests that IncreaseOpponentWins() increases the team's opponents' win
        /// count by the proper amount
        /// </summary>
        [Test]
        public void TestIncreaseOpponentWins()
        {
            sut.IncreaseOpponentWins(6);

            Assert.AreEqual(6, sut.GetOpponentWins());
        }

        /// <summary>
        /// Tests that IncreaseOpponentLosses() increases the team's opponents' loss
        /// count by the proper amount
        /// </summary>
        [Test]
        public void TestIncreaseOpponentLosses()
        {
            sut.IncreaseOpponentLosses(7);

            Assert.AreEqual(7, sut.GetOpponentLosses());
        }

        /// <summary>
        /// Tests that IncreaseOpponentTotalPoints() increases the team's opponents'
        /// total points scored by the proper amount
        /// </summary>
        [Test]
        public void TestIncreaseOpponentPoints()
        {
            sut.IncreaseOpponentTotalPoints(33);

            Assert.AreEqual(33, sut.GetOpponentTotalPoints());
        }

        /// <summary>
        /// Tests that IncreaseOpponentPointsAllowed() increases the team's opponents'
        /// total points allowed by the proper amount
        /// </summary>
        [Test]
        public void TestIncreaseOpponentPointsAllowed()
        {
            sut.IncreaseOpponentTotalPointsAllowed(22);

            Assert.AreEqual(22, sut.GetOpponentTotalPointsAllowed());
        }
    }
}