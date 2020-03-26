using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpManchester
{
    public class CalculateMatch : ICalculateMatch
    {
        public List<Tuple<Team, Team>> _matches;
        public Output _output;
        public string selectedTeam;
        public string _results;

        public CalculateMatch(string results, string teamSelect)
        {
            _matches = new List<Tuple<Team, Team>>();
            _output = new Output();
            _results = results;
            selectedTeam = teamSelect;
        }

        public Output Output { get; set; }
        public string SelectedTeam { get; set; }
        public string Results { get; set ; }

        public void CalculateScore()
        {
            foreach (var match in _matches)
            {
                //Console.WriteLine($"{match.Item1.Name} : {match.Item1.Score} - {match.Item2.Name} : {match.Item2.Score}");
                //team 1
                if (match.Item1.Name == selectedTeam)
                {
                    _output.TotalGoal += match.Item1.Score;
                }

                // team 2
                if (match.Item2.Name == selectedTeam)
                {
                    _output.TotalGoal += match.Item2.Score;
                }

                //draw
                if (match.Item1.Name == selectedTeam || match.Item2.Name == selectedTeam)
                {
                    if (match.Item1.Score == match.Item2.Score)
                    {
                        _output.TotalDraw += 1;
                        _output.TotalPoints += 1;
                    }
                }
                //end draw

                // team 1 win
                if (match.Item1.Name == selectedTeam)
                {
                    if (match.Item1.Score > match.Item2.Score)
                    {
                        _output.TotalWin += 1;
                        _output.TotalPoints += 3;
                    }
                }

                // team 2 win
                if (match.Item2.Name == selectedTeam)
                {
                    if (match.Item2.Score > match.Item1.Score)
                    {
                        _output.TotalWin += 1;
                    }
                }
                //end win

                // team 1 defeat
                if (match.Item1.Name == selectedTeam)
                {
                    if (match.Item1.Score < match.Item2.Score)
                    {
                        _output.TotalDefeat += 1;
                    }
                }

                // team 2 defeat
                if (match.Item2.Name == selectedTeam)
                {
                    if (match.Item2.Score < match.Item1.Score)
                    {
                        _output.TotalDefeat += 1;
                        _output.TotalPoints += 3;
                    }
                }
                //end defeat

                //concede team 1 
                if (match.Item1.Name != selectedTeam)
                {
                    _output.TotalConcede += match.Item1.Score;
                }

                //concede team 2 
                if (match.Item2.Name != selectedTeam)
                {
                    _output.TotalConcede += match.Item2.Score;
                }
            }
        }

        public void ConvertToMatches()
        {
            List<Tuple<Team, Team>> matches = new List<Tuple<Team, Team>>();
            foreach (var item in _results.Split(","))
            {
                //Console.WriteLine(item);


                // index of first team - name + score
                var teamOneIndexEnd = item.IndexOfAny("0123456789".ToCharArray());

                // index of first team - name + score
                var teamTwoIndexEnd = 
                    item.Substring(teamOneIndexEnd + 1).IndexOfAny("0123456789".ToCharArray());


                var team1 = item.Substring(0, teamOneIndexEnd).Trim(); // Manchester United
                var team1Score = 
                    (int)Char.GetNumericValue(item[teamOneIndexEnd]); // score1

                var team2 = item.Substring(teamOneIndexEnd + 1, teamTwoIndexEnd).Trim();
                var team2Score = 
                    (int)Char.GetNumericValue(item.Substring(teamOneIndexEnd + 1, teamTwoIndexEnd + 1)[teamTwoIndexEnd]);
                
                _matches.Add(
                    Tuple.Create(
                    new Team { Name = team1, Score = team1Score },
                    new Team { Name = team2, Score = team2Score }));
            }
        }
    }
}
