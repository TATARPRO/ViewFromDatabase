using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using System;
using RazorFromDatabase.Models;

namespace RazorFromDatabase.Services
{
    public class ThemeFileProvider : IFileProvider
    {
        //private readonly ILongRepository<Page> _context;
        private readonly IServiceProvider _serviceProvider;
        public ThemeFileProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            //var pageService = serviceProvider.GetRequiredService<ILongRepository<Page>>();
            //_context = pageService;
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return new ThemeDirectoryContent(_serviceProvider, subpath);
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            var result = new ThemeFileInfo(_serviceProvider, subpath);
            return result.Exists ? result as IFileInfo : new NotFoundFileInfo(subpath);
        }

        public IChangeToken Watch(string filter)
        {
            return new ThemeChangeToken(_serviceProvider, filter);
        }
    }
}
