using OrdersManager.ControllersActions;

namespace OrdersManager.Middleware
{
    public class TokenMiddleware
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                if (!context.Request.Headers.ContainsKey("token"))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.WriteAsync("UNAUTHORIZED!").Wait();
                    return;
                }
                var tokenExpiration = context.RequestServices.GetService<CheckTokenExpiration>();
                if (!tokenExpiration.IsValid(context.Request.Headers["token"]!))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.WriteAsync("UNAUTHORIZED!").Wait();
                    return;
                }
                
                await next.Invoke(context);
            });
        }
    }
}
