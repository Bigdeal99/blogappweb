
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
    [Route("api/comment")]
    public class CommentController : ControllerBase
    {
        private readonly CommentService _commentService;

        public CommentController(CommentService commentService)
        {
            _commentService = commentService;
        }
        
        [HttpGet]
        [Route("/api/Comment")]
        public ResponseDto Get()
        {
            HttpContext.Response.StatusCode = 200;
            return new ResponseDto()
            {
                MessageToClient = "Successfully fetched",
                ResponseData = _commentService.GetCommenForFeedAsync()
            };
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
                    MessageToClient = "comment not found"
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
                    _commentService.UpdateCommentAsync(Id, dto.Name, dto.Email, dto.Text, dto.PublicationDate, dto.BlogPostId)
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
        
        [HttpPost]
        [ValidateModel]
        [Route("/api/comment")]
        public ResponseDto Post([FromBody] CreateCommentRequestDto dto)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status201Created;
            return new ResponseDto()
            {
                MessageToClient = "Successfully created a comment",
                ResponseData = _commentService.CreateCommentAsync (dto.Name, dto.Email, dto.Text, dto.PublicationDate, dto.BlogPostId)
            };
        }
    }
    
}



