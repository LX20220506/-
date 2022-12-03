using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class AuthorizeController : ControllerBase
    {
    }
}
