using BlogApp.Domain.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.MVC.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly CommentService _commentService;

        public CommentController(CommentService commentService)
        {
            _commentService = commentService;
        }

        // POST: /Comment/Create
        [HttpPost]
        public async Task<IActionResult> Create(int postId, string content)
        {
            var comment = new Comment
            {
                Content = content,
                PostId = postId,
                AuthorId = User.Identity.Name, // Pega o ID do usuário logado
                CreatedAt = DateTime.Now
            };

            await _commentService.AddCommentAsync(comment);
            return RedirectToAction("Details", "Post", new { id = postId });
        }
    }
}
