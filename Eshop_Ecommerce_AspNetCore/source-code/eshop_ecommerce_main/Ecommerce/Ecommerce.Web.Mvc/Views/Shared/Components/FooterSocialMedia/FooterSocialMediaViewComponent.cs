using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecommerce.Web.Mvc.Views.Shared.Components.FooterSocialMedia
{
    public class FooterSocialMediaViewComponent : ViewComponent
    {
        public FooterSocialMediaViewComponent()
        {
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
