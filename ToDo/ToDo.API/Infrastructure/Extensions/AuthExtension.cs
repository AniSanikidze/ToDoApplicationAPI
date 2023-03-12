﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ToDoApp.API.Infrastructure.Auth
{
    public static class AuthExtension
    {
        public static IServiceCollection AddTokenAuthentication(this IServiceCollection services,string key)
        {
            var keyBytes = Encoding.ASCII.GetBytes(key);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = "localhost",
                    ValidAudience = "localhost"
                });
            return services;
        }
    }
}
