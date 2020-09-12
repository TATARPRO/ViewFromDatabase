using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.DependencyInjection;
using ViewsFromDatabase.Models;
using ViewsFromDatabase.Services;

namespace ViewsFromDatabase.Extensions
{
    public static class StartupExtension
    {
        public static IServiceCollection AddDatabaseFileProvider(this IServiceCollection services)
        {
            services.Configure<MvcRazorRuntimeCompilationOptions>(options =>
            {
                var pageService = services.BuildServiceProvider().GetRequiredService<ILongRepository<CMSPage>>();
                options.FileProviders.Add(new DatabaseFileProvider(pageService));
            });
            return services;
        }
    }
}
