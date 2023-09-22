using Motocross_Bikes.Interfaces;
using Motocross_Bikes.Models;
using Npgsql;
using System.Diagnostics;

namespace Motocross_Bikes.Repositories
{
    // Handles database operations for users
    public class LoginRepository : ILoginRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connString;

        public LoginRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            
            _connString = _configuration.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// Adds user to the database.
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>True if operation was succesful, otherwise false</returns>
        public async Task<bool> AddUserAsync(User user)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
                {
                    await conn.OpenAsync();
                    Debug.WriteLine("Db Conn Open");
                    using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO \"users\" (name,password,role) VALUES (@name,@password,@role)", conn))
                    {
                        cmd.Parameters.AddWithValue("name", user.Name);
                        cmd.Parameters.AddWithValue("password", user.Password);
                        cmd.Parameters.AddWithValue("role", user.Role);

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                await File.AppendAllTextAsync("error.log", "\n" + ex.Message);

                return false;
            }
        }

        /// <summary>
        /// Checks database for a matching user.
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="password">Password</param>
        /// <returns>User if found, else null</returns>
        public async Task<User> GetUserAsync(LoginRequest loginrequest)
        {
            try
            {
                User user = new User();

                using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
                {
                    await conn.OpenAsync();
                    Debug.WriteLine("Db Conn Open");
                    using (NpgsqlCommand cmd = new NpgsqlCommand($"SELECT * FROM users WHERE name = '{loginrequest.Name}' AND password = '{loginrequest.Password}'", conn))
                    {
                        var reader = await cmd.ExecuteReaderAsync();
                        while (reader.Read())
                        {
                            //Gets id
                            user.Id = reader.GetInt32(0);
                            //Gets name
                            user.Name = reader.GetString(1);
                            //Gets password
                            user.Password = reader.GetString(2);
                            //Gets roleid
                            user.Role = reader.GetString(3);
                        }
                        reader.Close();
                    }
                }
                if (user.Name != null)
                {
                    return user;
                }
                return null;
            }
            catch (Exception ex)
            {
                await File.AppendAllTextAsync("error.log", "\n" + ex.Message);

                return null;
            }
        }
    }
}