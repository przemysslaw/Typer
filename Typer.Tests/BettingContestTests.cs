using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Typer.Tests
{
    [TestFixture]
    [Category("UnitTests")]
    public class BettingContestTests
    {
        private BettingContest Target { get; set; }
        private Mock<ITimeProvider> TimeProviderMock { get; set; }

        private BettingContest createBettingContest()
        {
            return new BettingContest(new DateTime(2015, 01, 01),"SampleName" );
        }

        private void CreateMocks()
        {
            TimeProviderMock = new Mock<ITimeProvider>();
            TimeProviderMock.Setup(mock => mock.GetUtcNow()).Returns(new DateTime(2014, 11, 29, 12, 00, 00));
        }

        [SetUp]
        public void SetUp()
        {
            CreateMocks();
            ApplicationTime.CurrentTimeProvider = TimeProviderMock.Object;
            Target = createBettingContest();
        }
        
        [Test]
        public void When_Constructor_is_called_with_a_start_date_in_the_past_Then_an_exception_is_thrown()
        {
            var timeProviderMock = new Mock<ITimeProvider>();
            timeProviderMock.Setup(mock => mock.GetUtcNow()).Returns(new DateTime(2014, 11, 29, 12, 00, 00));
            ApplicationTime.CurrentTimeProvider = timeProviderMock.Object;
            var sampleName = "SampleName";

            Assert.Throws<ArgumentException>(() => new BettingContest(new DateTime(2014, 11, 29, 11, 59, 00), sampleName));
        }

        [Test]
        public void When_Constructor_is_called_with_an_empty_string_as_name_Then_an_exception_is_thrown()
        {
            var startDate = DateTime.Now + new TimeSpan(1, 0, 0);

            Assert.Throws<ArgumentNullException>(() => new BettingContest(startDate, String.Empty));
        }

        [Test]
        public void When_Constructor_is_called_with_null_as_name_Then_an_exception_is_thrown()
        {
            var startDate = DateTime.Now + new TimeSpan(1, 0, 0);

            Assert.Throws<ArgumentNullException>(() => new BettingContest(startDate, null));
        }

        [Test]
        public void When_Constructor_is_called_with_only_whitespaces_as_name_Then_an_exception_is_thrown()
        {
            var startDate = DateTime.Now + new TimeSpan(1, 0, 0);

            Assert.Throws<ArgumentNullException>(() => new BettingContest(startDate, "       "));
        }

        [Test]
        public void When_Constructor_is_called_with_valid_parameters_Then_no_exception_is_thrown()
        {
            var timeProviderMock = new Mock<ITimeProvider>();
            timeProviderMock.Setup(mock => mock.GetUtcNow()).Returns(new DateTime(2014, 11, 29, 12, 00, 00));
            ApplicationTime.CurrentTimeProvider = timeProviderMock.Object;
            var sampleName = "SampleName";

            Assert.DoesNotThrow(() => new BettingContest(new DateTime(2014, 11, 29, 12, 01, 00), sampleName));
        }

        [Test]
        public void When_Constructor_is_called_with_valid_parameters_Then_StartDate_is_set_to_passed_value()
        {
            var timeProviderMock = new Mock<ITimeProvider>();
            timeProviderMock.Setup(mock => mock.GetUtcNow()).Returns(new DateTime(2014, 11, 29, 12, 00, 00));
            ApplicationTime.CurrentTimeProvider = timeProviderMock.Object;
            var sampleName = "SampleName";
            var startDate = new DateTime(2014, 11, 29, 12, 01, 00);

            var bettingContest = new BettingContest(new DateTime(2014, 11, 29, 12, 01, 00), sampleName);

            Assert.AreEqual(startDate, bettingContest.StartDate);
        }

        [Test]
        public void When_Constructor_is_called_with_valid_parameters_Then_Name_is_set_to_passed_value()
        {
            var timeProviderMock = new Mock<ITimeProvider>();
            timeProviderMock.Setup(mock => mock.GetUtcNow()).Returns(new DateTime(2014, 11, 29, 12, 00, 00));
            ApplicationTime.CurrentTimeProvider = timeProviderMock.Object;
            var sampleName = "SampleName";
            var startDate = new DateTime(2014, 11, 29, 12, 01, 00);

            var bettingContest = new BettingContest(new DateTime(2014, 11, 29, 12, 01, 00), sampleName);

            Assert.AreEqual(sampleName, bettingContest.Name);
        }

        [TestFixture]
        [Category("UnitTests")]
        public class AddBettingRound : BettingContestTests
        {
            [Test]
            public void When_null_is_passed_as_betting_round_Then_an_exception_is_thrown()
            {
                Assert.Throws<ArgumentNullException>(() => Target.AddBettingRound(null));
            }

            [Test]
            public void When_a_betting_round_is_passed_Then_the_passed_round_is_added_to_the_list_of_betting_rounds()
            {
                var bettingRoundName = "name";
                var newBettingRound = new BettingRound(bettingRoundName, new DateTime(2015, 07, 18), new DateTime(2015, 07, 19), 1, Target);

                Target.AddBettingRound(newBettingRound);

                Assert.IsNotNull(Target.BettingRounds.Single(item => item == newBettingRound));
            }

            [Test]
            public void
                When_a_betting_round_with_a_name_that_is_allready_present_in_the_list_of_betting_rounds_is_added_Then_an_Exception_is_thrown
                ()
            {
                var bettingRoundName = "name";
                var newBettingRound = new BettingRound(bettingRoundName, new DateTime(2015, 07, 18), new DateTime(2015, 07, 19), 1, Target);
                Target.AddBettingRound(newBettingRound);

                var duplicateBettingRound = new BettingRound(bettingRoundName, new DateTime(2015, 07, 18), new DateTime(2015, 07, 19), 1, Target);

                Assert.Throws<InvalidOperationException>(() => Target.AddBettingRound(duplicateBettingRound));
            }

            [Test]
            public void
                When_a_betting_round_with_a_name_taht_is_allready_present_in_the_list_of_betting_rounds_and_with_whitespaces_is_added_Then_an_Exception_is_thrown
                ()
            {
                var bettingRoundName = "name";
                var defaultRoundEndDate = Target.StartDate + new TimeSpan(0, 0, 1);
                var newBettingRound = new BettingRound(bettingRoundName, Target.StartDate, defaultRoundEndDate, 1, Target);
                Target.AddBettingRound(newBettingRound);

                var duplicateBettingRound = new BettingRound("  " + bettingRoundName + "  ", Target.StartDate, defaultRoundEndDate, 1, Target);

                Assert.Throws<InvalidOperationException>(() => Target.AddBettingRound(duplicateBettingRound));
            }

            [Test]
            public void
                When_a_betting_reound_with_an_earlier_default_end_date_then_contests_start_date_is_added_Then_an_exception_is_thrown
                ()
            {
                var bettingRoundName = "name";
                var newBettingRound = new BettingRound(bettingRoundName, Target.StartDate - new TimeSpan(12, 0, 0), Target.StartDate - new TimeSpan(0, 0, 1), 1, Target);
                Assert.Throws<InvalidOperationException>(() => Target.AddBettingRound(newBettingRound));
            }
        }
    }
}
