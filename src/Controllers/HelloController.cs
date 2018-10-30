using System;
using System.ComponentModel.DataAnnotations;
using HappyBirthdayWorld.Api.Domain;
using HappyBirthdayWorld.Api.Models;
using HappyBirthdayWorld.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HappyBirthdayWorld.Api.Controllers
{
    [Route("/hello")]
    public class HelloController : Controller
    {
        private readonly IBirthdayRepository birthdayRepository;
        private readonly IBirthdayCalculator birthdayCalculator;

        public HelloController(
            IBirthdayRepository birthdayRepository,
            IBirthdayCalculator birthdayCalculator)
        {
            this.birthdayRepository = birthdayRepository;
            this.birthdayCalculator = birthdayCalculator;
        }

        [HttpGet("{name}")]
        public ActionResult<string> Get(string name)
        {
            if (name == null) return BadRequest();

            name = name.Trim();
            var foundDob = birthdayRepository.TryGetDateOfBirth(name, out var dateOfBirth);
            if (!foundDob) return NotFound();

            var daysUntilNextBirthday =
                birthdayCalculator.DaysUntilNextBirthday(dateOfBirth);

            var countdownMessage = daysUntilNextBirthday == 0
                ? "Happy Birthday!"
                : $"Your birthday is in {daysUntilNextBirthday} days";

            return Ok(new {Message = $"Hello, {name}! {countdownMessage}"});

        }

        [HttpPut("{name}")]
        public ActionResult Put(
            [FromRoute, Required] string name,
            [FromBody, Required] DateOfBirth dateOfBirth)
        {
            if (!ModelState.IsValid || name == null || DobIsInFuture(dateOfBirth)) return BadRequest(ModelState);
            birthdayRepository.PutDateOfBirth(name.Trim(), dateOfBirth.dateOfBirth.Date);
            return NoContent();
        }

        private static bool DobIsInFuture(DateOfBirth dateOfBirth)
        {
            return !(dateOfBirth.dateOfBirth.Date <= DateTime.Today);
        }
    }


}
