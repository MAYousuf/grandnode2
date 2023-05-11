using Grand.Infrastructure.Models;

namespace Authentication.OpenIdConnect.Models
{
    public class FailedModel : BaseModel
    {
        public string ErrorMessage { get; set; }
    }
}
