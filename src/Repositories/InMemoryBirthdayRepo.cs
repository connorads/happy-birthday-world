using System;
using System.Collections.Generic;
using HappyBirthdayWorld.Api.Models;

namespace HappyBirthdayWorld.Api.Repositories
{
    public class InMemoryBirthdayRepo : IBirthdayRepository
    {
        private IDictionary<string, DateTime> birthdays = new Dictionary<string, DateTime>();

        public bool TryGetDateOfBirth(string name, out DateTime dateOfBirth)
        {
            if (name != null) return birthdays.TryGetValue(name, out dateOfBirth);
            dateOfBirth = new DateTime();
            return false;
        }

        public void PutDateOfBirth(BirthRecord birthRecord)
        {
            if (birthRecord.Name == null) throw new ArgumentNullException(nameof(birthRecord.Name));
            birthdays[birthRecord.Name] = birthRecord.DateOfBirth;
        }
    }
}