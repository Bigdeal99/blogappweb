using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using api.Filters;
using api.TransferModels;
using infrastructure.DataModels;
using service;

namespace library.Controllers

{
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(CategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }



        [HttpGet("{Id}")]
        public async Task<IActionResult> GetBlogsByCategoryIdAsync([FromRoute] int Id)
        {
            try
            {
                var category = await _categoryService.GetBlogsByCategoryIdAsync(Id);

                if (category == null)
                {
                    return NotFound(new ResponseDto { MessageToClient = "Category not found" });
                }

                return Ok(new ResponseDto
                {
                    MessageToClient = "Successfully fetched Category",
                    ResponseData = category
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching category with ID {CategoryId}", Id);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseDto { MessageToClient = "An error occurred while processing your request" });
            }
        }



        [HttpPut]
        [ValidateModel]
        [Route("/api/category/{Id}")]
        public ResponseDto Put([FromRoute] int Id,
            [FromBody] UpdateCategoryRequestDto dto)
        {
            HttpContext.Response.StatusCode = 201;
            return new ResponseDto()
            {
                MessageToClient = "Successfully updated",
                ResponseData =
                    _categoryService.UpdateCategoryAsync(Id, dto.Name, dto.Description)
            };

        }

        [HttpDelete]
        [Route("/api/Category/{Id}")]
        public async Task<IActionResult> DeleteCategoryAsync([FromRoute] int Id)
        {
            var success = await _categoryService.DeleteCategoryAsync(Id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        [ValidateModel]
        [Route("/api/category")]
        public ResponseDto Post([FromBody] CreateCategoryRequestDto dto)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status201Created;
            return new ResponseDto()
            {
                MessageToClient = "Successfully created a category",
                ResponseData = _categoryService.CreateCategoryAsync(dto.Name, dto.Description)
            };
        }
    }
}
    


    
    

