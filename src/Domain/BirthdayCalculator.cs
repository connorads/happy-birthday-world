using System;
using HappyBirthdayWorld.Api.Services;

namespace HappyBirthdayWorld.Api.Domain
{
    public class BirthdayCalculator : IBirthdayCalculator
    {
        private readonly IDateService dateService;

        public BirthdayCalculator(IDateService dateService)
        {
            this.dateService = dateService;
        }

        public int DaysUntilNextBirthday(DateTime dateOfBirth)
        {
            return DaysUntilNextBirthday(dateOfBirth, dateService.GetDateToday());
        }

        public int DaysUntilNextBirthday(DateTime dateOfBirth, DateTime fromDate)
        {
            RemoveTimeFromDates(ref dateOfBirth, ref fromDate);
            var nextBirthdayDate = CalculateNextBirthdayDate(dateOfBirth, fromDate);
            var daysUntilNextBirthday = (nextBirthdayDate - fromDate).Days;
            return daysUntilNextBirthday;
        }

        private static DateTime CalculateNextBirthdayDate(DateTime dateOfBirth, DateTime fromDate)
        {
            var nextBirthday = dateOfBirth.AddYears(fromDate.Year - dateOfBirth.Year);
            if (nextBirthday < fromDate)
            {
                nextBirthday = !DateTime.IsLeapYear(nextBirthday.Year + 1)
                    ? nextBirthday.AddYears(1)
                    : new DateTime(nextBirthday.Year + 1, dateOfBirth.Month, dateOfBirth.Day);
            }

            return nextBirthday;
        }

        private static void RemoveTimeFromDates(ref DateTime dateOfBirth, ref DateTime fromDate)
        {
            dateOfBirth = dateOfBirth.Date;
            fromDate = fromDate.Date;
        }
    }
}
