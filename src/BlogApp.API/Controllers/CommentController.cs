using BlogApp.Domain.Entities;
using BlogApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly CommentService _commentService;

        public CommentController(CommentService commentService)
        {
            _commentService = commentService;
        }

        // GET: api/comment/post/5
        [HttpGet("post/{postId}")]
        public async Task<IActionResult> GetCommentsByPostId(int postId)
        {
            var comments = await _commentService.GetCommentsByPostIdAsync(postId);
            return Ok(comments);
        }

        // GET: api/comment/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        // POST: api/comment
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _commentService.AddCommentAsync(comment);
            return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment);
        }

        // PUT: api/comment/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingComment = await _commentService.GetCommentByIdAsync(id);
            if (existingComment == null)
            {
                return NotFound();
            }

            await _commentService.UpdateCommentAsync(comment);
            return NoContent();
        }

        // DELETE: api/comment/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var existingComment = await _commentService.GetCommentByIdAsync(id);
            if (existingComment == null)
            {
                return NotFound();
            }

            await _commentService.DeleteCommentAsync(id);
            return NoContent();
        }
    }
}
