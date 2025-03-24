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
                cfg.AddLogMappings();
                cfg.AddUserMappings();
                cfg.AddRoleMappings();
                cfg.AddProductMappings();
                cfg.AddProductImageMappings();
                cfg.AddDiscountTypeMappings();
                cfg.AddDiscountMappings();
                cfg.AddOrderMappings();
                cfg.AddAddressMappings();
                cfg.AddStatusMappings();
                cfg.AddOrderItemMappings();
                cfg.AddInvoiceMappings();
                cfg.AddPaymentMappings();
                cfg.AddProductStockMappings();
                cfg.AddCategoryMappings();
                cfg.AddCountryMappings();
                cfg.AddAddressTypeMappings();
            });

            var mapper = new Mapper(config);
            return mapper;
        }
    }
}