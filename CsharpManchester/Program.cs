using CsharpManchester;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpManchester
{
    public class Program
    {
        static void Main(string[] args)
        {
            string results = "Manchester United 1 Chelsea 0,Arsenal 1 Manchester United 1,Manchester United 3 Fulham 1,Liverpool 2 Manchester United 1,Swansea 2 Manchester United 4";
            var matches = results.Split(",");
            string selectedTeam = "Manchester United";

            var calculateMatch = new CalculateMatch(results, selectedTeam);
            calculateMatch.ConvertToMatches();
            calculateMatch.CalculateScore();

            Console.WriteLine($"number of wins = {calculateMatch.Output.TotalWin}");
            Console.WriteLine($"number of draws = {calculateMatch.Output.TotalDraw}");
            Console.WriteLine($"number of defeats = {calculateMatch.Output.TotalDefeat}");
            Console.WriteLine($"goals scored = {calculateMatch.Output.TotalGoal}");
            Console.WriteLine($"goals conceded = {calculateMatch.Output.TotalConcede}");
            Console.WriteLine($"number of points = {calculateMatch.Output.TotalPoints}");


        }//end for matches 

        public static Output CalculateScore(string[] matches,string selectedTeam)
        {
            var output = new Output();
            foreach (var match in matches.ToMatches())
            {
                //team 1
                if (match.Item1.Name == selectedTeam)
                {
                    output.TotalGoal += match.Item1.Score;
                    Console.WriteLine(match.Item1.Score);
                    Console.WriteLine(output.TotalGoal);
                }

                // team 2
                if (match.Item2.Name == selectedTeam)
                {
                    output.TotalGoal += match.Item2.Score;
                }

                //draw
                if (match.Item1.Name == selectedTeam || match.Item2.Name == selectedTeam)
                {
                    if (match.Item1.Score == match.Item2.Score)
                    {
                        output.TotalDraw += 1;
                        output.TotalPoints += 1;
                    }
                }
                //end draw

                // team 1 win
                if (match.Item1.Name == selectedTeam)
                {
                    if (match.Item1.Score > match.Item2.Score)
                    {
                        output.TotalWin += 1;
                        output.TotalPoints += 3;
                    }
                }

                // team 2 win
                if (match.Item2.Name == selectedTeam)
                {
                    if (match.Item2.Score > match.Item1.Score)
                    {
                        output.TotalWin += 1;
                    }
                }
                //end win

                // team 1 defeat
                if (match.Item1.Name == selectedTeam)
                {
                    if (match.Item1.Score < match.Item2.Score)
                    {
                        output.TotalDefeat += 1;
                    }
                }

                // team 2 defeat
                if (match.Item2.Name == selectedTeam)
                {
                    if (match.Item2.Score < match.Item1.Score)
                    {
                        output.TotalDefeat += 1;
                        output.TotalPoints += 3;
                    }
                }
                //end defeat

                //concede team 1 
                if (match.Item1.Name != selectedTeam)
                {
                    output.TotalConcede += match.Item1.Score;
                }

                //concede team 2 
                if (match.Item2.Name != selectedTeam)
                {
                    output.TotalConcede += match.Item2.Score;
                }
            }
            return output;
        }
    }

    public static class MatchExtensions
    {
        public static IEnumerable<Tuple<Team,Team>> ToMatches(this IEnumerable<string> source)
        {
            foreach (var item in source)
            {
                // index of first team - name + score
                var teamOneIndexEnd = item.IndexOfAny("0123456789".ToCharArray());

                // index of first team - name + score
                var teamTwoIndexEnd = item.Substring(teamOneIndexEnd + 1).IndexOfAny("0123456789".ToCharArray());


                var team1 = item.Substring(0, teamOneIndexEnd).Trim(); // Manchester United
                var team1Score = (int)Char.GetNumericValue(item[teamOneIndexEnd]); // score1

                var team2 = item.Substring(teamOneIndexEnd + 1, teamTwoIndexEnd).Trim();
                var team2Score = (int)Char.GetNumericValue(item.Substring(teamOneIndexEnd + 1, teamTwoIndexEnd + 1)[teamTwoIndexEnd]);
                yield return Tuple.Create(
                    new Team { Name = team1, Score = team1Score },
                    new Team { Name = team2, Score = team2Score });
            }
        }
    }
}
    

