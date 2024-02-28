
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace CompanyDetailsWebAPI.Common
{
    public static class TokenHelper
    {
        private const string SecretKey = ""; // Replace with a secure secret key.
        private static readonly byte[] SecretKeyBytes = Convert.FromBase64String(SecretKey);

        public static string GenerateToken(string userEmail)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Email, userEmail)
            }),
                Expires = DateTime.UtcNow.AddHours(1), // Set the token expiration time.
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(SecretKeyBytes),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static bool ValidateToken(string token, out string userEmail)
        {
            userEmail = null;
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(SecretKeyBytes),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                userEmail = claimsPrincipal.FindFirst(ClaimTypes.Email).Value;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

}
