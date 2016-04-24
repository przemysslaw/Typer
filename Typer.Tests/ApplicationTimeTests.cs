using System;
using Moq;
using NUnit.Framework;

namespace Typer.Tests
{
    [TestFixture]
    [Category("UnitTests")]
    public class ApplicationTimeTests
    {
        [Test]
        public void When_accessed_the_first_time_Then_default_logic_is_set()
        {
            var logic = ApplicationTime.CurrentTimeProvider;

            Assert.IsInstanceOf<DefaultTimeProvider>(logic);
        }

        [Test]
        public void When_ResetToDefault_is_called_Then_logic_is_set_to_default_logic()
        {
            ApplicationTime.CurrentTimeProvider = new Mock<ITimeProvider>().Object;
            ApplicationTime.ResetToDefault();
            var logic = ApplicationTime.CurrentTimeProvider;

            Assert.IsInstanceOf<DefaultTimeProvider>(logic);
        }

        [Test]
        public void When_null_is_passed_as_TimeProvider_Then_exception_is_thrown()
        {
            Assert.Throws<ArgumentNullException>(() => ApplicationTime.CurrentTimeProvider = null);
        }

        [Test]
        public void When_time_provider_is_set_to_custom_provider_Then_current_time_provider_is_set_to_passed_instance()
        {
            var timeProviderMock = new Mock<ITimeProvider>();
            ApplicationTime.CurrentTimeProvider = timeProviderMock.Object;

            Assert.AreSame(timeProviderMock.Object, ApplicationTime.CurrentTimeProvider);
        }

        [Test]
        public void When_GetUtcNow_is_called_for_the_first_time_Then_no_exception_is_thrown()
        {
            ApplicationTime.ClearLogic();

            Assert.DoesNotThrow(() => ApplicationTime.GetUtcNow());
        }

        [Test]
        public void When_GetUtcNow_is_called_Then_GetUtcNow_of_time_provider_is_called()
        {
            var timeProviderMock = new Mock<ITimeProvider>();
            ApplicationTime.CurrentTimeProvider = timeProviderMock.Object;

            ApplicationTime.GetUtcNow();

            timeProviderMock.Verify(mock => mock.GetUtcNow());
        }

        [Test]
        public void
            When_CurrentTimeProvider_is_accessed_after_ClearLogic_is_called_Then_time_provider_is_set_to_default_logic()
        {
            ApplicationTime.ResetToDefault();
            ApplicationTime.ClearLogic();

            Assert.IsInstanceOf<DefaultTimeProvider>(ApplicationTime.CurrentTimeProvider);
        }

    }
}
