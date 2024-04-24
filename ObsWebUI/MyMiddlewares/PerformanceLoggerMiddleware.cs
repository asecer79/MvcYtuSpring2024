using System;
using System.Diagnostics;

namespace ObsWebUI.MyMiddlewares
{
    public class PerformanceLoggerMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            Stopwatch sw = Stopwatch.StartNew();

            await next(context);

            sw.Stop();

            var logDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
            var logFilePath = Path.Combine(logDirectoryPath, "PerformanceLogs.txt");

            if (!Directory.Exists(logDirectoryPath))
            {
                Directory.CreateDirectory(logDirectoryPath);
            }
            var log = $"[{DateTime.Now}] ElapsedMs: {sw.Elapsed.TotalMilliseconds}, Url: {context.Request.Path}\n";
            await File.AppendAllTextAsync(logFilePath, log);


        }
    }
}
