using SafeScribe.API.Services;
using System.IdentityModel.Tokens.Jwt;

namespace SafeScribe.API.Middleware
{
    public class JwtBlacklistMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtBlacklistMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ITokenBlacklistService blacklistService)
        {
            var user = context.User;
            if (user.Identity?.IsAuthenticated == true)
            {
                var jti = user.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
                if (jti != null && await blacklistService.IsBlacklistedAsync(jti))
                {

                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }
            }

            await _next(context); 
        }
    }
}