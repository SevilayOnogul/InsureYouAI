using InsureYouAI.Context;
using InsureYouAI.Entities;
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
        public IActionResult BlogDetail(int id)
        {
            ViewBag.i=id;
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

        [HttpGet]
        public PartialViewResult AddComment()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult AddComment(Comment comment)
        {
            comment.CommentDate = DateTime.Now;
            comment.AppUserId = "B0BB9732-0618-4F89-8A39-5B1271EEBD87";
            _context.Comments.Add(comment);
            _context.SaveChanges();
            return RedirectToAction("BlogList");
        }
    }
}
