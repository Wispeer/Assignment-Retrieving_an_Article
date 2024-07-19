using Assignment_Retrieving_an_Article.Models;
using System.Collections.Generic;
using System.Linq;

namespace Assignment_Retrieving_an_Article.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Articles.Any())
            {
                return;   // DB has been seeded
            }

            var products1 = new List<Product>
            {
                new Product { Name = "Product 1", Description = "Description 1" },
                new Product { Name = "Product 2", Description = "Description 2" }
            };

            var products2 = new List<Product>
            {
                new Product { Name = "Product 3", Description = "Description 3" },
                new Product { Name = "Product 4", Description = "Description 4" }
            };

            var articles = new List<Article>
            {
                new Article
                {
                    Title = "Article 1",
                    Description = "Description 1",
                    Content = "Content 1",
                    Products = products1
                },
                new Article
                {
                    Title = "Article 2",
                    Description = "Description 2",
                    Content = "Content 2",
                    Products = products2
                }
            };

            context.Products.AddRange(products1);
            context.Products.AddRange(products2);
            context.Articles.AddRange(articles);
            context.SaveChanges();
        }
    }
}
