using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

namespace school_web.AppCode
{
    public class JwtValidator
    {
        public static ClaimsPrincipal ValidateToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("A9f$K2lP@8xZ#QwR7tY!mN4uV6cB1dE3gH5"); 
            return handler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = "nninternational",

                ValidateAudience = true,
                ValidAudience = "nnierp",

                ValidateLifetime = true,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)

            }, out SecurityToken validatedToken);
        }
    }
}