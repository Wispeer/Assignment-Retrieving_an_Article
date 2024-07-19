using Assignment_Retrieving_an_Article.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment_Retrieving_an_Article.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Product> Products { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .HasMany(a => a.Products)
                .WithMany(p => p.Articles)
                .UsingEntity(j => j.ToTable("ArticleProduct"));
        }
    }
}
