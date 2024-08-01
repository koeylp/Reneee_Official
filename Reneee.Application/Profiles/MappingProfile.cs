using AutoMapper;
using Reneee.Application.DTOs.Attribute;
using Reneee.Application.DTOs.AttributeValue;
using Reneee.Application.DTOs.Category;
using Reneee.Application.DTOs.Order;
using Reneee.Application.DTOs.OrderDetails;
using Reneee.Application.DTOs.Payment;
using Reneee.Application.DTOs.Product;
using Reneee.Application.DTOs.ProductAttribute;
using Reneee.Application.DTOs.ProductImage;
using Reneee.Application.DTOs.ProductPromotion;
using Reneee.Application.DTOs.Promotion;
using Reneee.Application.DTOs.Transaction;
using Reneee.Application.DTOs.User;
using Reneee.Domain.Entities;
using Attribute = Reneee.Domain.Entities.Attribute;


namespace Reneee.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Attribute, AttributeDto>().ReverseMap();
            CreateMap<AttributeValue, AttributeValueDto>().ReverseMap();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderDetails, OrderDetailsDto>().ReverseMap();
            CreateMap<Payment, PaymentDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductAttribute, ProductAttributeDto>().ReverseMap();
            CreateMap<ProductImage, ProductImageDto>().ReverseMap();
            //CreateMap<Promotion, PromotionDto>().ReverseMap();
            CreateMap<ProductPromotion, ProductPromotionDto>().ReverseMap();
            CreateMap<ProductPromotion, PromotionDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Promotion.Id))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Promotion.Description))
            .ForMember(dest => dest.DiscountType, opt => opt.MapFrom(src => src.Promotion.DiscountType.ToString()))
            .ForMember(dest => dest.DiscountValue, opt => opt.MapFrom(src => src.Promotion.DiscountValue))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.Promotion.StartDate.ToString()))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.Promotion.EndDate.ToString()))
            .ForMember(dest => dest.ProductPromotions, opt => opt.MapFrom(src => src.Promotion.ProductPromotions.ToList()));

            CreateMap<Transaction, TransactionDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }

    }
}
