using Microsoft.Extensions.FileProviders;
using System;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace RazorFromDatabase.Models
{
    public class ThemeFileInfo : IFileInfo
    {
        private readonly ILongRepository<Page> _pageContext;
        private readonly string _viewPath;
        private byte[] _content;

        public ThemeFileInfo(IServiceProvider serviceProvider, string path)
        {
            var pageService = serviceProvider.GetRequiredService<ILongRepository<Page>>();
            _pageContext = pageService;
            _viewPath = path;
            GetView(path);
        }

        public bool Exists { get; private set; }

        public string Name => Path.GetFileName(_viewPath);

        public DateTimeOffset LastModified { get; private set; }

        public bool IsDirectory => false;

        public string PhysicalPath => "/Views/Page/";

        public long Length
        {
            get
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
