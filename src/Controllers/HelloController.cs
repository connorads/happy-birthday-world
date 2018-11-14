using System;
using System.ComponentModel.DataAnnotations;
using HappyBirthdayWorld.Api.Domain;
using HappyBirthdayWorld.Api.Dto;
using HappyBirthdayWorld.Api.Models;
using HappyBirthdayWorld.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HappyBirthdayWorld.Api.Controllers
{
    [Route("/hello")]
    public class HelloController : Controller
    {
        private const int MaxNameLength = 50;
        private readonly IBirthdayRepository birthdayRepository;
        private readonly IBirthdayCalculator birthdayCalculator;

        public HelloController(
            IBirthdayRepository birthdayRepository,
            IBirthdayCalculator birthdayCalculator)
        {
            this.birthdayRepository = birthdayRepository;
            this.birthdayCalculator = birthdayCalculator;
        }

        /// <summary>
        /// Return birthday related message
        /// </summary>
        /// <param name="name">e.g. "Connor" (Must be 50 characters or less)</param>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpGet("{name}")]
        public ActionResult<string> Get([MaxLength(MaxNameLength)] string name)
        {
            if (!ModelState.IsValid || name == null) return BadRequest(ModelState);

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

        /// <summary>
        /// Save/update name and date of birth
        /// </summary>
        /// <param name="name">e.g. "Connor" (Must be 50 characters or less)</param>
        /// <param name="dateOfBirth">e.g. "1997-08-04" (YYYY-MM-DD)</param>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPut("{name}")]
        public ActionResult Put(
            [FromRoute, Required, MaxLength(MaxNameLength)] string name,
            [FromBody, Required] DateOfBirth dateOfBirth)
        {
            if (!ModelState.IsValid || name == null || DobIsInFuture(dateOfBirth)) return BadRequest(ModelState);
            birthdayRepository.PutDateOfBirth(new BirthRecord(name.Trim(), dateOfBirth.dateOfBirth.Date));
            return NoContent();
        }

        private static bool DobIsInFuture(DateOfBirth dateOfBirth)
        {
            return !(dateOfBirth.dateOfBirth.Date <= DateTime.Today);
        }
    }


}
