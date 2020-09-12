using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RazorFromDatabase.Models;

namespace RazorFromDatabase.Services
{
    public class PageService : IPageService
    {
        private readonly ILongRepository<Page> _pageContext;
        public PageService(ILongRepository<Page> pageContext)
        {
            _pageContext = pageContext;
        }

        public IEnumerable<Page> GetAllPages()
        {
            return _pageContext.Return().ToList();
        }

        public Page GetPage(long id)
        {
            return _pageContext.Return().FirstOrDefault(x => x.Id == id);
        }

        public void CreatePage(Page page)
        {
            var existing = _pageContext.Return().Where(x => x.Location.Contains(page.Location)).ToList();
            if (existing != null && existing.Count != 0)
            {
                var lastOccurrence = existing.Last();
                var fileName = Path.GetFileNameWithoutExtension(lastOccurrence.Location);
                var succeeded = int.TryParse(fileName.Split(new char[] { '(', ')' }).Last(), out int number);

                if (succeeded)
                {
                    page.Location = Path.Combine(Path.GetFullPath(page.Location), $"{page.Name}({number})");
                    page.Name = $"{page.Name}({number})";
                }
            }

            _pageContext.Insert(page);
            _pageContext.SaveChanges();
        }

        public void UpdatePage(Page page)
        {
            var existing = _pageContext.Return().FirstOrDefault(x => x.Location == page.Location);
            if (existing != null)
            {
                page.LastModified = DateTime.Now;
                _pageContext.Update(page);
            }
            CreatePage(page);
        }

        public void DeletePage(long id)
        {
            _pageContext.Delete(id);
            _pageContext.SaveChanges();
        }
    }
}
