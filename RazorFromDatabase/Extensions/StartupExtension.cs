using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.DependencyInjection;
using RazorFromDatabase.Models;
using RazorFromDatabase.Services;

namespace RazorFromDatabase.Extensions
{
    public static class StartupExtension
    {
        public static IServiceCollection AddDatabaseFileProvider(this IServiceCollection services)
        {
            services.Configure<MvcRazorRuntimeCompilationOptions>(options =>
            {
                var serviceProvider = services.BuildServiceProvider();
                options.FileProviders.Add(new DatabaseFileProvider(serviceProvider));
            });
            return services;
        }
    }
}
