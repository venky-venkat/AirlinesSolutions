using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ValidateToken.Filter
{
   public class ValidateUserToken : DelegatingHandler
        {
            public readonly string _secretKey = string.Empty;
          
            public ValidateUserToken()
            {
                var configurationBuilder = new ConfigurationBuilder();
                var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
                configurationBuilder.AddJsonFile(path, false);
                var root = configurationBuilder.Build();
                _secretKey = root.GetSection("AppSettings").GetSection("SecretKey").Value;
              
            }

            public string SecretKey
            {
                get => _secretKey;
            }
           
            public static bool TryRetrieveToken(IEnumerable<string> request, out string token)
            {
                token = null;
                if (!(request.Count() > 0))
                {
                    return false;
                }
                var bearerToken = request.ElementAt(0);
                token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
                return true;
            }

            public HttpStatusCode ValidateToken(IEnumerable<string> requestHeader)
            {
                HttpStatusCode statusCode;
                string token;
                if (!TryRetrieveToken(requestHeader, out token))
                {
                    statusCode = HttpStatusCode.Unauthorized;
                    return statusCode;
                }
                try
                {
                    var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
                    SecurityToken securityToken;
                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                    TokenValidationParameters validationParameters = new TokenValidationParameters()
                    {
                        ValidateLifetime = false,
                       ValidateIssuerSigningKey = false,
                       ValidateIssuer = false,
                       ValidateAudience = false,
                        LifetimeValidator = this.LifetimeValidator,
                        IssuerSigningKey = securityKey
                    };
                    Thread.CurrentPrincipal = handler.ValidateToken(token, validationParameters, out securityToken);
                    statusCode = HttpStatusCode.OK;
                }
                catch (SecurityTokenValidationException)
                {
                    statusCode = HttpStatusCode.Unauthorized;
                }
                catch (Exception)
                {
                    statusCode = HttpStatusCode.InternalServerError;
                }
                return statusCode;
            }

            public bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
            {
                if (expires != null)
                {
                    if (DateTime.UtcNow < expires) return true;
                }
                return false;
            }
        }
    }
