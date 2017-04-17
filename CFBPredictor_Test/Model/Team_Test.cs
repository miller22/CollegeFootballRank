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

        [Test]
        public void TestTwoArgumentConstructor()
        {
            CFBpredictor.Team team = new CFBpredictor.Team("testName", "testConference");

            Assert.AreEqual("testName", team.getTeamName());
            Assert.AreEqual("testConference", team.getTeamConference());
        }

        [Test]
        public void TestThreeArgumentConstructor()
        {
            Assert.AreEqual("testTeam", sut.getTeamName());
            Assert.AreEqual("testConference", sut.getTeamConference());
            Assert.AreEqual("222", sut.getTeamID());
        }

        [Test]
        public void TestGetWinPctWithGamesPlayed()
        {
            sut.increaseWins();
            sut.increaseLosses();

            Assert.AreEqual(0.500, sut.getWinPercentage());
        }

        [Test]
        public void TestGetWinPctWithNoGamesPlayed()
        {
            Assert.AreEqual(0.000, sut.getWinPercentage());
        }

        [Test]
        public void TestGetPPGWithGamesPlayed()
        {
            sut.increaseWins();
            sut.increaseWins();
            sut.increaseTotalPoints(84);

            Assert.AreEqual(42, sut.getPPG());
        }

        [Test]
        public void TestGetPPGWithNoGamesPlayed()
        {
            Assert.AreEqual(0, sut.getPPG());
        }

        [Test]
        public void TestGetDPPGWithGamesPlayed()
        {
            sut.increaseWins();
            sut.increaseWins();
            sut.increaseTotalPointsAllowed(35);
            Assert.AreEqual(17.5, sut.getDefensePPG());
        }

        [Test]
        public void TestGetDPPGWithNoGamesPlayed()
        {
            Assert.AreEqual(0, sut.getDefensePPG());
        }
    }
}