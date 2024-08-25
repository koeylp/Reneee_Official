﻿using AutoMapper;
using Reneee.Application.DTOs.Attribute;
using Reneee.Application.DTOs.AttributeValue;
using Reneee.Application.DTOs.Category;
using Reneee.Application.DTOs.Comment;
using Reneee.Application.DTOs.District;
using Reneee.Application.DTOs.Order;
using Reneee.Application.DTOs.OrderDetails;
using Reneee.Application.DTOs.Payment;
using Reneee.Application.DTOs.Product;
using Reneee.Application.DTOs.ProductAttribute;
using Reneee.Application.DTOs.ProductImage;
using Reneee.Application.DTOs.ProductPromotion;
using Reneee.Application.DTOs.Promotion;
using Reneee.Application.DTOs.Province;
using Reneee.Application.DTOs.Transaction;
using Reneee.Application.DTOs.User;
using Reneee.Application.DTOs.Ward;
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
            CreateMap<OrderDetails, OrderDetailsDto>()
                .ForMember(dest => dest.ProductAttributeInfo, opt => opt.MapFrom(src => src.ProductAttribute));
            CreateMap<Payment, PaymentDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductInfoDto>();
            CreateMap<ProductAttribute, ProductAttributeDto>().ReverseMap();
            CreateMap<ProductAttribute, ProductAttributeInfoDto>()
                .ForMember(dest => dest.ProductInfo, opt => opt.MapFrom(src => src.Product));
            CreateMap<ProductImage, ProductImageDto>().ReverseMap();
            CreateMap<ProductPromotion, ProductPromotionDto>().ReverseMap();
            CreateMap<Promotion, PromotionDto>().ReverseMap();
            CreateMap<Transaction, TransactionDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<Province, ProvinceDto>()
                .ForMember(dest => dest.ProvinceID, opt => opt.MapFrom(src => src.code))
                .ForMember(dest => dest.ProvinceName, opt => opt.MapFrom(src => src.full_name_en));
            CreateMap<District, DistrictDto>()
               .ForMember(dest => dest.DistrictID, opt => opt.MapFrom(src => src.code))
               .ForMember(dest => dest.DistrictName, opt => opt.MapFrom(src => src.full_name_en));
            CreateMap<Ward, WardDto>()
              .ForMember(dest => dest.WardCode, opt => opt.MapFrom(src => src.code))
              .ForMember(dest => dest.WardName, opt => opt.MapFrom(src => src.full_name_en));
        }

    }
}
