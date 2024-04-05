namespace ObsWebUI.MyMiddlewares
{
    public class IpLoggerMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            var ipAddress = context.Request.Host.Value;

            var ipLogDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
            var ipLogFilePath = Path.Combine(ipLogDirectoryPath, "IpLogs.txt");

            if (!Directory.Exists(ipLogDirectoryPath))
            {
                Directory.CreateDirectory(ipLogDirectoryPath);
            }

            var log = $"{DateTime.Now} Ip: {context.Request.Host.Value} {Environment.NewLine}";

            await File.AppendAllTextAsync(ipLogFilePath,log);

            await next(context);
        }
    }
}
