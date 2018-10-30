using System;

namespace HappyBirthdayWorld.Api.Repositories
{
    public interface IBirthdayRepository
    {
        bool TryGetDateOfBirth(string name, out DateTime dateOfBirth);

        void PutDateOfBirth(string name, DateTime dateOfBirth);
    }
}
