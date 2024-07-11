using Core.Services.Interfaces;
using ECommerce.Entities.DtoModels.Create;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AccountController(IAuthService _authService)
        {
            this._authService = _authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]CreateUser model)
        {
            if (!ModelState.IsValid)
                return BadRequest(model);

            var result = await _authService.RegisterAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.message);

            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUser model)
        {
            if (!ModelState.IsValid)
                return BadRequest(model);

            var result = await _authService.GetTokenAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.message);

            return Ok(result);
        }
    }
}
