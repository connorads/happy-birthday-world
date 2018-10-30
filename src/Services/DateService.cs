using System;

namespace HappyBirthdayWorld.Api.Services
{
    public class DateService : IDateService
    {
        public DateTime GetDateToday() => DateTime.Today;
    }
}