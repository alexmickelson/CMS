using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Nickname { get; set; }
        public string Bio { get; set; }
        public Guid PictureId { get; set; }
    }
}
