using API.Common.Application.DTOs.Queries.Product;
using API.Common.Domain.Product;
using AutoMapper;

namespace Application.MappingRegistration.Mapper.Product
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<API.Common.Domain.Product.Product, ProductDto>()
                .ForMember(dest => dest.ProductImages, opt => opt.MapFrom(src =>
                    src.ProductImages.Select(img => img.ImageBase64).ToList()));
        }
    }
}
