using Microsoft.AspNetCore.Mvc;

namespace Authentication.Google.Components
{
    [ViewComponent(Name = "OpenIdAuthentication")]
    public class OpenIdAuthenticationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}