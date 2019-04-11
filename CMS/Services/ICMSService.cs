using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Services
{
    public interface ICMSService
    {
        Task<List<Page>> GetAllPagesAsync();
        Task<bool> AddPageAsync(Page newPage);
        Task<Comment[]> GetCommentsAsync();
        Page GetPage(Guid pageId);
    }
}
