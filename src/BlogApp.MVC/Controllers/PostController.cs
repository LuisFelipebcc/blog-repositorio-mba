using BlogApp.Domain.Entities;
using BlogApp.MVC.Models;
using BlogApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogApp.MVC.Controllers
{
    public class PostController : Controller
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetAllPostsAsync();
            return View(posts);
        }

        public async Task<IActionResult> Details(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Post post)
        {
            if (ModelState.IsValid)
            {
                await _postService.AddPostAsync(post);
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // Implementar Edit e Delete de forma semelhante
    }
}
