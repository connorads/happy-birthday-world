using System;
using System.ComponentModel.DataAnnotations;


namespace HappyBirthdayWorld.Api.Models
{
    public class DateOfBirth
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime dateOfBirth { get; set; }

    }
}