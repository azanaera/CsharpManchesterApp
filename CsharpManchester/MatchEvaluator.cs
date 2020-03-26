using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpManchester
{
    public class MatchEvaluator
    {
        public ICalculateMatch _calculateMatch;
        public MatchEvaluator(ICalculateMatch calculateMatch)
        {
            _calculateMatch = calculateMatch;
        }

        public void methodToTest()
        {
            _calculateMatch.ConvertToMatches();
            _calculateMatch.CalculateScore();
        }

    }
}
