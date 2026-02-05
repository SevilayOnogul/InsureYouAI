using InsureYouAI.Context;
using InsureYouAI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsureYouAI.ViewComponents.DefaultViewComponents
{
    public class _DefaultLast3ArticleComponentPartial:ViewComponent
    {
        private readonly InsureContext _context;

        public _DefaultLast3ArticleComponentPartial(InsureContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var values = _context.Articles
                .Include(z => z.AppUser)
                .Include(y => y.Category)
                .OrderByDescending(x => x.ArticleId)
                .Take(3)
                .ToList();

            return View(values ?? new List<Article>()); // Liste null ise boş liste gönder
        }

    }
}
