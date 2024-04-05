namespace ObsWebUI.MyMiddlewares
{
    public class AccessLoggerMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            var logDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
            var logFilePath = Path.Combine(logDirectoryPath, "AccessLogs.txt");

            if (!Directory.Exists(logDirectoryPath))
            {
                Directory.CreateDirectory(logDirectoryPath);
            }

            var formData = Environment.NewLine;

            if (context.Request.Method=="POST")
            {
                foreach (var key in context.Request.Form.Keys)
                {
                    if (key!="__RequestVerificationToken")
                    {
                        formData += " Key: "+key +", Data: "+context.Request.Form[key];
                    }
                }
            }
         

            var log = $"Time: {DateTime.Now} " +
                      $"Ip: {context.Request.Host.Value}" +
                      $"Method: {context.Request.Method}" +
                      $"Request Url: {context.Request.Path}  "+ Environment.NewLine+
                      $"Form Data: {formData}" 
                      ;

            await File.AppendAllTextAsync(logFilePath, log);


            await next(context);
        }
    }
}
