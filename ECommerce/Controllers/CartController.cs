using Core.Services.Interfaces;
using ECommerce.Core.Repositories.Interfaces;
using ECommerce.Entities.DtoModels.Display;
using ECommerce.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IUnitOfWork _unit;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICartService _cartService;

        public CartController(IUnitOfWork unit , UserManager<AppUser> userManager, 
            ICartService cartService)
        {
            _unit = unit;
            _userManager = userManager;
            _cartService = cartService;
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("GetCartProducts")]
        public async Task<IActionResult> GetProducts(string Username)
        {
            var result = await _cartService.GetProducts(Username);
            if (result is null)
                return BadRequest();

            return Ok(result);
        }

        [Authorize(Roles = "Customer")]
        [HttpDelete("RemoveProduct")]
        public async Task<IActionResult> RemoveProduct(int Id, string Username)
        {
            var result = await _cartService.RemoveProduct(Id, Username);

            if(!result)
                return BadRequest($"The Product With Id {Id} Doesn't Exists!");

            return Ok();
        }
    
    }
}
