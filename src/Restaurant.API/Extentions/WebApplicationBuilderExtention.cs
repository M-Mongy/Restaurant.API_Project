using Microsoft.OpenApi.Models;
using Restaurant.API.Middlewares;
using Serilog;

namespace Restaurant.API.Extentions
{
    public static class WebApplicationBuilderExtention
    {
        public static void Addpresentaion(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication();
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("bearerAuth", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
            {
                    new OpenApiSecurityScheme
                  {
                        Reference=new OpenApiReference{ Type=ReferenceType.SecurityScheme,Id="bearerAuth"}
                  },
                 []

            }

        });
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddScoped<ErrorHandlingMilddle>();
            builder.Services.AddScoped<RequestTimeLoggingMiddleware>();
            builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration)
);

        }

    }

}
