using System;
/**
 * extension method is inherently a static method.what makes it different from 
 * other static methods is that it accepts this as the first parameter, and 
 * this represents the data tyoe of the object which will be using that extension
 * method.An extension method must be defined inside a static class.
 * 
 * extension methods are great for organizing your code and extending functionalities.
 */
namespace CompanyEmployees.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
           services.AddCors(options =>
           {
               options.AddPolicy("CorsPolicy", builder =>
                    builder
                        .AllowAnyOrigin() // WithOrigins("https://example.com")
                        .AllowAnyMethod() // WithMethods("POST","GET")
                        .AllowAnyHeader()); // WithHeaders("accept","content-type")
           });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {


            });
    }
}

