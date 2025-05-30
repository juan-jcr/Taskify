using Microsoft.OpenApi.Models;

namespace WebAPI;
public static class SwaggerConfiguration
{
   public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
   {
      services.AddSwaggerGen(options =>
      {
         options.SwaggerDoc("v1", new OpenApiInfo
         {
            Title = "Taskify API",
            Version = "v1"
         });

         // Configuración de JWT en Swagger
         options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
         {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Enter the JWT token here"
         });

         options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
               new List<string>()
            }
         });
      });
      return services;
   }

}
