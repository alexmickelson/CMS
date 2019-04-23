using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Data;
using CMS.Models;
using CMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    public class CommentsController : Controller
    {
        private CMSContext _context;

        public CommentsController(CMSContext context)
        {
            _context = context;
        }

        public List<Comment> getComments()
        {
            return _context.Comments.ToList();
        }

        [HttpPost]
        public IActionResult getComments([FromBody]Guid pageId)
        {
            // all comments
            var allComments = _context.Comments.Where(c => c.PageId == pageId).ToList();
            ViewBag.Comments = allComments;
            //only top level comments
            return PartialView(allComments.Where(c => c.ParentId == null).ToList());
        }

        //[HttpPost]
        //public async Task<bool> Create([FromBody] Comment comment)
        //{

        //    _context.Add(comment);
        //    await _context.SaveChangesAsync();
        //    return true;


        //}
    }
}
