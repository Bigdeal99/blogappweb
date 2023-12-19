using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using api.Filters;
using api.TransferModels;
using service;
using System.Threading.Tasks;
using infrastructure.DataModels;
using infrastructure.QueryModels;

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
        public async Task<ActionResult<ResponseDto>> Get()
        {
            var comments = await _commentService.GetCommentsForFeedAsync();
            return Ok(new ResponseDto
            {
                MessageToClient = "Successfully fetched",
                ResponseData = comments
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto>> GetCommentByIdAsync(int id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
    
            if (comment == null)
            {
                return NotFound(new ResponseDto { MessageToClient = "Comment not found" });
            }

            return Ok(new ResponseDto
            {
                MessageToClient = "Successfully fetched comment",
                ResponseData = comment
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommentAsync(int id)
        {
            var success = await _commentService.DeleteCommentAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<ActionResult<ResponseDto>> Put(int id, [FromBody] UpdateCommentRequestDto dto)
        {
            var updatedComment = await _commentService.UpdateCommentAsync(id, dto);
            if (updatedComment == null)
            {
                return NotFound(new ResponseDto { MessageToClient = "Comment not found" });
            }

            return Ok(new ResponseDto
            {
                MessageToClient = "Successfully updated",
                ResponseData = updatedComment
            });
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<ResponseDto>> Post([FromBody] CreateCommentRequestDto dto)
        {
            var createdComment = await _commentService.CreateCommentAsync(dto);
            return CreatedAtAction(nameof(GetCommentByIdAsync), new { id = createdComment.Id }, new ResponseDto
            {
                MessageToClient = "Successfully created a comment",
                ResponseData = createdComment
            });
        }
    }
}
