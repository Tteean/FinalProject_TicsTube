using FinalProject_Service.Exceptions;

namespace FinalProject_Presentation.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public ExceptionMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate.Invoke(context);
            }
            catch (Exception ex)
            {
                if (context.Response.HasStarted)
                {
                    throw; // Если ответ уже отправлен, повторно кидаем исключение
                }

                context.Response.Clear();

                if (IsApiRequest(context))
                {
                    var message = ex.Message;
                    var errors = new Dictionary<string, string>();
                    context.Response.StatusCode = 500;
                    if (ex is CustomException customException)
                    {
                        message = customException.Message;
                        errors = customException.Errors;
                        context.Response.StatusCode = customException.Code;
                    }
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(new { message, errors });
                }
                else
                {
                    context.Response.Redirect("/Error");
                }
            }
        }

        private bool IsApiRequest(HttpContext context)
        {
            return context.Request.Headers["Accept"].Any(h => h.Contains("application/json"));
        }

    }
}
