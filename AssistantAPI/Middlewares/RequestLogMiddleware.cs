﻿using AssistantAPI.Interfaces;
using RequestLog = AssistantAPI.Models.RequestLog;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AssistantAPI.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RequestLogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogRepository _logRepository;

        public RequestLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task InvokeAsync(HttpContext httpContext, ILogRepository logRepository)
        {
            var watch = new Stopwatch();
            watch.Start();
            string? requestArrives = watch.ElapsedMilliseconds.ToString();
            string? requestLeaves = null;
            httpContext.Response.OnStarting(() => {
                watch.Stop();
                requestLeaves = watch.ElapsedMilliseconds.ToString();
                RequestLog requestLog = new()
                {
                    RequestArrivalTime = requestArrives,
                    RequestLeaveTime = requestLeaves,
                };
                logRepository.AddRequestLog(requestLog);
                return Task.CompletedTask;
            });
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RequestLogMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLogMiddleware>();
        }
    }
}
