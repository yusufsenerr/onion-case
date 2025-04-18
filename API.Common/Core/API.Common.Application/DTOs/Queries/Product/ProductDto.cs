namespace API.Common.Application.DTOs.Queries.Product
{
    public class ProductDto
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public List<string> ProductImages { get; set; }
    }
}
