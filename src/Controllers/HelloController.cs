using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace HappyBirthdayWorld.Api.Controllers
{
    [Route("/hello")]
    public class HelloController : Controller
    {
        private IDictionary<string, DateTime> birthdays;

        public HelloController(IDictionary<string, DateTime> birthdays)
        {
            this.birthdays = birthdays;
        }

        [HttpGet("{name}")]
        public ActionResult<string> Get(string name)
        {
            var foundDob = birthdays.TryGetValue(name, out var dob);
            if (foundDob)
            {
                return Ok(new BirthdayMessage {Message = 
                    $"Happy Birthday {name} - {dob}"
                });
            }

            return NotFound();
        }

        [HttpPut("{name}")]
        public ActionResult Put(
            [FromRoute] [Required] string name,
            [FromBody] [Required] DateOfBirth dateOfBirth)
        {
            if (ModelState.IsValid)
            {
                birthdays[name] = dateOfBirth.dateOfBirth;
                return NoContent();
            }
            
            return BadRequest(ModelState);
        } 
    }

    public class DateOfBirth
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime dateOfBirth { get; set; }
    }

    public class BirthdayMessage
    {
        public string Message { get; set; }
    }
}
