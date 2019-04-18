using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CMS.Models;
using CMS.Services;
using Microsoft.AspNetCore.Identity;

namespace CMS.Controllers
{
    public class HomeController : Controller
    {
        private ICMSService _cms;
        private UserManager<User> _userManager;

        public HomeController(ICMSService cms,
                            UserManager<User> userManager)
        {
            _cms = cms;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            //ViewData["permalink"] = permalink;
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public async Task<IActionResult> ListOfPages()
        {
            var pages = await _cms.GetAllPagesAsync();
            return View(pages);
        }

        public IActionResult BuildPage()
        {

            return View();
        }

        public IActionResult ViewPage(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction(nameof(Error));
            }
            var page = _cms.GetPage(id);

            return View(page);
        }

        [HttpPost]
        public async Task<IActionResult> SavePage([FromForm]Page page)
        {
            var user = await _userManager.GetUserAsync(User);
            page.Id = new Guid();

            var a = await _cms.AddPageAsync(page);
            return RedirectToAction(nameof(ListOfPages));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
