using Microsoft.Extensions.FileProviders;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ViewsFromDatabase.Models
{
    public class DatabaseDirectoryContent : IDirectoryContents
    {
        private readonly ILongRepository<CMSPage> _pageContext;
        private string _relativePath;
        private const string directoryPattern = "/Areas/Views/InnertPages/";

        public DatabaseDirectoryContent(ILongRepository<CMSPage> pageContext, string relativePath)
        {
            _pageContext = pageContext;
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
                yield return new DatabaseFileInfo(_pageContext, item.Location);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            var files = _pageContext.Return().ToList();
            return files.GetEnumerator();
        }
    }
}
