using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ViewsFromDatabase.Models;

namespace ViewsFromDatabase.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<CMSPage> CMSPages { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CMSPage>()
                .ToTable("Core_CMSPage");
        }
    }
}
