using Motocross_Bikes.Interfaces;
using Motocross_Bikes.Models;
using Motocross_Bikes.Repositories;

namespace Motocross_Bikes.Services
{
    // Provides services for the login process
    public static class LoginService
    {
        /// <summary>
        /// Validates if the user exists in database.
        /// </summary>
        /// <param name="loginRepository">LoginRepository</param>
        /// <param name="loginRequest">LoginRequest</param>
        /// <returns>User if found, otherwise null</returns>
        public async static Task<User> ValidateUser(ILoginRepository loginRepository, LoginRequest loginRequest)
        {
            var user = await loginRepository.GetUserAsync(loginRequest);

            if (user != null)
            {
                return user;
            }

            return null;
        }
    }
}
