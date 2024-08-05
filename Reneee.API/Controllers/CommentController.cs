using Microsoft.AspNetCore.Mvc;
using Reneee.Application.DTOs.Comment;
using Reneee.Application.Services;

namespace Reneee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController(ICommentService commentService) : ControllerBase
    {
        private readonly ICommentService _commentService = commentService;

        [HttpPost]
        public async Task<ActionResult<CommentDto>> CreateComment([FromBody] CreateCommentDto commentRequest)
        {
            return Ok(await _commentService.CreateComment(commentRequest));
        }

        [HttpGet("product/{id}")]
        public async Task<ActionResult<IReadOnlyList<CommentDto>>> GetCommentByProduct([FromRoute] int id)
        {
            return Ok(await _commentService.GetCommentByProduct(id));
        }
    }
}
