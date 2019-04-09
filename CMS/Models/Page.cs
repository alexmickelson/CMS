using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class Page
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
