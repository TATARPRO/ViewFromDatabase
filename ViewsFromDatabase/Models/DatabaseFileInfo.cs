using Microsoft.Extensions.FileProviders;
using System;
using System.IO;
using System.Linq;
using System.Text;
using ViewsFromDatabase.Data;

namespace ViewsFromDatabase.Models
{
    public class DatabaseFileInfo : IFileInfo
    {
        private readonly ILongRepository<CMSPage> _pageContext;
        private readonly string _viewPath;
        private byte[] _content;

        public DatabaseFileInfo(ILongRepository<CMSPage> pageContext, string path)
        {
            _pageContext = pageContext;
            GetView(path);
        }

        public bool Exists { get; private set; }

        public string Name => Path.GetFileName(_viewPath);

        public DateTimeOffset LastModified { get; private set; }

        public bool IsDirectory => false;

        public string PhysicalPath => throw new NotImplementedException();

        public long Length
        { get
            {
                using (MemoryStream reader = new MemoryStream(_content))
                {
                    return reader.Length;
                }
            }
        }

        public Stream CreateReadStream()
        {
            return new MemoryStream(_content);
        }

        private void GetView(string path)
        {
            var view = _pageContext.Return().FirstOrDefault(x => x.Location == path);
            if (view != null)
            {
                Exists = true;
                _content = Encoding.UTF8.GetBytes(view.Content);
                LastModified = view.LastModified;
            }
        }
    }
}
