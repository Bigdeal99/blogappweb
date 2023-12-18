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

    public BlogController(ILogger<BlogController> logger,BlogService blogService)
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
    [ValidateModel]
    [Route("/api/blog/{Id}")]
    public ResponseDto Put([FromRoute] int Id,
        [FromBody] UpdateBlogRequestDto dto)
    {
        HttpContext.Response.StatusCode = 201;
        return new ResponseDto()
        {
            MessageToClient = "Successfully updated",
            ResponseData =
                _blogService.UpdateBlogAsync(Id, dto.Title, dto.Summary, dto.Content, dto.PublicationDate, dto.CategoryId, dto.FeaturedImage)
        };

    } 

    [HttpDelete]
    [Route("/api/blog/{Id}")]
    public async Task<IActionResult> DeleteBlogAsync([FromRoute] int Id)
    {
        var success = await _blogService.DeleteBlogAsync(Id);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
    [HttpPost]
    [ValidateModel]
    [Route("/api/blog")]
    public ResponseDto Post([FromBody] CreateBlogRequestDto dto)
    {
        HttpContext.Response.StatusCode = StatusCodes.Status201Created;
        return new ResponseDto()
        {
            MessageToClient = "Successfully created a blog",
            ResponseData = _blogService.CreateBlogAsync(dto.Title, dto.Summary, dto.Content, dto.PublicationDate, dto.CategoryId,dto.FeaturedImage)
        };
    }
}



