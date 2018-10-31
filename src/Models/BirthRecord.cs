using System;
using System.ComponentModel.DataAnnotations;

namespace HappyBirthdayWorld.Api.Models
{
    public class BirthRecord
    {
        public BirthRecord(string name, DateTime dateOfBirth)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
        }

        [Key]
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
