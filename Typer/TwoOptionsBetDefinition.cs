using System;

namespace Typer
{
    public class TwoOptionsBetDefinition
    {
        private DateTime _closingDate;
        private string _optionLeft;
        private string _optionRight;
        private long _optionLeftHandicap;
        private long _optionRightHandicap;
        private decimal _numberOfPointsForCorrectPick;


        public TwoOptionsBetDefinition(string optionLeft, string optionRight, DateTime closingDate, decimal numberOfPointsForCorrectPick, long optionLeftHandicap = 0, long optionRightHandicap = 0)
        {
            
        }
    }
}
