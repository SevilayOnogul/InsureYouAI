using InsureYouAI.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsureYouAI.ViewComponents.BlogViewComponents
{
    public class _BlogListByCategoryComponentPartial:ViewComponent
    {
        private readonly InsureContext _context;

        public _BlogListByCategoryComponentPartial(InsureContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int id)
        {
            var values=_context.Articles.Include(x=>x.Category).Where(y=>y.CategoryId==id).Include(z=>z.AppUser).ToList();
            return View(values);
        }
    }
}
