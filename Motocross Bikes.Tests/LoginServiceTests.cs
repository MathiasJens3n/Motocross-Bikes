using Moq;
using Motocross_Bikes.Interfaces;
using Motocross_Bikes.Models;
using Motocross_Bikes.Services;
using Xunit;

namespace Motocross_Bikes.Tests
{
    public class LoginServiceTests
    {
        [Fact]
        public async Task ValidateUser_UserFound_ReturnsUser()
        {
            // Arrange
            var loginRepoMock = new Mock<ILoginRepository>();
            var loginRequest = new LoginRequest();
            var user = new User();

            loginRepoMock.Setup(x => x.GetUserAsync(loginRequest)).ReturnsAsync(user);

            // Act
            var result = await LoginService.ValidateUser(loginRepoMock.Object, loginRequest);

            // Assert
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task ValidateUser_UserNotFound_ReturnsNull()
        {
            // Arrange
            var loginRepoMock = new Mock<ILoginRepository>();
            var loginRequest = new LoginRequest();

            loginRepoMock.Setup(x => x.GetUserAsync(loginRequest)).ReturnsAsync((User)null);

            // Act
            var result = await LoginService.ValidateUser(loginRepoMock.Object, loginRequest);

            // Assert
            Assert.Equal(null, result);
        }
    }
}