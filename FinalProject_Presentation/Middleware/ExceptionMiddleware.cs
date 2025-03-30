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
                var message = ex.Message;
                var errors = new Dictionary<string, string>();
                context.Response.StatusCode = 500;
                if (ex is CustomException customException)
                {
                    message = customException.Message;
                    errors = customException.Errors;
                    context.Response.StatusCode = customException.Code;
                }
                await context.Response.WriteAsJsonAsync(new { message, errors });
            }
        }
    }
}
