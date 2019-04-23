using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid PageId { get; set; }
        public Guid? ParentId { get; set; }
        public string Body { get; set; }
        public DateTime Posted { get; set; }
        
    }
}
