using BlogApp.Core.DbContext;
using BlogApp.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Api.Controllers
{
    [Authorize(Roles = "User,Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Author> _userManager;

        public CommentController(ApplicationDbContext context, UserManager<Author> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {
            return await _context.Comments.Include(c => c.Post).ToListAsync();
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            return comment == null ? NotFound() : Ok(comment);
        }

        [HttpPost]
        public async Task<ActionResult<Comment>> CreateComment(Comment comment)
        {
            var user = await _userManager.GetUserAsync(User);
            comment.AuthorId = user.Id;

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, Comment comment)
        {
            if (id != comment.Id)
                return BadRequest();

            var existingComment = await _context.Comments.FindAsync(id);
            if (existingComment == null)
                return NotFound();

            var user = await _userManager.GetUserAsync(User);
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            if (existingComment.AuthorId != user.Id && !isAdmin)
                return Forbid();

            // Atualiza as propriedades permitidas
            existingComment.Content = comment.Content;
            existingComment.CommentedDate = comment.CommentedDate;

            _context.Entry(existingComment).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
                return NotFound();

            var user = await _userManager.GetUserAsync(User);
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            if (comment.AuthorId != user.Id && !isAdmin)
                return Forbid();

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
