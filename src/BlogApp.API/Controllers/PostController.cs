using BlogApp.Domain.Entities;
using BlogApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        // GET: api/post
        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postService.GetAllPostsAsync();
            return Ok(posts);
        }

        // GET: api/post/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        // POST: api/post
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _postService.AddPostAsync(post);
            return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
        }

        // PUT: api/post/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingPost = await _postService.GetPostByIdAsync(id);
            if (existingPost == null)
            {
                return NotFound();
            }

            await _postService.UpdatePostAsync(post);
            return NoContent();
        }

        // DELETE: api/post/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var existingPost = await _postService.GetPostByIdAsync(id);
            if (existingPost == null)
            {
                return NotFound();
            }

            await _postService.DeletePostAsync(id);
            return NoContent();
        }
    }
}
