using System;
using System.Collections.Generic;
using System.Text;
using CMS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CMS.Data
{
    public class CMSContext : IdentityDbContext
    {
        public CMSContext(DbContextOptions<CMSContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            

            builder.Entity<Page>()
                .HasMany<Comment>(b => b.Comments)
                .WithOne();

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
