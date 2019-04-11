using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class Page
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        //Change
        public List<Comment> Comments { get; set; }
    }
}
