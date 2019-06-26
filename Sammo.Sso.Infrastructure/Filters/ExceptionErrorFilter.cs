using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sammo.Sso.Domain.Constants;
using Sammo.Sso.Infrastructure.Exceptions;

namespace Sammo.Sso.Infrastructure.Filters
{
    public class ExceptionErrorFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is KnownException exception)
            {
                context.Result = new JsonResult(new
                {
                    Code = exception.Code,
                    Message = context.Exception.Message,
                    Data = string.Empty
                });
            }
            else
            {
                context.Result = new JsonResult(new
                {
                    Code = ErrorCode.Default,
                    Message = "System Error",
                    Data = string.Empty
                });
            }
        }
    }
}
