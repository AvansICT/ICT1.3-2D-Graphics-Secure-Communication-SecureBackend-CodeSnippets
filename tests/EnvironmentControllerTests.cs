/* 
    This test class contains an example method on how to
    test controllers. If you use different result types or other
    code structures you need to modify this code to your needs
*/


using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProjectMap.WebApi.Controllers;
using ProjectMap.WebApi.Models;
using ProjectMap.WebApi.Repositories;
using System.Collections.Generic;

namespace ProjectMap.Tests
{
    [TestClass]
    public sealed class EnvironmentsControllerTests
    {
        /// <summary>
        /// Example test for a controller returning an ActionResult<T>
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Add_AddEnvinromentToUserWithNoEnviroments_ReturnsCreatedEnvironment()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var newEnvironment = GenerateRandomEnvironment("new environment");

            var environmentRepository = new Mock<IEnvironmentRepository>();
            var objectRepository = new Mock<IObjectRepository>();
            var userRepository = new Mock<IUserRepository>();

            var existingUserEnvironments = GenerateRandomEnvironments(0);

            userRepository.Setup(x => x.GetCurrentUserId()).Returns(userId);

            // When these methods need to be simulated since they're called
            // from the unit that we're testing (which is environmentController.Add)
            environmentRepository.Setup(x => x.ReadByOwnerUserId(userId)).ReturnsAsync(existingUserEnvironments);
            
            // My repository returns an object, so we need to set this up as well
            environmentRepository.Setup(x => x.InsertAsync(newEnvironment)).ReturnsAsync(newEnvironment);
    
            var environmentController = new EnvironmentsController(environmentRepository.Object, objectRepository.Object, userRepository.Object);

            // Act
            var response = await environmentController.AddAsync(newEnvironment);

            // The return result type of the controller method is Task<ActionResult<Environment2D>>
            // to test the outcome op the controller methode we need to dissect
            // this object. It contains a property .Value and a property .Result.

            // First we assert if the ActionResult is an OkObjectResult and
            // out the specific type of this generic type so we can use it later on

            // This code looks like the int.TryParse(input, out int value) used in 1.1
            Assert.IsInstanceOfType(response.Result, out OkObjectResult okObjectResult);

            // The OkObjectResult contains a value that is returned as the object to the client
            Assert.IsInstanceOfType(okObjectResult.Value, out Environment2D actualEnvironment);

            // We can now assert if the environment meets certain conditions
            Assert.IsTrue(actualEnvironment.Id == newEnvironment.Id);
        }

        // My test class contains these private methods to provide functionality needed by all tests
        private List<Environment2D> GenerateRandomEnvironments(int numberOfEnvironments)
        {
            List<Environment2D> list = [];

            for (int i = 0; i < numberOfEnvironments; i++)
            {
                list.Add(GenerateRandomEnvironment($"Random Environment {i}"));
            }

            return list;
        }

        private Environment2D GenerateRandomEnvironment(string name)
        {
            var random = new Random();
            return new Environment2D
            {
                Id = Guid.NewGuid(),
                MaxHeight = random.Next(1, 100),
                MaxLength = random.Next(1, 100),
                Name = name ?? "Random environment",
                OwnerUserId = Guid.NewGuid().ToString()
            };
        }
    }
}