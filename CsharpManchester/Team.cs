using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpManchester
{
    public class Team
    {
        public  ICalculateMatch _matchMaker;

        public string Name { get; set; }
        public int Score { get; set; }
        public Team(){}
        public Team(ICalculateMatch matchMaker)
        {
            _matchMaker = matchMaker;
        }

        public bool ConvertAndCalculateMatch()
        {
            _matchMaker.ConvertToMatches();
            _matchMaker.CalculateScore();
            return true;
        }


    }

}
