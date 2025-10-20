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
                    // Se o token está na blacklist, retorna 401 Unauthorized
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }
            }

            await _next(context); // Se não, continua para o próximo middleware
        }
    }
}