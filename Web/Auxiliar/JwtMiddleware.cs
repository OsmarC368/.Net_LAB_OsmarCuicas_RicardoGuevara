using Core.Entities;
using Core.Interfaces.Services;
using Core.Responses;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Web.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            var idsession = context.Request.Query["idusersession"].ToString();

	

			if (token != null)
            {
				if (!String.IsNullOrEmpty(idsession) && token != null)
				{
					await attachUserToContextConId(context, userService, token, int.Parse(idsession));
				}
				else
				{
					await attachUserToContext(context, userService, token);
				}
			}
                

            await _next(context);
        }

		private async Task attachUserToContextConId(HttpContext context, IUserService userService, string token, int idsession)
        {
			
			try
			{
				
				var tokenHandler = new JwtSecurityTokenHandler();
				var skey = _configuration["Jwt:Key"];
				var key = Encoding.ASCII.GetBytes(skey);
				tokenHandler.ValidateToken(token, new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false,
					ClockSkew = TimeSpan.Zero
				}, out SecurityToken validatedToken);

				

				var jwtToken = (JwtSecurityToken)validatedToken;
				var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

				
				var user = await userService.GetByIdAsync(userId);

				if (user != null && userId == idsession)
                {
					context.Items["ok"] = true;
				}                 
				else
                {
					context.Items["ok"] = null;
				}

			}
			catch
			{
			}
		}


		private async Task attachUserToContext(HttpContext context, IUserService userService, string token)
        {
            try
            {
				
				var tokenHandler = new JwtSecurityTokenHandler();
                var skey = _configuration["Jwt:Key"];
                var key = Encoding.ASCII.GetBytes(skey);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
				
				var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                

                var user = await userService.GetByIdAsync(userId);

                if (user != null) context.Items["ok"] = true;
            }
            catch
            {
            }
        }

    }
}
