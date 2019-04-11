using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Models;

namespace CMS.Services
{
    public class CMSService : ICMSService
    {
        public Task<bool> AddPageAsync(Page newPage)
        {
            throw new NotImplementedException();
        }

        public Task<Page[]> GetAllPagesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Comment[]> GetCommentsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
