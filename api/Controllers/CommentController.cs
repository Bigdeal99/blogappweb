using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Filters;
using api.TransferModels;
using service;

namespace library.Controllers
{
    
    public class CommentController : ControllerBase
    {
        private readonly ILogger<CommentController> _logger;
        private readonly CommentService _commentService;

        public CommentController(ILogger<CommentController> logger,CommentService commentService)
        {
            _logger = logger;
            _commentService = commentService;
        }
        
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> Get()
        {
            var comments = await _commentService.GetCommentsForFeedAsync();
            return Ok(new ResponseDto
            {
                MessageToClient = "Successfully fetched",
                ResponseData = comments
            });
        }
        [HttpGet]
        [Route("/api/comment/{Id}")]
        public async Task<ResponseDto> GetCommentByIdAsync([FromRoute] int Id)
        {
            var comment = await _commentService.GetCommentByIdAsync(Id);
    
            if (comment == null)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                return new ResponseDto()
                {
                    MessageToClient = "Comment not found"
                };
            }

            return new ResponseDto()
            {
                MessageToClient = "Successfully fetched comment",
                ResponseData = comment
            };
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
                    _commentService.UpdateCommentAsync(dto.Name, dto.Email, dto.Text, dto.PublicationDate, dto.BlogPostId)
            };

        } 
        
        
        
        
        [HttpDelete("/api/comment{id}")]
        public async Task<IActionResult> DeleteCommentAsync(int id)
        {
            var success = await _commentService.DeleteCommentAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
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
                ResponseData = _commentService.CreatecommentAsync(dto.Name, dto.Email, dto.Text, dto.PublicationDate, dto.BlogPostId)
            };
        }
    }
}
