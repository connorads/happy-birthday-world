using System;
using HappyBirthdayWorld.Api.Controllers;
using HappyBirthdayWorld.Api.Domain;
using HappyBirthdayWorld.Api.Dto;
using HappyBirthdayWorld.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HappyBirthdayWorld.Tests
{
    public class HelloControllerTests
    {
        private readonly DateOfBirth validDateOfBirth = new DateOfBirth{ dateOfBirth = DateTime.Parse("01/01/2000")};
        private readonly string validName = "Connor";

        [Fact]
        public void SuccessfulPutReturnsNoContent()
        {
            var helloController = new HelloController(Mock.Of<IBirthdayRepository>(), Mock.Of<IBirthdayCalculator>());

            var putResult = helloController.Put(validName, validDateOfBirth);

            Assert.IsType<NoContentResult>(putResult);
        }

        [Fact]
        public void NullStringPutReturnsBadRequestObject()
        {
            var helloController = new HelloController(Mock.Of<IBirthdayRepository>(), Mock.Of<IBirthdayCalculator>());

            var putResult = helloController.Put(null, validDateOfBirth);

            Assert.IsType<BadRequestObjectResult>(putResult);
        }

        [Fact]
        public void FutureDatePutReturnsBadRequestObject()
        {
            var helloController = new HelloController(Mock.Of<IBirthdayRepository>(), Mock.Of<IBirthdayCalculator>());
            var tomorrowsDate = new DateOfBirth{dateOfBirth = DateTime.Today.AddDays(1)};
            
            var putResult = helloController.Put(validName, tomorrowsDate);

            Assert.IsType<BadRequestObjectResult>(putResult);
        }

        [Fact]
        public void SuccessfulGet_BirthdayNotToday_ReturnsMessageAndOk()
        {
            var mockBirthdayRepository = new Mock<IBirthdayRepository>();
            DateTime dateOfBirth;
            mockBirthdayRepository.Setup(br => br.TryGetDateOfBirth(It.IsAny<string>(), out dateOfBirth))
                .Returns(true);
            var mockBirthdayCalculator = new Mock<IBirthdayCalculator>();
            mockBirthdayCalculator.Setup(bd => bd.DaysUntilNextBirthday(It.IsAny<DateTime>())).Returns(10);
            var helloController = new HelloController(mockBirthdayRepository.Object, mockBirthdayCalculator.Object);

            var getResult = helloController.Get("Connor").Result;
            
            var okObjectResult = Assert.IsType<OkObjectResult>(getResult);
            var actual = okObjectResult.Value.ToString();
            var expected = new {Message = "Hello, Connor! Your birthday is in 10 days"}.ToString();
            Assert.Equal(expected, actual);

        }


        [Fact]
        public void SuccessfulGet_BirthdayToday_ReturnsMessageAndOk()
        {
            var mockBirthdayRepository = new Mock<IBirthdayRepository>();
            DateTime dateOfBirth;
            mockBirthdayRepository.Setup(br => br.TryGetDateOfBirth(It.IsAny<string>(), out dateOfBirth))
                .Returns(true);
            var mockBirthdayCalculator = new Mock<IBirthdayCalculator>();
            mockBirthdayCalculator.Setup(bd => bd.DaysUntilNextBirthday(It.IsAny<DateTime>())).Returns(0);
            var helloController = new HelloController(mockBirthdayRepository.Object, mockBirthdayCalculator.Object);

            var getResult = helloController.Get("Connor").Result;
            
            var okObjectResult = Assert.IsType<OkObjectResult>(getResult);
            var actual = okObjectResult.Value.ToString();
            var expected = new {Message = "Hello, Connor! Happy Birthday!"}.ToString();
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void NullStringGetReturnsBadRequest()
        {
            var helloController = new HelloController(Mock.Of<IBirthdayRepository>(), Mock.Of<IBirthdayCalculator>());

            var getResult = helloController.Get(null).Result;

            Assert.IsType<BadRequestObjectResult>(getResult);
        }
    }
}
