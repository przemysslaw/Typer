using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using SDK;

namespace Typer
{
    public class BettingContest
    {
        private readonly DateTime _startDate;
        private readonly string _name;
        private List<BettingRound> _bettingRounds;

        public DateTime StartDate { get { return _startDate; } }
        public object Name { get { return _name; } }

        public IReadOnlyList<BettingRound> BettingRounds
        {
            get { return new ReadOnlyCollection<BettingRound>(_bettingRounds); }
        }

        public BettingContest(DateTime startingDate, String name)
        {
            if (startingDate < ApplicationTime.GetUtcNow())
            {
                throw new ArgumentException("A contest can not start in the past");
            }

            Require.NotNullNotEmpty(name, "name");

            _startDate = startingDate;
            _name = name;
            _bettingRounds = new List<BettingRound>();
        }

        public void AddBettingRound(BettingRound newBettingRound)
        {
            Require.NotNull(newBettingRound, "newBettingRound");
            Require.GreaterEqual<InvalidOperationException>(newBettingRound.DefaultBettingEndDate, StartDate);

            if (_bettingRounds.Any(round => String.Compare(round.Name.Trim(), newBettingRound.Name.Trim(), true, CultureInfo.InvariantCulture) == 0))
            {
                throw new InvalidOperationException("A betting round with the same name allready exists in this betting contest!");
            }

            _bettingRounds.Add(newBettingRound);
        }
    }
}
