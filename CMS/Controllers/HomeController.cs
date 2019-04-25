using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CMS.Models;
using CMS.Services;
using Microsoft.AspNetCore.Identity;
using CMS.Data;
using System.Net.Http;
using Newtonsoft.Json;

namespace CMS.Controllers
{
    public class HomeController : Controller
    {
        private ICMSService _cms;
        private CMSContext _context;
        private UserManager<User> _userManager;

        public HomeController(ICMSService cms,
                            UserManager<User> userManager, 
                            CMSContext context)
        {
            _cms = cms;
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("http://quotesondesign.com/wp-json/posts?filter[orderby]=rand&filter[posts_per_page]=1");

            var searchResults = JsonConvert.DeserializeObject<IEnumerable<Quote>>(json);
            ViewData["searchResults"] = searchResults.First();
            //ViewData["permalink"] = permalink;
            return View();
        }

        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            try
            {
                ViewBag.img = _context.Images.Single(i => i.Id == user.PictureId).Picture;
            }
            catch (Exception e)
            {
                ViewBag.img = "";
            }

            return View(user);
        }

        public async Task<IActionResult> EditProfile()
        {

            var user = await _userManager.GetUserAsync(User);

            try
            {
               ViewBag.img = _context.Images.Single(i => i.Id == user.PictureId);
            }
            catch(Exception e)
            {
                ViewBag.img = new Image();
            }

        
            if ( user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Channels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile([FromForm]User user)
        {

            var currUser = await _userManager.GetUserAsync(User);


            currUser.Bio = user.Bio;
            currUser.PictureId = user.PictureId;
            currUser.Name = user.Name;

            if (ModelState.IsValid)
            {
                try
                {
                   var u = await _userManager.UpdateAsync(currUser);
                   ViewBag.img = _context.Images.Single(i => i.Id == currUser.PictureId).Picture;
                    //await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error, whoopsie");
                }
                
            }
            return View("Profile", currUser);
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

            return RedirectToAction("Index", "Content", new { permalink = page.Url });
        }

        public IActionResult RequirementsPage()
        {
            return View();
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
