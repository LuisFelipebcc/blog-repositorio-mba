using Microsoft.AspNetCore.Mvc;

namespace BlogApp.MVC.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
