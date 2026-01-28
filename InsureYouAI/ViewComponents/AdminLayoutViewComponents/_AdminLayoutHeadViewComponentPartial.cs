using Microsoft.AspNetCore.Mvc;

namespace InsureYouAI.ViewComponents.AdminLayoutViewComponents
{
    public class _AdminLayoutHeadViewComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
    
}
