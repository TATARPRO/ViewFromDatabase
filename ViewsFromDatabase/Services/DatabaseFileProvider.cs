using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using System;
using System.ComponentModel.DataAnnotations;
using ViewsFromDatabase.Data;
using ViewsFromDatabase.Models;

namespace ViewsFromDatabase.Services
{
    public class DatabaseFileProvider : IFileProvider
    {
        private readonly ILongRepository<CMSPage> _context;
        public DatabaseFileProvider(ILongRepository<CMSPage> context)
        {
            _context = context;
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return new DatabaseDirectoryContent(_context, subpath);
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            var result = new DatabaseFileInfo(_context, subpath);
            return result.Exists ? result as IFileInfo : new NotFoundFileInfo(subpath);
        }

        public IChangeToken Watch(string filter)
        {
            return new DatabaseChangeToken(_context, filter);
        }
    }
}
