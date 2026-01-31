using InsureYouAI.Context;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAI.Controllers
{
    public class BlogController : Controller
    {
        private readonly InsureContext _context;

        public BlogController(InsureContext context)
        {
            _context = context;
        }

        public IActionResult BlogList()
        {
            return View();
        }

        public PartialViewResult GetBlog()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult GetBlog(string keyword)
        {
            return View();
        }
    }
}
