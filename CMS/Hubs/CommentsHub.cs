using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Data;
using Microsoft.AspNetCore.SignalR;

namespace CMS.Hubs
{
    public class CommentsHub : Hub
    {
        private CMSContext _context;

        public CommentsHub(CMSContext context)
        {
            _context = context;
        }
    }
}
