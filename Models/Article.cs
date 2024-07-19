namespace Assignment_Retrieving_an_Article.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
