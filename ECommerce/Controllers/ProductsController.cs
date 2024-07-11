using Core.Services.Interfaces;
using ECommerce.Core.Repositories.Interfaces;
using ECommerce.Entities.DtoModels.Create;
using ECommerce.Entities.DtoModels.Display;
using ECommerce.Entities.DtoModels.Update;
using ECommerce.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unit;
        private readonly UserManager<AppUser> _userManager;
        private readonly IProductSevice _productService;

        public ProductsController(IUnitOfWork _unit , UserManager<AppUser> _userManager, 
            IProductSevice productService)
        {
            this._unit = _unit;
            this._userManager = _userManager;
            _productService = productService;
        }

        [Authorize(Roles = "Customer, Seller")]
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _productService.GetAllProducts();

            return Ok(result);
        }

        [Authorize(Roles = "Customer, Seller")]
        [HttpGet("GetProduct/{Id}")]
        public async Task<IActionResult> GetProduct(int Id)
        {
            var result = await _productService.GetProduct(Id);
            if(result is null)
                return BadRequest($"The Product With Id {Id} Doesn't Exists!");

            return Ok(result);
        }

        [Authorize(Roles = "Seller")]
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromForm]CreateProduct model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productService.AddProduct(model);

            if(result.Name is "Registered")
                return BadRequest($"The Product With Name {model.Name} Already Exist!");

            if (result is null)
                return BadRequest("Failed to add Product");

            return Ok(result);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("AddProductToCart/{Id}")]
        public async Task<IActionResult> AddProductToCart(int Id, string Username)
        {
            var result = await _productService.AddProductToCart(Username, Id);
            if(result is false)
                return BadRequest($"This Product or user Doesn't Exists!");

            return Ok();
        }

        [Authorize(Roles = "Seller")]
        [HttpPut("updateProduct")]
        public async Task<IActionResult> UpdateProduct([FromForm] UpdateProduct model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productService.UpdateProduct(model);

            if(result.Name is "Not Registered")
                return BadRequest($"The Product With ID {model.Id} Not Exist!");

            if (result is null)
                return BadRequest("Failed to update Product");

            return Ok(result);
        }

        [Authorize(Roles = "Seller")]
        [HttpDelete("DeleteProduct/{Id}")]
        public async Task<IActionResult> DeleteProduct(int Id)
        {
            var result = await _productService.DeleteProduct(Id);

            if (!result)
                return BadRequest();

            return Ok();
        }


    }
}
