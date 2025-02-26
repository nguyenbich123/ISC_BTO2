
namespace AuthorizationAPI.Middlewares
{

    using Microsoft.AspNetCore.Http;
    using System.Net;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                context.Response.ContentType = "application/json";
                var response = new { Status = "error", Message = "Unauthorized. Please login to continue." };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            else if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
            {
                context.Response.ContentType = "application/json";
                var response = new { Status = "error", Message = "Access Denied. You do not have permission." };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }


}
