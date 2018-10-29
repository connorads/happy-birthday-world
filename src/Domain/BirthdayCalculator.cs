using System;

namespace HappyBirthdayWorld.Api.Domain
{
    public class BirthdayCalculator : IBirthdayCalculator
    {
        public int DaysUntilNextBirthday(DateTime dateOfBirth, DateTime fromDate)
        {
            // TODO Question for product owner, what do we do if the dateOfBirth is after the fromDate? (i.e. Today)
            var nextBirthday = dateOfBirth.AddYears(fromDate.Year - dateOfBirth.Year);
            if (nextBirthday < fromDate)
            {
                nextBirthday = !DateTime.IsLeapYear(nextBirthday.Year + 1)
                    ? nextBirthday.AddYears(1)
                    : new DateTime(nextBirthday.Year + 1, dateOfBirth.Month, dateOfBirth.Day);
            }
                
            return (nextBirthday - fromDate).Days;
        }
    }
}
