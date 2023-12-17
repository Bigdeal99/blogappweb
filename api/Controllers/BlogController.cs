using System.ComponentModel.DataAnnotations;
using api.CustomDataAnnotations;
using api.Filters;
using api.TransferModels;
using infrastructure.DataModels;
using infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using service;

namespace library.Controllers;


public class BlogController : ControllerBase
{

    private readonly ILogger<BlogController> _logger;
    private readonly BlogService _blogService;

    public BlogController(ILogger<BlogController> logger,
        BlogService blogService)
    {
        _logger = logger;
        _blogService = blogService;
    }



    [HttpGet]
    [Route("/api/blog")]
    public ResponseDto Get()
    {
        HttpContext.Response.StatusCode = 200;
        return new ResponseDto()
        {
            MessageToClient = "Successfully fetched",
            ResponseData = _blogService.GetBlogForFeedAsync()
        };
    }
    [HttpGet]
    [Route("/api/blog")]
    public async Task<ResponseDto> GetAllBlogsAsync()
    {
        var blogs = await _blogService.GetBlogForFeedAsync();

        return new ResponseDto
        {
            MessageToClient = "Successfully fetched",
            ResponseData = blogs
        };
    }

    [HttpGet]
    [Route("/api/blog/{Id}")]

    public async Task<ResponseDto> GetBlogByIdAsync([FromRoute] int id)
    {
        var blog = await _blogService.GetBlogByIdAsync(id);
    
        if (blog == null)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            return new ResponseDto()
            {
                MessageToClient = "Blog not found"
            };
        }

        return new ResponseDto()
        {
            MessageToClient = "Successfully fetched blog",
            ResponseData = blog
        };
    }
   
    
    
    
    
    [HttpPut]
    [Route("/api/blog/{id}")]
    public async Task<IActionResult> UpdateBlogAsync(int id, [FromBody] UpdateBlogRequestDto blogDto)
    {
        // Map DTO to domain model here
        var blog = new Blog { /* Assign properties from blogDto */ };
        blog.Id = id; // Ensure the ID is set

        var success = await _blogService.UpdateBlogAsync(blog);

        if (!success)
        {
            return NotFound(new ResponseDto { MessageToClient = "Blog not found" });
        }

        return NoContent();
    }
    [HttpDelete]
    [Route("/api/blog/{id}")]
    public async Task<IActionResult> DeleteBlogAsync(int id)
    {
        var success = await _blogService.DeleteBlogAsync(id);

        if (!success)
        {
            return NotFound(new ResponseDto { MessageToClient = "Blog not found" });
        }

        return NoContent();
    }


   
}


