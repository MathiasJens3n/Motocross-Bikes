using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Motocross_Bikes.Interfaces;
using Motocross_Bikes.Models;

namespace Motocross_Bikes.Controllers
{
    // Motocross endpoints
    [Route("api/[controller]")]
    [ApiController]
    public class MotocrossController : ControllerBase
    {
        private readonly IBikeRepository _bikeRepository;

        public MotocrossController(IBikeRepository bikeRepository)
        {
            _bikeRepository = bikeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetBikes()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBike(int id)
        {
            var bike = await _bikeRepository.GetBikeAsync(id);

            if(bike != null)
            {
                return Ok(bike);
            }

            return NotFound("Bike not found");
        }

        [HttpPost]
        [Authorize(Roles = "administrator")]
        public async Task<IActionResult> AddBike(Bike bike)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }

            bool succes = await _bikeRepository.AddBikeAsync(bike);

            if (!succes)
            {
                return BadRequest();
            }

            return Ok("Bike has been succesfully added");
        }
    }
}