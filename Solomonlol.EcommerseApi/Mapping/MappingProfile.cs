using AutoMapper;
using Solomonlol.EcommerseApi.Models.Base;
using Solomonlol.EcommerseApi.Models.Dto;

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
        }
    }
}
