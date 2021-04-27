using Microsoft.IdentityModel.Tokens;
using StudentDashboard.Utilities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace StudentDashboard.Security
{
    public static class JwtManager
    {
        /// <summary>
        /// Use the below code to generate symmetric Secret Key
        ///     var hmac = new HMACSHA256();
        ///     var key = Convert.ToBase64String(hmac.Key);
        /// </summary>
        private const string Secret = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";

        public static string GenerateToken(string username, int expireInYears = 1)
        {
            string token=null;
            try
            {
                var symmetricKey = Convert.FromBase64String(Secret);
                var tokenHandler = new JwtSecurityTokenHandler();

                var now = DateTime.UtcNow;
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                            {
                            new Claim(ClaimTypes.Name, username)
                        }),

                    Expires = now.AddYears(Convert.ToInt32(expireInYears)),

                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
                };

                SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
                token = tokenHandler.WriteToken(securityToken);
            }
            catch(Exception Ex)
            {
                StringBuilder m_strLogMessage = new StringBuilder();
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GenerateToken", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return token;
        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                SecurityToken objSecurityToken=null;
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                var symmetricKey = Convert.FromBase64String(Secret);

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters,out objSecurityToken);

                return principal;
            }

            catch (Exception Ex)
            {
                StringBuilder m_strLogMessage = new StringBuilder();
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ClaimsPrincipal", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return null;
        }
    }
}