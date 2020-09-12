using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using System;
using System.ComponentModel.DataAnnotations;
using RazorFromDatabase.Data;
using RazorFromDatabase.Models;
using Microsoft.Extensions.DependencyInjection;

namespace RazorFromDatabase.Services
{
    public class DatabaseFileProvider : IFileProvider
    {
        //private readonly ILongRepository<Page> _context;
        private readonly IServiceProvider _serviceProvider;
        public DatabaseFileProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            //var pageService = serviceProvider.GetRequiredService<ILongRepository<Page>>();
            //_context = pageService;
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return new DatabaseDirectoryContent(_serviceProvider, subpath);
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            var result = new DatabaseFileInfo(_serviceProvider, subpath);
            return result.Exists ? result as IFileInfo : new NotFoundFileInfo(subpath);
        }

        public IChangeToken Watch(string filter)
        {
            return new DatabaseChangeToken(_serviceProvider, filter);
        }
    }

    public class DatabaseFileSystem
    {
        private readonly ILongRepository<Page> _context;
        public DatabaseFileSystem(ILongRepository<Page> context)
        {
            _context = context;
        }


    }
}
