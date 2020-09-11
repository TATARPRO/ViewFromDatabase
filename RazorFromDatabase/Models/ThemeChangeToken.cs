using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace RazorFromDatabase.Models
{
    public class ThemeChangeToken : IChangeToken
    {
        private readonly ILongRepository<Page> _pageContext;
        private readonly string _path;

        public ThemeChangeToken(IServiceProvider serviceProvider, string path)
        {
            var pageService = serviceProvider.GetRequiredService<ILongRepository<Page>>();
            _pageContext = pageService;
            _path = path;
        }

        public bool HasChanged
        {
            get
            {
                var result = _pageContext.Return().FirstOrDefault(x => x.Location == _path);
                if (result == null)
                {
                    return false;
                    //throw new ArgumentException($"The view {_path} was not found.");
                }
                else if (result.LastModified == null)
                {
                    return false;
                }
                else
                {
                    return result.LastModified > result.LastRequested;
                }
            }
        }

        public bool ActiveChangeCallbacks => false;

        public IDisposable RegisterChangeCallback(Action<object> callback, object state)
        {
            return EmptyDisposale.Instance;
        }

        internal class EmptyDisposale : IDisposable
        {
            public static EmptyDisposale Instance { get; } = new EmptyDisposale();

            private EmptyDisposale() { }
            public void Dispose() { }
        }
    }
}
