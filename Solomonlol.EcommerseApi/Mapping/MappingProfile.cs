using AutoMapper;
using Solomonlol.EcommerseApi.Models.Base;
using Solomonlol.EcommerseApi.Models.Dto.Category;
using Solomonlol.EcommerseApi.Models.Dto.Product;
using Solomonlol.EcommerseApi.Models.Dto.Sale;
using Solomonlol.EcommerseApi.Models.Dto.User;

namespace Solomonlol.EcommerseApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>()
                .ForMember(c => c.Id, d => d.Ignore())
                .ForMember(c => c.Attributes, d => d.Ignore())
                .ForMember(c => c.Products, d => d.Ignore());

            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>()
                .ForMember(p => p.Id, d => d.Ignore())
                .ForMember(p => p.CategoryId, d => d.UseDestinationValue())
                .ForMember(p => p.Category, d => d.Ignore());

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>()
                .ForMember(u => u.Sales, d => d.Ignore())
                .ForMember(u => u.PasswordHash, d => d.Ignore())
                .ForMember(u => u.Id, d => d.Ignore());
            CreateMap<UserDtoCreation, User>()
                .ForMember(u => u.Sales, d => d.Ignore())
                .ForMember(u => u.PasswordHash, d => d.Ignore())
                .ForMember(u => u.Id, d => d.Ignore());

            CreateMap<ProductAttribute, ProductAttributeDto>();
            CreateMap<ProductAttributeDto, ProductAttribute>()
                .ForMember(p => p.Values, d => d.Ignore());

            CreateMap<ProductAttributeValue, ProductAttributeValueDto>();
            CreateMap<ProductAttributeValueDto, ProductAttributeValue>()
                .ForMember(a => a.Product, d => d.Ignore())
                .ForMember(a => a.ProductId, d => d.Ignore())
                .ForMember(a => a.ProductAttributeId, d => d.Ignore())
                .ForMember(a => a.ProductAttribute, d => d.Ignore());

            CreateMap<Sale, SaleDtoRequest>();
            CreateMap<Sale, SaleDtoResponce>();
            CreateMap<SaleDtoRequest, Sale>()
                .ForMember(s => s.EndedAt, d => d.Ignore())
                .ForMember(s => s.IsEnded, d => d.Ignore())
                .ForMember(s => s.Id, d => d.Ignore())
                .ForMember(s => s.User, s => s.Ignore());

            CreateMap<SaleItem, SaleItemDto>();
            CreateMap<SaleItemDto, SaleItem>();
        }
    }
}
