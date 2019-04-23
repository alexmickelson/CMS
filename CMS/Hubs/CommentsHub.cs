using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Data;
using CMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace CMS.Hubs
{
    public class CommentsHub : Hub
    {
        private CMSContext _context;
        private UserManager<User> _userManager;

        public CommentsHub(CMSContext context,
                            UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task addComment(Comment comment)
        {
            var user = await _userManager.GetUserAsync( Context.User);
            //comment.Id = Guid.NewGuid();
            comment.Posted = DateTime.Now;
            if(user != null)
            {
                comment.UserId = user.Id;
                
            } 

            _context.Add(comment);
            await _context.SaveChangesAsync();
            await Clients.All.SendAsync("newComment", comment);
            return;
        }

    }
}
