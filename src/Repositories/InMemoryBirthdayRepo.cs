using System;
using System.Collections.Generic;

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

        public void PutDateOfBirth(string name, DateTime dateOfBirth)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            birthdays[name] = dateOfBirth;
        }
    }
}