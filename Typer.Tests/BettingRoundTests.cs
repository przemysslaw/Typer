using System;
using System.Linq;
using Moq;
using NUnit.Framework;

namespace Typer.Tests
{
    [TestFixture]
    [Category("UnitTests")]
    public class BettingRoundTests
    {
        private DateTime ContestStartDate { get; set; }
        private DateTime RoundDefaultEndDate { get; set; }
        private DateTime RoundDefaultStartDate { get; set; }
        private int DefaultNumberOfPointsPerBet { get; set; }
        private BettingRound Target { get; set; }
        private Mock<ITimeProvider> TimeProviderMock { get; set; }

        private BettingContest CreateBettingContest()
        {
            return new BettingContest(ContestStartDate, "SampleName");
        }

        private BettingRound CreateBettingRound()
        {
            return new BettingRound("SampleName", RoundDefaultStartDate, RoundDefaultEndDate, DefaultNumberOfPointsPerBet, CreateBettingContest());
        }

        private void SetTestDates()
        {
            ContestStartDate = new DateTime(2015, 01, 01);
            RoundDefaultEndDate = new DateTime(2015, 01, 02);
            DefaultNumberOfPointsPerBet = 1;
        }

        private void CreateMocks()
        {
            TimeProviderMock = new Mock<ITimeProvider>();
            TimeProviderMock.Setup(mock => mock.GetUtcNow()).Returns(ContestStartDate - new TimeSpan(1, 0, 0));
        }

        [SetUp]
        public void SetUp()
        {
            SetTestDates();
            CreateMocks();
            ApplicationTime.CurrentTimeProvider = TimeProviderMock.Object;
            Target = CreateBettingRound();
        }

        private class BetDefinitionStub : BetDefinition
        {
            public BetDefinitionStub(DateTime closeDate)
                : base(closeDate)
            {
            }
        }

        [TestFixture]
        [Category("UnitTests")]
        public class Constructor : BettingRoundTests
        {
            private string _sampleName;
            private DateTime _sampleDefaultStartDate;
            private DateTime _sampleDefaultEndDate;
            private int _sampleNumberOfPoints;
            private BettingContest _sampleBettingContest;

            [SetUp]
            public void SetUp()
            {
                _sampleName = "Betting round 1";
                _sampleDefaultStartDate = new DateTime(2015, 07, 10);
                _sampleDefaultEndDate = new DateTime(2015, 07, 19);
                _sampleNumberOfPoints = 1;
                _sampleBettingContest = new BettingContest(new DateTime(2015, 07, 02), "sample contest");
            }

            [Test]
            public void When_a_name_is_passed_as_parameter_Then_the_name_is_set_to_passed_value()
            {
                var bettingRound = new BettingRound(_sampleName, _sampleDefaultStartDate, _sampleDefaultEndDate, _sampleNumberOfPoints, _sampleBettingContest);

                Assert.AreEqual(_sampleName, bettingRound.Name);
            }

            [Test]
            public void When_a_default_start_bet_date_is_passed_as_parameter_Then_the_DefaultDate_is_set_to_passed_value()
            {
                var bettingRound = new BettingRound(_sampleName, _sampleDefaultStartDate, _sampleDefaultEndDate, _sampleNumberOfPoints, _sampleBettingContest);

                Assert.AreEqual(_sampleDefaultStartDate, bettingRound.DefaultBettingStartDate);
            }

            [Test]
            public void When_a_default_bet_end_date_is_passed_as_parameter_Then_the_DefaultBettingEndDate_is_set_to_passed_value()
            {
                var bettingRound = new BettingRound(_sampleName, _sampleDefaultStartDate, _sampleDefaultEndDate, _sampleNumberOfPoints, _sampleBettingContest);

                Assert.AreEqual(_sampleDefaultEndDate, bettingRound.DefaultBettingEndDate);
            }

