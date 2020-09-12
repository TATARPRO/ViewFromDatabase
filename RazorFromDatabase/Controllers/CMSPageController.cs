using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RazorFromDatabase.Areas.ViewModels;
using RazorFromDatabase.Models;
using RazorFromDatabase.Services;

namespace RazorFromDatabase.Areas.Core.Controllers
{
    //[ApiController]
    //[Area("Core")]
    public class PageController : Controller
    {
        private readonly IPageService _pageService;
        public PageController(IPageService pageService)
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
                return View(model.Location);
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
        public IActionResult Create(PageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var page = new Page(model.Name)
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
        public IActionResult Edit(PageViewModel model)
        {

            return View();
        }
    }
}
