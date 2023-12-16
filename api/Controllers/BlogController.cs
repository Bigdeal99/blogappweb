using System.ComponentModel.DataAnnotations;
using api.CustomDataAnnotations;
using api.Filters;
using api.TransferModels;
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

    

    
}


