using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Globalization;

namespace M.ServiceAPI.Filters
{
    /// <summary>
    /// 事件日志记录方法请求信息，包含路由，参数，请求时间
    /// </summary>
    public class EventLogAttribute : ActionFilterAttribute
    {
        private readonly Stopwatch _stopwatch;
        private readonly ILogger<EventLogAttribute> _logger;
        public EventLogAttribute(ILogger<EventLogAttribute> logger)
        {
            this._logger = logger;
            if (_stopwatch == null)
            {
                _stopwatch = new Stopwatch();
            }
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            try
            {
                _stopwatch.Stop();
                var request = context.HttpContext.Request;
                string log = string.Format(
                        CultureInfo.InvariantCulture,
                        "AppContract Request {0} {1} {2}://{3}{4}{5}{6} {7} {8} TimeSpent:{9}ms",
                        request.Protocol,
                        request.Method,
                        request.Scheme,
                        request.Host.Value,
                        request.PathBase.Value,
                        request.Path.Value,
                        request.QueryString.Value,
                        request.ContentType,
                        request.ContentLength,
                        _stopwatch.ElapsedMilliseconds
                        );
                _logger.LogInformation(log);
            }
            catch (Exception ex)
            {
                _logger.LogError("EventLogAttribute Occured Error", ex);
            }
            base.OnActionExecuted(context);
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _stopwatch.Restart();
            base.OnActionExecuting(context);
        }
    }
}
