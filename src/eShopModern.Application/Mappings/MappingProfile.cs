using AutoMapper;
using eShopModern.Application.DTOs;
using eShopModern.Domain.Entities;

namespace eShopModern.Application.Mappings;

/// <summary>
/// AutoMapper profile for mapping between entities and DTOs
/// </summary>
public class MappingProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the MappingProfile class
    /// </summary>
    public MappingProfile()
    {
        CreateMaps();
    }

    /// <summary>
    /// Creates the AutoMapper mappings
    /// </summary>
    private void CreateMaps()
    {
        // CatalogItem mappings
        CreateMap<CatalogItem, CatalogItemDto>()
            .ForMember(dest => dest.CatalogTypeName, opt => opt.MapFrom(src => src.CatalogType != null ? src.CatalogType.Type : string.Empty))
            .ForMember(dest => dest.CatalogBrandName, opt => opt.MapFrom(src => src.CatalogBrand != null ? src.CatalogBrand.Brand : string.Empty));

        CreateMap<CatalogItemCreateDto, CatalogItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
            .ForMember(dest => dest.CatalogType, opt => opt.Ignore())
            .ForMember(dest => dest.CatalogBrand, opt => opt.Ignore());

        CreateMap<CatalogItemUpdateDto, CatalogItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.CatalogType, opt => opt.Ignore())
            .ForMember(dest => dest.CatalogBrand, opt => opt.Ignore());

        // CatalogBrand mappings
        CreateMap<CatalogBrand, CatalogBrandDto>()
            .ReverseMap();

        CreateMap<CatalogBrandCreateDto, CatalogBrand>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
            .ForMember(dest => dest.CatalogItems, opt => opt.Ignore());

        CreateMap<CatalogBrandUpdateDto, CatalogBrand>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.CatalogItems, opt => opt.Ignore());

        // CatalogType mappings
        CreateMap<CatalogType, CatalogTypeDto>()
            .ReverseMap();

        CreateMap<CatalogTypeCreateDto, CatalogType>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
            .ForMember(dest => dest.CatalogItems, opt => opt.Ignore());

        CreateMap<CatalogTypeUpdateDto, CatalogType>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.CatalogItems, opt => opt.Ignore());
    }
}