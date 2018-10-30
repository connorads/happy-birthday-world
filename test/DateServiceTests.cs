using System;
using HappyBirthdayWorld.Api.Services;
using Xunit;

namespace HappyBirthdayWorld.Tests
{
    public class DateServiceTests
    {
        [Fact]
        public void ReturnsToday()
        {
            Assert.Equal(DateTime.Today, new DateService().GetDateToday());
        }
    }
}
