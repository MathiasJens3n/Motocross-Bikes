using Microsoft.Extensions.Configuration;
using Moq;
using Motocross_Bikes.Models;
using Motocross_Bikes.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Motocross_Bikes.Tests
{
    public class TokenServiceTests
    {
        [Fact]
        public async Task GenerateJwtToken_NoExeption_ReturnsToken()
        {
            // Arrange 
            var user = new User()
            {
                Name = "test",
                Role = "test"
            };
            var conf = new Mock<IConfiguration>();
            conf.Setup(x => x["JwtSettings:SecretKey"]).Returns("VeryMuchSpecialSecretKey123456789");


            var tokenService = new TokenService(conf.Object);

            // Act
            var result = await tokenService.GenerateJwtToken(user);

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task GenerateJwtToken_Exeption_ReturnsNull()
        {
            // Arrange 
            var user = new User();
            var conf = new Mock<IConfiguration>();
            conf.Setup(x => x[null]);


            var tokenService = new TokenService(conf.Object);

            // Act
            var result = await tokenService.GenerateJwtToken(user);

            // Assert
            Assert.Null(result);
        }

    }
}
