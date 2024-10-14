using BlogApp.Domain.Entities;
using BlogApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.MVC.Controllers
{
    public class CommentController : Controller
    {
        private readonly CommentService _commentService;

        public CommentController(CommentService commentService)
        {
            _commentService = commentService;
        }

        // POST: /Comment/Create
        [HttpPost]
        [Authorize]  // Somente usuários autenticados podem criar comentários
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int postId, string content)
        {
            var comment = new Comment
            {
                Content = content,
                PostId = postId,
                AuthorId = User.Identity.Name,
                CreatedAt = DateTime.Now
            };

            if (ModelState.IsValid)
            {
                await _commentService.AddCommentAsync(comment);
                return RedirectToAction("Details", "Post", new { id = postId });
            }
            return RedirectToAction("Details", "Post", new { id = postId });
        }
    }
}
