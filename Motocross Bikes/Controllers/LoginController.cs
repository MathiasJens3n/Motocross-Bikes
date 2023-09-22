using Microsoft.AspNetCore.Mvc;
using Motocross_Bikes.Interfaces;
using Motocross_Bikes.Models;
using Motocross_Bikes.Services;

namespace Motocross_Bikes.Controllers
{
    // Login endpoints
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository _loginrepository;
        private readonly TokenService _tokenService;

        public LoginController(ILoginRepository loginRepository, TokenService tokenService)
        {
            _loginrepository = loginRepository;
            _tokenService = tokenService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            if(!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }

            bool succes = await _loginrepository.AddUserAsync(user);

            if (succes)
            {
                return Ok("User has been created");
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }

            var validatedUser = await LoginService.ValidateUser(_loginrepository, loginRequest);

            if (validatedUser != null)
            {
                var token = await _tokenService.GenerateJwtToken(validatedUser);

                if (token != null)
                {
                    return Ok(new { Token = token });
                }

                Problem("An error occured while generation token");
            }

            return NotFound("User not found");
        }
    }
}