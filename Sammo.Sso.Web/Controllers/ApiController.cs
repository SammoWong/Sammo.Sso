using Microsoft.AspNetCore.Mvc;
using Sammo.Sso.Domain.Constants;

namespace Sammo.Sso.Web.Controllers
{
    public class ApiController : ControllerBase
    {
        protected IActionResult Succeed(string message = null, object result = null)
        {
            return Ok(new { Code = ErrorCode.Success, Message = message, Data = result });
        }

        protected IActionResult Fail(int code = ErrorCode.Default, string message = null, object result = null)
        {
            return BadRequest(new { Code = code, Message = message, Data = result });
        }
    }
}