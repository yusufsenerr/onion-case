using API.Common.Domain.Commons;

namespace API.Common.Domain.Product
{
    public class Product:BaseEntity
    {
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Stock {  get; set; }
    }
}
