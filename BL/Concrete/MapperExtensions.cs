using AutoMapper;
using Entities.DTO;
using Entities.Models;

namespace BL.Concrete
{
    public static class MapperExtensions
    {
        public static void AddCompanyMappings(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Company, CompanyGetDto>();
            cfg.CreateMap<CompanyPostDto, Company>();
            cfg.CreateMap<CompanyPutDto, Company>();
        }

        public static void AddPermissionMappings(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Permission, PermissionGetDto>();
            cfg.CreateMap<PermissionPostDto, Permission>();
            cfg.CreateMap<PermissionPutDto, Permission>();
        }
    }
}