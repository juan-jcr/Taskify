using System.Reflection;
using Microsoft.OpenApi.Models;

namespace WebAPI.Swagger;
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
         var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
         options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
         
      });
      return services;
   }

}
