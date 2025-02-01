using AutoMapper;
using OrderMS.App.Domain;
using Shared.RabbitMQ.Models;

namespace OrderMS.App.Web.AutoMapper
{    
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductUpdatedMessage, ProductItem>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId));
        }
    }
}
