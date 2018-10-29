using System;

namespace HappyBirthdayWorld.Api.Domain
{
    public interface IBirthdayCalculator
    {
        int DaysUntilNextBirthday(DateTime dateOfBirth, DateTime fromDate);
    }
}