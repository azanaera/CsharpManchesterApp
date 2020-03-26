using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using CsharpManchester;
using Moq;

namespace CsharpManchester.Tests
{
    public class ManChesterUnitedShould
    {
        string results = "Manchester United 1 Chelsea 0,Arsenal 1 Manchester United 1,Manchester United 3 Fulham 1,Liverpool 2 Manchester United 1,Swansea 2 Manchester United 4";

        string selectedTeam = "Manchester United";

        [Fact]
        public void TestWorking()
        {
            var calculateMatch = new CalculateMatch(results,selectedTeam);
            calculateMatch.ConvertToMatches();
            calculateMatch.CalculateScore();
            Assert.Equal(3,     calculateMatch.Output.TotalWin); // win 
            Assert.Equal(1,     calculateMatch.Output.TotalDraw); // draw
            Assert.Equal(1,     calculateMatch.Output.TotalDefeat); // defeat
            Assert.Equal(10,    calculateMatch.Output.TotalGoal); // goal scored
            Assert.Equal(6,     calculateMatch.Output.TotalConcede); // concede
            Assert.Equal(10,    calculateMatch.Output.TotalPoints); // points
        }

        [Fact] // TestMethod
        public void HaveTheCorrectPoints()
        {
            var mockMatch = new Mock<ICalculateMatch>();
            mockMatch.Setup(x => x.Results).Returns(results);
            mockMatch.Setup(x => x.SelectedTeam).Returns(It.IsAny<string>());
            mockMatch.Setup(x => x.ConvertToMatches());
            mockMatch.Setup(x => x.CalculateScore());

            var team = new Team(mockMatch.Object);
            bool isCorrect = team.ConvertAndCalculateMatch();

            Assert.True(isCorrect);
            //mockMatch.Verify(x => x.Output.TotalConcede,"Should not be null");
        }

            //Assert
            //Assert.Equal(3, team._matchMaker.Output.TotalWin); // win 
            //Assert.Equal(1, team._matchMaker.Output.TotalDraw); // draw
            //Assert.Equal(1, team._matchMaker.Output.TotalDefeat); // defeat
            //Assert.Equal(10, team._matchMaker.Output.TotalGoal); // goal scored
            //Assert.Equal(6, team._matchMaker.Output.TotalConcede); // concede
            //Assert.Equal(10, team._matchMaker.Output.TotalPoints); // points
        
        [Fact]
        public void testMock()
        {
            var mockMatch = new Mock<ICalculateMatch>();
            mockMatch.Setup(x => x.Results).Returns(results);
            mockMatch.Setup(x => x.SelectedTeam).Returns(selectedTeam);

            var evaluate = new MatchEvaluator(mockMatch.Object);
            evaluate.methodToTest();

            Assert.Equal(3, evaluate._calculateMatch.Output.TotalWin);
        }
    }
}
