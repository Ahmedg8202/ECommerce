using Core.Services;
using Core.Services.Interfaces;
using ECommerce.Core.Repositories.Interfaces;
using ECommerce.Entities.DtoModels.Create;
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
    public class OrdersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IOrderService _orderService;

        public OrdersController(UserManager<AppUser> _userManager,
            IOrderService orderService)
        {
            this._userManager = _userManager;
            _orderService = orderService;
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrders()
        {
            var result = await _orderService.GetOrders();

            return Ok(result);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("GetOrder/{Id}")]
        public async Task<IActionResult> GetOrder(int Id)
        {
            var result = await _orderService.GetOrder(Id);

            if(result is null)
                return BadRequest($"The Order With Id {Id} Doesn't Exist!");

            return Ok(result);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("AddOrder")]
        public async Task<IActionResult> AddOrder(string Username, [FromBody] List<CreateOrderProduct> products)
        {
            var result = await _orderService.AddOrder(Username, products);

            if (result is null)
                return BadRequest();

            return Ok(products);
        }

        [Authorize(Roles = "Customer")]
        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> DeleteOrder(int Id)
        {
            var result = await _orderService.DeleteOrder(Id);

            if (result is false)
                return BadRequest();

            return Ok();
        }

    }
}
