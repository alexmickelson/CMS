using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    public class UserManagerController : Controller
    {
        private UserManager<User> _userManager;

        public UserManagerController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> GetUserLists()
        {
            var userslist = _userManager.Users.ToList();

            List<User> userslist2 = new List<User>();

            foreach (var bob in userslist)
            {
                userslist2.Add(new User { Email = bob.Email });
            }


            return View(userslist);
            //return View(userslist2);
        }

        public async Task<IActionResult> BlockUserFromSite(string user)
        {
            User RemovedUser = await _userManager.FindByEmailAsync(user);

            await _userManager.RemoveFromRoleAsync(RemovedUser, "Admin");
            await _userManager.RemoveFromRoleAsync(RemovedUser, "Contributor");




            return View("../Home/Index");
        }

        public async Task<IActionResult> UpgradeToAdmin(string user)
        {
            User NewAdminUser = await _userManager.FindByEmailAsync(user);

            await _userManager.AddToRoleAsync(NewAdminUser, "Admin");

            return View("../Home/Index");
        }
    }
}
