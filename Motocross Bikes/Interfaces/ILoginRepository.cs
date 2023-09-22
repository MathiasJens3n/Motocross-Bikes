using Motocross_Bikes.Models;

namespace Motocross_Bikes.Interfaces
{
    public interface ILoginRepository
    {
        /// <summary>
        /// Adds user to the database.
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>True if operation was succesful, otherwise false</returns>

        public Task<bool> AddUserAsync(User user);

        /// <summary>
        /// Checks database for a matching user.
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="password">Password</param>
        /// <returns>User if found, else null</returns>
        public Task<User> GetUserAsync(LoginRequest loginRequest);
    }
}