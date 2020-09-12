using Microsoft.Extensions.FileProviders;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace RazorFromDatabase.Models
{
    public class DatabaseDirectoryContent : IDirectoryContents
    {
        private readonly ILongRepository<Page> _pageContext;
        private readonly IServiceProvider _serviceProvider;
        private string _relativePath;
        private const string directoryPattern = "/Areas/Views/InnertPages/";

        public DatabaseDirectoryContent(IServiceProvider serviceProvider, string relativePath)
        {
            _serviceProvider = serviceProvider;
            var pageService = serviceProvider.GetRequiredService<ILongRepository<Page>>();
            _pageContext = pageService;
            _relativePath = relativePath;
        }

        public bool Exists { 
            get {
                if (_relativePath.Contains(directoryPattern))
                    return true;
                return false;
            }
        }

        public IEnumerator<IFileInfo> GetEnumerator()
        {
            var files = _pageContext.Return().ToList();
            foreach (var item in files)
            {
                yield return new DatabaseFileInfo(_serviceProvider, item.Location);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            var files = _pageContext.Return().ToList();
            return files.GetEnumerator();
        }
    }
}
