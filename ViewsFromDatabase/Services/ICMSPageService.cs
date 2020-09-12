using System.Collections.Generic;
using ViewsFromDatabase.Models;

namespace ViewsFromDatabase.Services
{
    public interface ICMSPageService
    {
        IEnumerable<CMSPage> GetAllPages();

        CMSPage GetPage(long id);

        void CreatePage(CMSPage page);

        void UpdatePage(CMSPage page);

        void DeletePage(long id);
    }
}
