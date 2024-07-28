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
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderDetails, OrderDetailsDto>().ReverseMap();
            CreateMap<Payment, PaymentDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductAttribute, ProductAttributeDto>().ReverseMap();
            CreateMap<ProductImage, ProductImageDto>().ReverseMap();
            CreateMap<ProductPromotion, ProductPromotionDto>().ReverseMap();
            CreateMap<Promotion, PromotionDto>().ReverseMap();
            CreateMap<Transaction, TransactionDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }

    }
}
