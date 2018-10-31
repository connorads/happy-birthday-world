using System;
using HappyBirthdayWorld.Api.Models;

namespace HappyBirthdayWorld.Api.Repositories
{

    public interface IBirthdayRepository
    {
        bool TryGetDateOfBirth(string name, out DateTime dateOfBirth);

        void PutDateOfBirth(BirthRecord birthRecord);
    }
}
