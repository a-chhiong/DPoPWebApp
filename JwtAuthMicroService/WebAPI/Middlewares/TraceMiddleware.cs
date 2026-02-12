using CrossCutting.Constants;
using CrossCutting.Extensions;
using CrossCutting.Logger;

namespace WebAPI.Middlewares;

public class TraceMiddleware
{
    private readonly RequestDelegate _next;

    public TraceMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        var traceId = context.Request.Headers[HttpHeaders.TRACE_ID].FirstOrDefault()
                      ?? Guid.NewGuid().ToString();
        
        TraceContextHolder.CurrentTraceId.Value = traceId;
        
        context.Response.Headers[HttpHeaders.TRACE_ID] = traceId;
        context.Response.Headers[HttpHeaders.VERSION_CODE] = VersionInfo.CODE.SafeFirst(7);
        await _next(context);
    }
}

public static class TraceContextHolder
{
    public static AsyncLocal<string?> CurrentTraceId { get; } = new AsyncLocal<string?>();
}