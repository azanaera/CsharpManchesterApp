using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpManchester
{
    public interface ICalculateMatch
    {
        public void ConvertToMatches();
        public void CalculateScore();
        public Output Output { get; set; }
        public string SelectedTeam { get; set; }
        public string Results { get; set; }
    }
}
