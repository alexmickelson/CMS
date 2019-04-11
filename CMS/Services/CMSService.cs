using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Data;
using CMS.Models;

namespace CMS.Services
{
    public class CMSService : ICMSService
    {
        private CMSContext _context;

        public CMSService(CMSContext context)
        {
            _context = context;
        }

        public async Task<bool> AddPageAsync(Page newPage)
        {
            await _context.Pages.AddAsync(newPage);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Page>> GetAllPagesAsync()
        {
            var pages = _context.Pages.ToList<Page>();
            await _context.SaveChangesAsync();
            return pages;
        }

        public Task<Comment[]> GetCommentsAsync()
        {
            throw new NotImplementedException();
        }

        public Page GetPage(Guid pageId)
        {
            var a = _context.Pages.Where(p => p.Id == pageId);
            return a.First();
        }
    }
}
