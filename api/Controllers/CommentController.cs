using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using api.Filters;
using api.TransferModels;
using service;
using System.Threading.Tasks;

namespace library.Controllers
{
    [ApiController]
    [Route("api/comment")]
    public class CommentController : ControllerBase
    {
        private readonly ILogger<CommentController> _logger;
        private readonly CommentService _commentService;

        public CommentController(ILogger<CommentController> logger, CommentService commentService)
        {
            _logger = logger;
            _commentService = commentService;
        }

        
        
        [HttpGet]
        [Route("/api/comment")]
        public ResponseDto Get()
        {
            HttpContext.Response.StatusCode = 200;
            return new ResponseDto()
            {
                MessageToClient = "Successfully fetched",
                ResponseData = _commentService.GetCommentsForFeedAsync()
            };
        }

       
        [HttpGet]
        [Route("/api/comment/{Id}")]
        public async Task<ResponseDto> GetBlogByIdAsync([FromRoute] int id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
    
            if (comment == null)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                return new ResponseDto()
                {
                    MessageToClient = "comment not found"
                };
            }

            return new ResponseDto()
            {
                MessageToClient = "Successfully fetched comment",
                ResponseData = comment
            };
        }

       

       
        [HttpDelete]
        [Route("/api/comment/{Id}")]
        public async Task<IActionResult> DeleteCommentAsync([FromRoute] int Id)
        {
            var success = await _commentService.DeleteCommentAsync(Id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
        [HttpPut]
        [ValidateModel]
        [Route("/api/comment/{Id}")]
        public ResponseDto Put([FromRoute] int Id,
            [FromBody] UpdateCommentRequestDto dto)
        {
            HttpContext.Response.StatusCode = 201;
            return new ResponseDto()
            {
                MessageToClient = "Successfully updated",
                ResponseData =
                    _commentService.UpdateCommentAsync(Id, dto.Name, dto.Email, dto.Text, dto.PublicationDate, dto.BlogId)
            };

        } 
        [HttpPost]
        [ValidateModel]
        [Route("/api/comment")]
        public ResponseDto Post([FromBody] CreateCommentRequestDto dto)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status201Created;
            return new ResponseDto()
            {
                MessageToClient = "Successfully created a comment",
                ResponseData = _commentService.CreateCommentAsync(dto.Name, dto.Email, dto.Text, dto.PublicationDate, dto.BlogId)
            };
        }

       
        }
    
    }

