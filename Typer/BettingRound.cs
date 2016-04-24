using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SDK;

namespace Typer
{
    public class BettingRound
    {
        private string _name;
        private int _defultNumberOfPointsForBet;
        private DateTime _defaultBettingEndDate;
        private DateTime _defaultBettingStartDate;
        private List<BetDefinition> _betsList;
        private readonly BettingContest _contest;

        public string Name
        {
            get { return _name; }
        }

        public int DefultNumberOfPointsForBet
        {
            get { return _defultNumberOfPointsForBet; }
        }

        public DateTime DefaultBettingStartDate
        {
            get { return _defaultBettingStartDate; }
        }

        public DateTime DefaultBettingEndDate
        {
            get { return _defaultBettingEndDate; }
        }

        public IReadOnlyList<BetDefinition> BetsList
        {
            get { return _betsList; }
        }

        public BettingContest Contest
        {
            get { return _contest; }
        }


        public BettingRound(string name, DateTime defaultBettingStartDate, DateTime defaultBettingEndDate, int defultNumberOfPointsForBet, BettingContest contest)
        {
            Require.NotNullNotEmpty(name, "name");
            Require.NotNull(contest, "contest");
            Require.Greater<ArgumentException>(defaultBettingEndDate, defaultBettingStartDate);
            Require.GreaterEqual<ArgumentException>(defultNumberOfPointsForBet, 1);

            _name = name;
            _defultNumberOfPointsForBet = defultNumberOfPointsForBet;
            _defaultBettingStartDate = defaultBettingStartDate;
            _defaultBettingEndDate = defaultBettingEndDate;
            _contest = contest;
            _betsList = new List<BetDefinition>();
        }

        public void AddBetDefinition(BetDefinition betDefinition)
        {
            Require.NotNull(betDefinition, "betDefinition");

            if (BettingDefinitionsCloseDateNotEarlierThanStartingDateOfTheContest(betDefinition))
            {

                _betsList.Add(betDefinition);
            }
            else
            {
                throw new ArgumentException("Can not add a bet definiition with close date earlier than the starting date of the contest!");
            }
        }

        private bool BettingDefinitionsCloseDateNotEarlierThanStartingDateOfTheContest(BetDefinition betDefinition)
        {
            return Contest.StartDate < betDefinition.CloseDate;
        }
    }
}