using AutoMapper;
using BL.Abstract;

namespace BL.Concrete
{
    public class MapperConfig : IMapperConfig
    {
        public static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddCompanyMappings();
                cfg.AddPermissionMappings();
            });

            var mapper = new Mapper(config);
            return mapper;
        }
    }
}