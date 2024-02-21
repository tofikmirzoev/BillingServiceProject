using System.Text;

namespace BillingAPI.CustomMiddlewares;

public class HttpLoggerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<HttpLoggerMiddleware> _logger;

    public HttpLoggerMiddleware(ILogger<HttpLoggerMiddleware> logger, RequestDelegate next)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        _logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path}");
        if (context.Request.Method == "GET")
        {
            var queryString = context.Request.QueryString.Value;
            _logger.LogInformation($"Request Data: {queryString}");
        }
        else if (context.Request.Method == "POST" || context.Request.Method == "PUT")
        {
            context.Request.EnableBuffering();
            using (var reader = new StreamReader(context.Request.Body,Encoding.UTF8,true,1024,true))
            {
                var body = await reader.ReadToEndAsync();
                _logger.LogInformation($"Request Data: {body}");
            }

            context.Request.Body.Seek(0,SeekOrigin.Begin);
            //context.Request.Body.Position = 0;
        }
        
        var originalBodyStream = context.Response.Body;
        using (var responseBody = new MemoryStream())
        {
            context.Response.Body = responseBody;

            await _next(context);
            
            _logger.LogInformation($"Response: {context.Request.Method} {context.Request.Path}, Status: {context.Response.StatusCode}");

            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
        }
    }
}