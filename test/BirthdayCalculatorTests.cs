using System;
using HappyBirthdayWorld.Api.Domain;
using HappyBirthdayWorld.Api.Services;
using Moq;
using Xunit;

namespace HappyBirthdayWorld.Tests
{
    public class BirthdayCalculatorTests
    {
        private readonly BirthdayCalculator bc = new BirthdayCalculator(new DateService());

        [Fact]
        public void NextBirthdayIsToday()
        {
            Assert.Equal(0, bc.DaysUntilNextBirthday(DateTime.Parse("09/06/2019"), DateTime.Parse("09/06/2019")));
        }

        [Fact]
        public void NextBirthdayIsCurrentYear()
        {
            Assert.Equal(174, bc.DaysUntilNextBirthday(DateTime.Parse("23/06/2016"), DateTime.Parse("01/01/2040")));
        }

        [Fact]
        public void NextBirthdayIsFollowingYear()
        {
            Assert.Equal(304, bc.DaysUntilNextBirthday(DateTime.Parse("29/08/1997"), DateTime.Parse("29/10/2018")));
        }

        [Fact]
        public void NextBirthdayIsFollowingYear_BornOnLeapDay_NextYearIsLeapYear()
        {
            Assert.Equal(142, bc.DaysUntilNextBirthday(DateTime.Parse("29/02/1996"), DateTime.Parse("10/10/2019")));
        }

        [Fact]
        public void NextBirthdayIsFollowingYear_BornOnLeapDay_NextYearIsNotLeapYear()
        {
            int daysUntilFebTwentyEighth = 141;
            Assert.Equal(daysUntilFebTwentyEighth, bc.DaysUntilNextBirthday(DateTime.Parse("29/02/1996"), DateTime.Parse("10/10/2018")));
        }

        [Fact]
        public void IgnoreTimeOnDates()
        {
            Assert.Equal(0, bc.DaysUntilNextBirthday(DateTime.Parse("09/06/2019 11:10:13"), DateTime.Parse("09/06/2019 23:14:11")));
        }

        [Fact]
        public void ShorterMethod_CallsDateServiceOnce()
        {
            var mock = new Mock<IDateService>();
            var birthdayCalculator = new BirthdayCalculator(mock.Object);

            birthdayCalculator.DaysUntilNextBirthday(new DateTime());

            mock.Verify(ds => ds.GetDateToday(), Times.Once);
        }
    }
}
