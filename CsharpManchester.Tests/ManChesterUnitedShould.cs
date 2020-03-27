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
        private string[] matches;
        private List<Team> teams = new List<Team>();
        [Fact]
        public void HaveCorrectPoints()
        {
            // Act
            var calculateMatch = new CalculateMatch(results);
            Team selectedTeam = calculateMatch.GetResults("Manchester United");
            // Assert
            Assert.Equal(3, selectedTeam.Wins); // win 
            Assert.Equal(1, selectedTeam.Draws); // draw
            Assert.Equal(1, selectedTeam.Losses); // defeat
            Assert.Equal(10, selectedTeam.GoalsScored); // goal scored
            Assert.Equal(6, selectedTeam.GoalsConceded); // concede
            Assert.Equal(10, selectedTeam.GetPoints()); // points
        }
    }
}
