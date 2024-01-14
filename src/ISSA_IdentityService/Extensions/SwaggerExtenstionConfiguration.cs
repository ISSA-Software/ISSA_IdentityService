using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace ISSA_IdentityService.Extensions
{
    public static class SwaggerExtenstionConfiguration
    {
        public static IServiceCollection ConfigureSwagger( this IServiceCollection services )
        {
            services.AddSwaggerGen( c =>
            {
                c.SwaggerDoc( "v1", new OpenApiInfo { Title = "ISSA Identity Service", Version = "v1" } );
                c.AddSecurityDefinition( "Bearer", new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    Description = @"JWT Authorization header using the Bearer scheme. \n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \n Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                } );
                c.AddSecurityRequirement( new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        []
                    }
                } );
            } );
            return services;
        }
    }
}
