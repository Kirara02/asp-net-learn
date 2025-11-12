using ApiService.Models.Entities;
using ApiService.Models.DTOs;
using AutoMapper;

namespace ApiService.Extensions
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<RegisterDto, User>();
            CreateMap<User, AuthResponseDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src));

            // Category
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Product
            CreateMap<Product, ProductReadDto>();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto, Product>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}