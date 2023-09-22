using Motocross_Bikes.Interfaces;
using Motocross_Bikes.Models;
using Npgsql;
using System.Diagnostics;

namespace Motocross_Bikes.Repositories
{
    // Handles database operations for bikes
    public class BikeRepository : IBikeRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connString;

        public BikeRepository(IConfiguration configuration)
        {
            _configuration = configuration;

            _connString = _configuration.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// Adds bike to the database.
        /// </summary>
        /// <param name="bike">Bike</param>
        /// <returns>True if operation was succesful, otherwise false</returns>
        public async Task<bool> AddBikeAsync(Bike bike)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
                {
                    await conn.OpenAsync();
                    Debug.WriteLine("Db Conn Open");
                    using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO bikes (model,year,power,weight,transmission,top_speed) VALUES (@model,@year,@power,@weight,@transmission,@top_speed)", conn))
                    {
                        cmd.Parameters.AddWithValue("model", bike.Model);
                        cmd.Parameters.AddWithValue("year", bike.Year);
                        cmd.Parameters.AddWithValue("power", bike.Power);
                        cmd.Parameters.AddWithValue("weight", bike.Weight);
                        cmd.Parameters.AddWithValue("transmission", bike.Transmission);
                        cmd.Parameters.AddWithValue("top_speed", bike.TopSpeed);

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
        /// Checks database for a matching bike.
        /// </summary>
        /// <returns>Bike Object.</returns>
        public async Task<Bike> GetBikeAsync(int id)
        {
            try
            {
                var bike = new Bike();

                using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
                {
                    await conn.OpenAsync();
                    Debug.WriteLine("Db Conn Open");
                    using (NpgsqlCommand cmd = new NpgsqlCommand($"SELECT * FROM bikes WHERE id = {id}", conn))
                    {
                        var reader = await cmd.ExecuteReaderAsync();
                        while (reader.Read())
                        {
                            //Gets bike id
                            bike.Id = reader.GetInt32(0);
                            //Gets bike model
                            bike.Model = reader.GetString(1);
                            //Gets bike year
                            bike.Year = reader.GetInt32(2);
                            //Gets bike power
                            bike.Power = reader.GetString(3);
                            //Gets bike weight
                            bike.Weight = reader.GetInt32(4);
                            //Gets bike transmission
                            bike.Transmission = reader.GetString(5);
                            //Gets bike topspeed
                            bike.TopSpeed = reader.GetInt32(6);
                        }
                        reader.Close();
                    }
                }
                if (bike.Model != null)
                {
                    return bike;
                }
                return null;
            }
            catch (Exception ex)
            {
                await File.AppendAllTextAsync("error.log", "\n" + ex.Message);

                return null;
            }
        }

        /// <summary>
        /// Return all bikes from the database.
        /// </summary>
        /// <returns>List of bikes.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<List<Bike>> GetBikesAsync()
        {
            throw new NotImplementedException();
        }
    }
}