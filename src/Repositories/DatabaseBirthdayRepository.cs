using System;
using System.Linq;
using HappyBirthdayWorld.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HappyBirthdayWorld.Api.Repositories
{
    public class DatabaseBirthdayRepository : IBirthdayRepository
    {
        private readonly ILogger<DatabaseBirthdayRepository> logger;

        private readonly BirthdayContext birthdayContext;

        public DatabaseBirthdayRepository(ILogger<DatabaseBirthdayRepository> logger, BirthdayContext birthdayContext)
        {
            this.logger = logger;
            this.birthdayContext = birthdayContext;
        }

        public bool TryGetDateOfBirth(string name, out DateTime dateOfBirth)
        {
            try
            {
                dateOfBirth = birthdayContext.BirthRecords.First(br => br.Name == name).DateOfBirth;
                return true;
            }
            catch (Exception e)
            {
                logger.LogInformation(e, "Failed to retrieve date of birth");
            }
            dateOfBirth = new DateTime();
            return false;
        }

        public void PutDateOfBirth(BirthRecord birthRecord)
        {
            var existingRecord = birthdayContext.Find<BirthRecord>(birthRecord.Name);

            if (existingRecord == null)
            {
                birthdayContext.Add(birthRecord);
            }
            else
            {
                if (existingRecord.DateOfBirth == birthRecord.DateOfBirth) return;
                existingRecord.DateOfBirth = birthRecord.DateOfBirth;
            }

            birthdayContext.SaveChanges();
        }
    }

    public class BirthdayContext : DbContext
    {
        public BirthdayContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BirthRecord> BirthRecords { get; set; }
    }
}