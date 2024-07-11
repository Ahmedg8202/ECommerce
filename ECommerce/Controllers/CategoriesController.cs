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
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unit;
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(IUnitOfWork _unit, ICategoriesService categoriesService)
        {
            this._unit = _unit;
            _categoriesService = categoriesService;
        }

        [Authorize(Roles = "Customer, Seller")]
        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _categoriesService.GetAllCategories();
            return Ok(result);
        }

        [Authorize(Roles = "Customer, Seller")]
        [HttpGet("GetCategory/{Id}")]
        public async Task<IActionResult> GetCategoryById(int Id)
        {
            var result = await _categoriesService.GetCategoryById(Id);
            if(result is null)
                return BadRequest($"The Category With Id {Id} Doesn't Exist!");

            return Ok(result);
        }

        [Authorize(Roles = "Seller")]
        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory(CreateCategory model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _categoriesService.AddCategory(model);
            
            if(result.Name == "Registered")
                return BadRequest($"The Category With Name {model.Name} Already Exist!");

            if (result is null)
                return BadRequest("Failed to add Category");

            return Ok(result);
        }

        [Authorize(Roles = "Seller")]
        [HttpPut("updateCategory")]
        public async Task<IActionResult> updateCategory(UpdateCategory model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _categoriesService.updateCategory(model);

            if (result.Name == "Not Registered")
                return BadRequest($"The Category With Id {model.Id} Doesn't Exist!");

            if (result is null)
                return BadRequest("Failed to update Category");

            return Ok(result);
        }

        [Authorize(Roles = "Seller")]
        [HttpDelete("DeleteCategory/{Id}")]
        public async Task<IActionResult> DeleteCategory(int Id) 
        {
            var Result = await _categoriesService.DeleteCategory(Id);
            if (!Result)
                return BadRequest();

            return Ok();
        }  
    }
}
