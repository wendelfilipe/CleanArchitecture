using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CleanArchMvc.Infra.Ioc
{
    public static class DependencyInjectionSwagger
    {
        public static IServiceCollection AddInfrastructureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen( c => 
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CleanArchMvc.Api", Version = "v1"});

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    // definir configurações
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space]" + 
                    "and the your token in the input below. \r\n\r\n Example: \"Bearer 12345abndef\"",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {

                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
        return services;
        }
    }
}