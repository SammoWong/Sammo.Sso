using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sammo.Sso.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Sammo.Sso.Web.Filters
{
    public class GlobalAccessLogFilter : ActionFilterAttribute
    {
        private readonly ILogger _logger;

        public GlobalAccessLogFilter(ILogger<GlobalAccessLogFilter> logger)
        {
            _logger = logger;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var watch = new Stopwatch();
            watch.Start();
            ActionExecutedContext result = await next();
            watch.Stop();
            if (result.Exception == null)
            {
                LogInformation(context, result, watch);
            }
            else
            {
                LogError(context, watch, result.Exception);
            }
        }

        private void LogError(ActionExecutingContext context, Stopwatch watch, Exception exception)
        {
            object response_content = null;
            var log = new AccessLogModel()
            {
                time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms"),
                elapsed_time = watch.Elapsed.TotalMilliseconds,
                interface_name = context.ActionDescriptor.DisplayName,
                request_content = context.ActionArguments,
                response_content = response_content,
                source_ip = context.HttpContext.Connection.RemoteIpAddress.ToString(),
                status = exception is KnownException ex ? ex.Code : 500,
                msg = exception.ToString(),
            };
            _logger.LogError(JsonConvert.SerializeObject(log));
        }

        private void LogInformation(ActionExecutingContext executingContext, ActionExecutedContext executedContext, Stopwatch watch)
        {
            object response_content = null;
            if (executedContext.Result is ObjectResult objectResult)
                response_content = objectResult.Value;

            var log = new AccessLogModel()
            {
                time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms"),
                elapsed_time = watch.Elapsed.TotalMilliseconds,
                interface_name = executingContext.ActionDescriptor.DisplayName,
                request_content = executingContext.ActionArguments,
                response_content = response_content,
                source_ip = executingContext.HttpContext.Connection.RemoteIpAddress.ToString(),
                status = executedContext.HttpContext.Response.StatusCode
            };
            _logger.LogInformation(JsonConvert.SerializeObject(log));
        }
    }

    internal class AccessLogModel
    {
        /// <summary>
        /// 日志产生时间
        /// </summary>
        public string time { get; set; }

        /// <summary>
        /// 接口调用返回码，返回码意义参照全局错误代码
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 接口调用耗时，ms为单位
        /// </summary>
        public double elapsed_time { get; set; }

        /// <summary>
        /// 调用方服务名称
        /// </summary>
        public string source_srv { get; set; }

        /// <summary>
        /// 调用方来源ip
        /// </summary>
        public string source_ip { get; set; }

        /// <summary>
        /// 接口名称
        /// </summary>
        public string interface_name { get; set; }

        /// <summary>
        /// 请求体内容，可以细化为更详细的字段
        /// </summary>
        public object request_content { get; set; }

        /// <summary>
        /// 响应体内容，可以细化为更详细的字段
        /// </summary>
        public object response_content { get; set; }

        /// <summary>
        ///  消息体，此字段作为扩展字段，可以为空，也可以存放发生异常时一些错误堆栈信息之类的
        /// </summary>
        public string msg { get; set; }
    }
}
