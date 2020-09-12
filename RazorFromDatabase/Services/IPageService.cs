using System.Collections.Generic;
using RazorFromDatabase.Models;

namespace RazorFromDatabase.Services
{
    public interface IPageService
    {
        IEnumerable<Page> GetAllPages();

        Page GetPage(long id);

        void CreatePage(Page page);

        void UpdatePage(Page page);

        void DeletePage(long id);
    }
}
