using System;
using HappyBirthdayWorld.Api.Domain;
using Xunit;

namespace HappyBirthdayWorld.Tests
{
    public class BirthdayCalculatorTests
    {
        private readonly IBirthdayCalculator bc = new BirthdayCalculator();

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
            // TODO Question for product owner, are we okay to assume a Feb 28th Birthday? https://bit.ly/2ENDhFe
            int daysUntilFebTwentyEighth = 141;
            Assert.Equal(daysUntilFebTwentyEighth, bc.DaysUntilNextBirthday(DateTime.Parse("29/02/1996"), DateTime.Parse("10/10/2018")));
        }
    }
}
