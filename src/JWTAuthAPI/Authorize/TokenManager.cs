using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JWTAuthAPI.Authorize
{
    public static class TokenManager
    {
        private static string Secret = "jyCFmEzLWVa4BrrrHl6oq98uiRf94EhuqUozYSMc8Olc1C4PTawIu/w7QhOkhrSNkOI43E9iGB+7S4p4nh+TDA==";
        
        private static JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

        public static string GenerateToken(string username)
        {
            byte[] key = Convert.FromBase64String(Secret);
            var descriptor = GenerateTokenDescriptor(username, key);
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);

            return handler.WriteToken(token);
        }

        private static SecurityTokenDescriptor GenerateTokenDescriptor(string username, byte[] key)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "Authenticated")
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(securityKey,
                SecurityAlgorithms.HmacSha256Signature)
            };

            return descriptor;
        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                
                if (jwtToken == null)
                    return null;

                byte[] key = Convert.FromBase64String(Secret);
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, parameters, out SecurityToken securityToken);

                return principal;
            }
            catch
            {
                return null;
            }
        }
    }
}