using Microsoft.AspNetCore.Mvc;

namespace Authentication.OpenIdConnect.Components
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