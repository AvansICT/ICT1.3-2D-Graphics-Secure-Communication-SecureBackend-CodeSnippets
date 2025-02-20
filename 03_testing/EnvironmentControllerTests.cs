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
            // ARRANGE

            // We create some fake data
            var userId = Guid.NewGuid().ToString();
            var newEnvironment = GenerateRandomEnvironment("new environment");
            var existingUserEnvironments = GenerateRandomEnvironments(0);

            // We create some mocks
            var environmentRepository = new Mock<IEnvironmentRepository>();
            var objectRepository = new Mock<IObjectRepository>();
            var userRepository = new Mock<IUserRepository>();

            // We do not setup every method in our mock, only the methods are called  
            // from the unit under test (in this case: environmentController.AddAsync).

            // Return the fake data when the current user is fetched
            userRepository.Setup(x => x.GetCurrentUserId()).Returns(userId);

            // Return the fake data when the environments owned by the current user are fetched
            environmentRepository.Setup(x => x.ReadByOwnerUserId(userId)).ReturnsAsync(existingUserEnvironments);

            // Create the controller            
            var environmentController = new EnvironmentsController(environmentRepository.Object, objectRepository.Object, userRepository.Object);

            // ACT
            var response = await environmentController.AddAsync(newEnvironment);


            // ASSERT

            // The return type of the controller method is Task<ActionResult<Environment2D>>.  
            // To test the outcome of the controller method, we need to dissect this object.  
            // It contains a .Value property and a .Result property.  

            // First, we assert whether the ActionResult is an OkObjectResult and  
            // extract the specific type of this generic type so we can use it later on.  

            // This approach is similar to using int.TryParse(input, out int value) in module 1.1.
            // This assert will fail if another response type is used (e.g. BadRequestObjectResult, or an OkResult (without an object))
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