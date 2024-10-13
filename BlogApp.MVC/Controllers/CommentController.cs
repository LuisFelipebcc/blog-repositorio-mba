using Microsoft.AspNetCore.Mvc;

namespace BlogApp.MVC.Controllers
{
    public class CommentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
