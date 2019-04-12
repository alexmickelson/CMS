using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    public class ContentController : Controller
    {
        public IActionResult Index(string permalink)
        {
            ViewData["permalink"] = permalink;
            return View();
        }
    }
}