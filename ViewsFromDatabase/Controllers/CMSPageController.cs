using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViewsFromDatabase.Areas.ViewModels;
using ViewsFromDatabase.Models;
using ViewsFromDatabase.Services;

namespace ViewsFromDatabase.Areas.Core.Controllers
{
    //[ApiController]
    //[Area("Core")]
    public class CMSPageController : Controller
    {
        private readonly ICMSPageService _pageService;
        public CMSPageController(ICMSPageService pageService)
        {
            _pageService = pageService;
        }

        public IActionResult Index()
        {
            var model = _pageService.GetAllPages();
            return View(model);
        }

        public IActionResult Preview(long id)
        {
            var model = _pageService.GetPage(id);
            if (model != null)
            {
                View(model.Location);
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CMSPageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var page = new CMSPage(model.Name)
            {
                Content = model.Content
            };
            _pageService.CreatePage(page);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CMSPageViewModel model)
        {

            return View();
        }
    }
}
