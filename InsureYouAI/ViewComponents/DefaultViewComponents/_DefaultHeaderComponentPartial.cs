using Microsoft.AspNetCore.Mvc;

namespace InsureYouAI.ViewComponents.DefaultViewComponents
{
    public class _DefaultHeaderComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}
