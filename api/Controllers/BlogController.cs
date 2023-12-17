using System.ComponentModel.DataAnnotations;
using api.CustomDataAnnotations;
using api.Filters;
using api.TransferModels;
using infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using service;

namespace library.Controllers

{
    [Route("/api/blog")]
    public class BlogController : ControllerBase
    {
        private readonly ILogger<BlogController> _logger;
        private readonly BlogService _blogService;

        public BlogController(ILogger<BlogController> logger, BlogService blogService)
        {
            _logger = logger;
            _blogService = blogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var blogs = await _blogService.GetBlogForFeedAsync();
            return Ok(new ResponseDto
            {
                MessageToClient = "Successfully fetched",
                ResponseData = blogs
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogByIdAsync(int id)
        {
            var blog = await _blogService.GetBlogByIdAsync(id);
            if (blog == null)
            {
                return NotFound(new ResponseDto { MessageToClient = "Blog not found" });
            }

            return Ok(new ResponseDto
            {
                MessageToClient = "Successfully fetched blog",
                ResponseData = blog
            });
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> PostAsync([FromBody] CreateBlogRequestDto dto)
        {
            var blog = await _blogService.CreateBlogAsync(dto);
            return CreatedAtAction(nameof(GetBlogByIdAsync), new { id = blog.Id }, blog);
        }
    }
}

