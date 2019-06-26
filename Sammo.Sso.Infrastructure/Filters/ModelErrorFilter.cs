using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sammo.Sso.Domain.Constants;
using Sammo.Sso.Infrastructure.Extensions;
using System.Linq;

namespace Sammo.Sso.Infrastructure.Filters
{
    public class ModelErrorFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new JsonResult(new
                {
                    Code = ErrorCode.InvalidRequest,
                    Message = context.ModelState.AllModelStateErrors().FirstOrDefault()?.Message,
                    Data = string.Empty
                });
            }
        }
    }
}
