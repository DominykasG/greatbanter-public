using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace api.Models
{
    public class BanterContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public BanterContext() { }
        public BanterContext(DbContextOptions<BanterContext> dbContext) : base(dbContext) { 
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Banter> Banters { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasMany(e => e.Banters).WithOne(e => e.User).HasForeignKey(e => e.UserId);
        }
    }
}
