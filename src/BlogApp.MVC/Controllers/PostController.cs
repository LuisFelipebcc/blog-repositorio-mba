using BlogApp.Domain.Entities;
using BlogApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.MVC.Controllers
{
    public class PostController : Controller
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        // GET: /Post/Index
        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetAllPostsAsync();
            return View(posts);
        }

        // GET: /Post/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // GET: /Post/Create
        [Authorize]  // Somente usuários autenticados podem criar posts
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Post/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]  // Proteção contra ataques CSRF
        public async Task<IActionResult> Create(Post post)
        {
            if (ModelState.IsValid)
            {
                post.AuthorId = User.Identity.Name;  // Associar o post ao usuário logado
                await _postService.AddPostAsync(post);
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: /Post/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null || post.AuthorId != User.Identity.Name)
            {
                return Unauthorized();  // Apenas o autor pode editar o post
            }
            return View(post);
        }

        // POST: /Post/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _postService.UpdatePostAsync(post);
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // POST: /Post/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]  // Apenas administradores podem deletar posts
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _postService.DeletePostAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
