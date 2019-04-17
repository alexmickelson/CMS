using CMS.Models;
using CMS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Controllers
{
    public class ImageController : Controller
    {
        private readonly ICMSService _cmsService;
        private readonly UserManager<User> _userManager;

        public ImageController(ICMSService cmsService,
                            UserManager<User> userManager)
        {
            _cmsService = cmsService;
            _userManager = userManager;
        }

        //[Consumes("multipart/form-data")]
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {

            var user = await _userManager.GetUserAsync(User);
            if (image != null && image.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    image.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string base64 = Convert.ToBase64String(fileBytes);

                    var img = new Image() {
                        Picture = base64,
                        //UserId = user.Id,
                        Id = new Guid()
                    
                    };

                    await _cmsService.UploadImage(img);
                    return PartialView(_cmsService.GetAllImages());
                }
            }
            return PartialView(_cmsService.GetAllImages());
        }

        public IActionResult ImageMenu()
        {
            
            return View(_cmsService.GetAllImages());
        }

        public IActionResult GetImages()
        {
            return View(_cmsService.GetAllImages());
        }

    }
}
