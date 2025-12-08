using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace UI_MVC.Middlwares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var requestId = Guid.NewGuid().ToString();
            context.Items["RequestId"] = requestId;

            await LogRequest(context, requestId);

            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                await _next(context);
                stopwatch.Stop();

               
                await LogResponse(context, requestId, stopwatch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();

                
                _logger.LogError(
                    ex,
                    "Request {RequestId} failed after {ElapsedMs}ms",
                    requestId,
                    stopwatch.ElapsedMilliseconds
                );

            
                if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode = 500; 
                   
                    context.Response.Redirect("/Home/Error");
                }

               
            }
            finally
            {
            
                responseBody.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task LogRequest(HttpContext context, string requestId)
        {
            var request = context.Request;

         
            bool isMultipart = request.ContentType != null && request.ContentType.Contains("multipart/form-data");

            string bodyText = "(Not Logged)";

            if (!isMultipart)
            {
                request.EnableBuffering();
                bodyText = await ReadStreamAsync(request.Body);
                request.Body.Position = 0;
            }

            _logger.LogInformation(
                "Request {RequestId}: {Method} {Path} {QueryString} from {RemoteIp}",
                requestId,
                request.Method,
                request.Path,
                request.QueryString,
                context.Connection.RemoteIpAddress
            );

            if (!string.IsNullOrEmpty(bodyText) && !bodyText.Equals("(Not Logged)"))
            {
                _logger.LogDebug("Request {RequestId} Body: {Body}", requestId, bodyText);
            }
        }

        private async Task LogResponse(HttpContext context, string requestId, long elapsedMs)
        {
            var response = context.Response;
            response.Body.Seek(0, SeekOrigin.Begin);
            var body = await ReadStreamAsync(response.Body);
            response.Body.Seek(0, SeekOrigin.Begin);

            _logger.LogInformation(
                "Response {RequestId}: {StatusCode} in {ElapsedMs}ms",
                requestId,
                response.StatusCode,
                elapsedMs
            );

            if (!string.IsNullOrEmpty(body) && response.StatusCode >= 400)
            {
                _logger.LogWarning("Response {RequestId} Error Body: {Body}", requestId, body);
            }
        }

        private static async Task<string> ReadStreamAsync(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(stream, leaveOpen: true);
            var content = await reader.ReadToEndAsync();
            stream.Seek(0, SeekOrigin.Begin);
            return content;
        }
    }
}