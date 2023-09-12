using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SpecificationPatternConsoleApp.Database.Entites;

namespace SpecificationPatternConsoleApp.Database
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            this.EnsureSeedData();
        }

        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<FileEntity> Files { get; set; }
        public DbSet<ImageEntity> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectEntity>(build =>
            {
                build.HasKey(entry => entry.Id);
                build.Property(entry => entry.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<FileEntity>(build =>
            {
                build.HasKey(entry => entry.Id);
                build.Property(entry => entry.Id).ValueGeneratedOnAdd();
                build.HasOne(entry => entry.Project).WithMany(p => p.Files);
            });

            modelBuilder.Entity<ImageEntity>(build =>
            {
                build.HasKey(entry => entry.Id);
                build.Property(entry => entry.Id).ValueGeneratedOnAdd();
                build.HasOne(entry => entry.Project).WithMany(p => p.Images);
                build.HasMany(entry => entry.Hashtags);
            });

            modelBuilder.Entity<HashtagEntity>(build =>
            {
                build.HasKey(entry => entry.Id);
                build.Property(entry => entry.Id).ValueGeneratedOnAdd();
                build.HasMany(entry => entry.Images);
            });
        }
    }
}