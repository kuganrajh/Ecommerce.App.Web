using AutoMapper;
using Product.App.Domain;
using Shared.RabbitMQ.Models;

namespace Product.App.Web.AutoMapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // Define a mapping from Product to ProductDTO
            CreateMap<ProductItem, ProductUpdatedMessage>();

                // If mannual Mapping required
            //.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            //.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            //.ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            //.ForMember(dest => dest.StockAvailable, opt => opt.MapFrom(src => src.StockAvailable))
            //.ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
            
        }
    }
}
