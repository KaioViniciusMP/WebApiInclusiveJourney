using Microsoft.AspNetCore.Mvc;
using WebApiInclusiveJourney.Application.DTO.Request;
using WebApiInclusiveJourney.Application.DTO.Response;
using WebApiInclusiveJourney.Application.IServices;

namespace WebApiInclusiveJourney.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService service)
        {
            _authService = service;
        }
        [HttpPost("/auth/login")]
        public IActionResult Login([FromBody] AuthRequest request)
        {
            AuthResponse resposta = _authService.Authentication(request);
            if (resposta == null)
            {
                return Unauthorized();
            }
            return Ok(resposta);
        }
    }
}
