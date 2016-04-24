using System;
using NUnit.Framework;

namespace Typer.Tests
{
    [TestFixture]
    [Category("UnitTests")]
    public class DefaultTimeProviderTests
    {
        [Test]
        public void When_GetUtcNow_is_called_Then_time_from_the_same_minute_as_current_time_is_returned()
        {
            var target = new DefaultTimeProvider();
            var currentTime = DateTime.UtcNow;

            var timeFromProvider = target.GetUtcNow();

            Assert.IsTrue(currentTime.Year == timeFromProvider.Year &&
                          currentTime.Month == timeFromProvider.Month &&
                          currentTime.Day == timeFromProvider.Day && 
                          currentTime.Hour == timeFromProvider.Hour &&
                          currentTime.Minute == timeFromProvider.Minute);
        }
    }
}
