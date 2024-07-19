using Assignment_Retrieving_an_Article.Controllers;
using Assignment_Retrieving_an_Article.Data;
using Assignment_Retrieving_an_Article.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Assignment_Retrieving_an_Article.Tests
{
    public class ArticlesControllerTests
    {
        private readonly AppDbContext _context;
        private readonly ArticlesController _controller;

        public ArticlesControllerTests()
        {
            _context = InMemoryDbContextFactory.Create();
            _controller = new ArticlesController(_context);

            // Seed in-memory database with test data
            var products1 = new List<Product>
            {
                new Product { Name = "Test Product 1", Description = "Test Description 1" },
                new Product { Name = "Test Product 2", Description = "Test Description 2" }
            };

            var products2 = new List<Product>
            {
                new Product { Name = "Test Product 3", Description = "Test Description 3" },
                new Product { Name = "Test Product 4", Description = "Test Description 4" }
            };

            var articles = new List<Article>
            {
                new Article
                {
                    Title = "Test Article 1",
                    Description = "Test Description 1",
                    Content = "Test Content 1",
                    Products = products1
                },
                new Article
                {
                    Title = "Test Article 2",
                    Description = "Test Description 2",
                    Content = "Test Content 2",
                    Products = products2
                }
            };

            _context.Products.AddRange(products1);
            _context.Products.AddRange(products2);
            _context.Articles.AddRange(articles);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetArticle_ReturnsArticle_WithAssociatedProducts()
        {
            // Ensure there is at least one article with ID 1
            var article = await _context.Articles.Include(a => a.Products).FirstOrDefaultAsync(a => a.Id == 1);

            Assert.NotNull(article); // Verify that the article exists

            var result = await _controller.GetArticle(1);
            var okResult = result.Result as OkObjectResult;

            Assert.NotNull(okResult); // Verify that the result is not null
            var articleResult = okResult.Value as ArticleDto;

            Assert.NotNull(articleResult);
            Assert.Equal("Test Article 1", articleResult.Title);
            Assert.Equal(2, articleResult.Products.Count);
        }
    }

    public static class InMemoryDbContextFactory
    {
        public static AppDbContext Create()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(System.Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);
            context.Database.EnsureCreated();
            DbInitializer.Initialize(context);

            return context;
        }
    }
}