            [Test]
            public void When_a_default_number_of_points_per_bet_is_passed_as_parameter_Then_the_DefultNumberOfPointsForBet_is_set_to_passed_value()
            {
                var bettingRound = new BettingRound(_sampleName, _sampleDefaultStartDate, _sampleDefaultEndDate, _sampleNumberOfPoints, _sampleBettingContest);

                Assert.AreEqual(_sampleNumberOfPoints, bettingRound.DefultNumberOfPointsForBet);
            }

            [Test]
            public void When_a_contest_is_passed_as_parameter_Then_the_Contest_is_set_to_passed_value()
            {
                var bettingRound = new BettingRound(_sampleName, _sampleDefaultStartDate, _sampleDefaultEndDate, _sampleNumberOfPoints, _sampleBettingContest);

                Assert.AreEqual(_sampleBettingContest, bettingRound.Contest);
            }

            [Test]
            public void When_null_is_passed_as_name_Then_an_exception_is_thrown()
            {
                var sampleBettingContest = new BettingContest(new DateTime(2015, 07, 02), "sample contest");

                Assert.Throws<ArgumentNullException>(() => new BettingRound(null, _sampleDefaultStartDate, _sampleDefaultEndDate, _sampleNumberOfPoints, sampleBettingContest));
            }

            [Test]
            public void When_zero_is_passed_as_defult_numer_of_points_for_bet_Then_an_exception_is_thrown()
            {
                var sampleBettingContest = new BettingContest(new DateTime(2015, 07, 02), "sample contest");

                Assert.Throws<ArgumentException>(() => new BettingRound("sample round", _sampleDefaultStartDate, _sampleDefaultEndDate, 0, sampleBettingContest));
            }

            [Test]
            public void When_the_default_bet_end_date_is_smaller_then_default_start_bet_date_Then_an_exception_is_thrown()
            {
                var sampleBettingContest = new BettingContest(new DateTime(2015, 07, 02), "sample contest");

                Assert.Throws<ArgumentException>(() => new BettingRound("sample round", _sampleDefaultEndDate, _sampleDefaultStartDate, _sampleNumberOfPoints, sampleBettingContest));
            }

            [Test]
            public void When_null_is_passed_as_bettingContest_Then_an_exception_is_thrown()
            {
                Assert.Throws<ArgumentNullException>(() => new BettingRound("sample round", _sampleDefaultStartDate, _sampleDefaultEndDate, _sampleNumberOfPoints, null));
            }
        }

        [TestFixture]
        [Category("UnitTests")]
        public class AddBetDefinition : BettingRoundTests
        {
            [Test]
            public void
                When_null_is_passed_as_bet_definition_Then_an_exception_is_thrown
                ()
            {
                //Act & Assert
                Assert.Throws<ArgumentNullException>(() => Target.AddBetDefinition(null));
            }

            [Test]
            public void
                When_a_bet_definition_with_close_date_earlier_than_the_starting_date_of_the_contest_is_added_Then_an_exception_is_thrown
                ()
            {
                //Arrange
                var sampleBetDefinition = new BetDefinitionStub(ContestStartDate - new TimeSpan(0, 0, 0, 1));

                //Act & Assert
                Assert.Throws<ArgumentException>(() => Target.AddBetDefinition(sampleBetDefinition));
            }

            [Test]
            public void
                When_a_bet_definition_with_close_date_later_than_the_starting_date_of_the_contest_is_added_Then_the_definition_is_added_to_the_list_of_bets
                ()
            {
                //Arrange
                var sampleBetDefinition = new BetDefinitionStub(ContestStartDate + new TimeSpan(0, 0, 0, 1));

                //Act
                Target.AddBetDefinition(sampleBetDefinition);

                //Assert
                Assert.IsNotNull(Target.BetsList.Single(bet => bet == sampleBetDefinition));
            }

            //[Test]
            //public void When_a_bet_definition_with_
        }
    }
}
