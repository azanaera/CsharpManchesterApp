using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsharpManchester
{
    public class CalculateMatch : IOutput
    {
        private readonly string[] matches;
        private readonly List<Team> teams = new List<Team>();
        private readonly string _results;

        public CalculateMatch(string results)
        {
            _results = results ?? throw new ArgumentNullException(results);
            matches = results.Split(",");
            RegisterTeams();
            CalculateScore();
        }
        //002 Calculate Score of each Team
        private void CalculateScore()
        {
            for (int i = 0; i < matches.Length; i++) // 1st match
            {
                Team homeTeam = null, awayTeam = null;
                int homeScore = 0, awayScore = 0;

                for (int j = 0; j < teams.Count; j++)
                {
                    if (matches[i].Contains(teams[j].Name,StringComparison.InvariantCulture))
                    {
                        int nameIndex = matches[i].IndexOf(teams[j].Name,StringComparison.InvariantCulture);
                        int scoreIndex = nameIndex + teams[j].Name.Length;
                        int score = 0;

                        if (nameIndex > 0)
                        {
                            awayTeam = teams[j];
                            bool IsTeam1score = int.TryParse(matches[i].Substring(scoreIndex), out score);

                            awayScore = score;
                        }
                        else
                        {
                            homeTeam = teams[j];
                            String partString = matches[i].Substring(scoreIndex).Trim();
                            bool isTeam2score = int.TryParse(partString.Substring(0, partString.IndexOf(' ',StringComparison.InvariantCulture)), out score);
                            homeScore = score;
                        }
                    }
                }

                awayTeam.GamesPlayed++;
                homeTeam.GamesPlayed++;

                homeTeam.GoalsScored += homeScore;
                awayTeam.GoalsScored += awayScore;

                homeTeam.GoalsConceded += awayScore;
                awayTeam.GoalsConceded += homeScore;

                if (homeScore > awayScore)
                {
                    homeTeam.Wins++;
                    awayTeam.Losses++;
                }
                else if (awayScore > homeScore)
                {
                    awayTeam.Wins++;
                    homeTeam.Losses++;
                }
                else
                {
                    homeTeam.Draws++;
                    awayTeam.Draws++;
                }
            }
        }
        //001 Convert to List of Teams
        private List<Team> RegisterTeams()
        {
            var matches = _results.Split(",");

            for (int i = 0; i < matches.Length; i++)
            {
                // index of first team - name + score
                var teamOneIndexEnd = matches[i].IndexOfAny("0123456789".ToCharArray());
                // index of 2nd team - name + score
                var teamTwoIndexEnd =
                    matches[i].Substring(teamOneIndexEnd + 1).IndexOfAny("0123456789".ToCharArray());
                //team name
                var team1 = matches[i].Substring(0, teamOneIndexEnd).Trim(); // Manchester United
                var team2 = matches[i].Substring(teamOneIndexEnd + 1, teamTwoIndexEnd).Trim();
                if (!teams.Where(t => t.Name == team1).Any())
                {
                    teams.Add(new Team { Name = team1 });
                }
                if(!teams.Where(t => t.Name == team2).Any())
                {
                    teams.Add(new Team { Name = team2 });
                }
            }
            return teams;
        }

        public Team GetResults(string selectedName)
        {
            return teams.Where(t => t.Name == selectedName).FirstOrDefault();
        }
    } // end class
}
