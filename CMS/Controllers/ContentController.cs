using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Models;
using CMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    public class ContentController : Controller
    {
        private readonly ICMSService _cmsService;

        public ContentController(ICMSService cMSService)
        {
            _cmsService = cMSService ?? throw new ArgumentNullException(nameof(cMSService));
        }
        public IActionResult Index(string permalink)
        {
            ViewData["permalink"] = permalink;
            var page = ControllerContext.HttpContext.Items["cmspage"] as Page;
            return View(page);
        }
    }
}
