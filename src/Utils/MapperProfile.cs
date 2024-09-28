using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using src.Entity;
using static src.DTO.CategoryDTO;
using static src.DTO.PaymentDTO;
using static src.DTO.ProductDTO;
using static src.DTO.UserDTO;
using static src.DTO.CartDTO;

namespace src.Utils
{
    public class MapperProfile : Profile
    {
        // user
        public MapperProfile()
        {
            // User mappings
            CreateMap<User, UserReadDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcProperty) => srcProperty != null)
                );

            CreateMap<Product, GetProductDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductInfoDto, Product>()
                // CreateMap<UpdateProdouctDescDto, Product>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcProperty) => srcProperty != null)
                );

            // Category mappings
            CreateMap<Category, CategoryReadDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>()
                .ForAllMembers(options =>
                    options.Condition((src, dest, srcProperty) => srcProperty != null)
                );

            // Subcategory mappings
            CreateMap<SubCategory, PaymentReadDto>();
            CreateMap<PaymentCreateDto, SubCategory>();
            CreateMap<PaymentUpdateDto, SubCategory>()
                .ForAllMembers(options =>
                    options.Condition((src, dest, srcProperty) => srcProperty != null)
                );

            // Payment mappings
            CreateMap<Payment, PaymentReadDto>();
            CreateMap<PaymentCreateDto, Payment>();
            CreateMap<PaymentUpdateDto, Payment>()
                .ForAllMembers(options =>
                    options.Condition((src, dest, srcProperty) => srcProperty != null)
                );

            // Cart mappings
            CreateMap<Cart, CartReadDto>();
            CreateMap<CartCreateDto, Cart>();
            CreateMap<CartUpdateDto, Cart>()
                .ForAllMembers(options =>
                    options.Condition((src, dest, srcProperty) => srcProperty != null)
                );

        }
    }
}
