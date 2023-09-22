using Motocross_Bikes.Models;

namespace Motocross_Bikes.Interfaces
{
    public interface IBikeRepository
    {
        /// <summary>
        /// Checks database for a matching bike.
        /// </summary>
        /// <returns>Bike Object.</returns>
        public Task<Bike> GetBikeAsync(int id);

        public Task<List<Bike>> GetBikesAsync();

        /// <summary>
        /// Adds bike to the database.
        /// </summary>
        /// <param name="bike">Bike</param>
        /// <returns>True if operation was succesful, otherwise false</returns>
        public Task<bool> AddBikeAsync(Bike bike);
    }
}