using AutoMapper;
using eShopOnWeb.Domain.Contracts.DTOs;
using eShopOnWeb.Domain.Entities;

namespace eShopOnWeb.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // CatalogItem mappings
        CreateMap<CatalogItem, CatalogItemDto>()
            .ForMember(dest => dest.CatalogTypeName, opt => opt.MapFrom(src => src.CatalogType != null ? src.CatalogType.Type : null))
            .ForMember(dest => dest.CatalogBrandName, opt => opt.MapFrom(src => src.CatalogBrand != null ? src.CatalogBrand.Brand : null));

        CreateMap<CatalogItemCreateDto, CatalogItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => "System"))
            .ForMember(dest => dest.CatalogType, opt => opt.Ignore())
            .ForMember(dest => dest.CatalogBrand, opt => opt.Ignore())
            .ForMember(dest => dest.PictureUri, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore());

        CreateMap<CatalogItemUpdateDto, CatalogItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => "System"))
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            .ForMember(dest => dest.CatalogType, opt => opt.Ignore())
            .ForMember(dest => dest.CatalogBrand, opt => opt.Ignore())
            .ForMember(dest => dest.PictureUri, opt => opt.Ignore());

        // CatalogBrand mappings
        CreateMap<CatalogBrand, CatalogBrandDto>();
        CreateMap<CatalogBrandCreateDto, CatalogBrand>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => "System"))
            .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore());

        CreateMap<CatalogBrandUpdateDto, CatalogBrand>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => "System"))
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore());

        // CatalogType mappings
        CreateMap<CatalogType, CatalogTypeDto>();
        CreateMap<CatalogTypeCreateDto, CatalogType>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => "System"))
            .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore());

        CreateMap<CatalogTypeUpdateDto, CatalogType>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => "System"))
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore());
    }
}
