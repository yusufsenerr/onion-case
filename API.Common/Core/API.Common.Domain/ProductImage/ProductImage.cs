using API.Common.Domain.Commons;

namespace API.Common.Domain.ProductImage
{
    public class ProductImage:BaseEntity
    {
        public Guid Id { get; set; }
        public string ImageBase64 { get; set; }
        public Guid ProductId { get; set; }
        public API.Common.Domain.Product.Product Product { get; set; }
    }

}
