using AutoMapper;
using Entities.DTO;
using Entities.Models;

namespace BL.Concrete
{
    public static class MapperExtensions
    {
        public static void AddCompanyMappings(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Company, CompanyGetDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
            cfg.CreateMap<CompanyPostDto, Company>()
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => "89c78a87-fab4-4e6f-b62e-9d84d1a2de63")); // Map to default Status.
            cfg.CreateMap<CompanyPutDto, Company>();
        }

        public static void AddPermissionMappings(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Permission, PermissionGetDto>();
            cfg.CreateMap<PermissionPostDto, Permission>();
            cfg.CreateMap<PermissionPutDto, Permission>();
        }

        public static void AddLogMappings(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Log, LogGetDto>();
            cfg.CreateMap<LogPostDto, Log>();
            cfg.CreateMap<LogType, LogTypeGetDto>();
            cfg.CreateMap<LogTypePostDto, LogType>();
            cfg.CreateMap<LogTypePutDto, LogType>();
        }

        public static void AddRoleMappings(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Role, RoleGetDto>();
            cfg.CreateMap<RolePostDto, Role>();
            cfg.CreateMap<RolePutDto, Role>();
        }

        public static void AddUserMappings(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<User, UserGetDto>();
            cfg.CreateMap<UserPostDto, User>();
            cfg.CreateMap<UserPutDto, User>();
            cfg.CreateMap<UserRegister, User>()
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => "89c78a87-fab4-4e6f-b62e-9d84d1a2de63")) // Map to default status.
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => "8ff752a6-4a57-4b19-b7e6-f17b2dd69149")); // Map to default role.
        }

        public static void AddStatusMappings(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Status, StatusGetDto>();
            cfg.CreateMap<StatusPostDto, Status>();
            cfg.CreateMap<StatusPutDto, Status>();
        }

        public static void AddOrderMappings(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Order, OrderGetDto>();
            cfg.CreateMap<OrderPostDto, Order>()
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => "89c78a87-fab4-4e6f-b62e-9d84d1a2de63")); // Map to default Status.
            cfg.CreateMap<OrderPutDto, Order>();
        }

        public static void AddOrderItemMappings(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<OrderItem, OrderItemGetDto>();
            cfg.CreateMap<OrderItemPostDto, OrderItem>()
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => "USD"));
        }

        public static void AddInvoiceMappings(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Invoice, InvoiceGetDto>();
            cfg.CreateMap<InvoicePostDto, Invoice>();
            cfg.CreateMap<InvoicePutDto, Invoice>();
        }

        public static void AddPaymentMappings(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Payment, PaymentGetDto>();
            cfg.CreateMap<PaymentPostDto, Payment>();
            cfg.CreateMap<PaymentPutDto, Payment>();
        }

        public static void AddProductMappings(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Product, ProductGetDto>();
            cfg.CreateMap<ProductPostDto, Product>();
            cfg.CreateMap<ProductPutDto, Product>();
        }

        public static void AddProductImageMappings(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ProductImage, ProductImageGetDto>();
            cfg.CreateMap<ProductImagePostDto, ProductImage>();
        }

        public static void AddProductStockMappings(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ProductStock, ProductStockGetDto>();
            cfg.CreateMap<ProductStockPostDto, ProductStock>();
            cfg.CreateMap<ProductStockPutDto, ProductStock>();
        }

        public static void AddCategoryMappings(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Category, CategoryGetDto>();
            cfg.CreateMap<CategoryPostDto, Category>();
            cfg.CreateMap<CategoryPutDto, Category>();
        }

        public static void AddLogTypeMappings(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<LogType, LogTypeGetDto>();
            cfg.CreateMap<LogTypePostDto, LogType>();
            cfg.CreateMap<LogTypePutDto, LogType>();
        }

        public static void AddDiscountMappings(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Discount, DiscountGetDto>();
            cfg.CreateMap<DiscountPostDto, Discount>();
            cfg.CreateMap<DiscountPutDto, Discount>();
        }

        public static void AddDiscountTypeMappings(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<DiscountType, DiscountTypeGetDto>();
            cfg.CreateMap<DiscountTypePostDto, DiscountType>();
            cfg.CreateMap<DiscountTypePutDto, DiscountType>();
        }

        public static void AddAddressMappings(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Address, AddressGetDto>();
            cfg.CreateMap<AddressPostDto, Address>();
            cfg.CreateMap<AddressPutDto, Address>();
        }

        public static void AddCountryMappings(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Country, CountryGetDto>();
            cfg.CreateMap<CountryPostDto, Country>();
            cfg.CreateMap<CountryPutDto, Country>();
        }

        public static void AddAddressTypeMappings(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<AddressType, AddressTypeGetDto>();
            cfg.CreateMap<AddressTypePostDto, AddressType>();
            cfg.CreateMap<AddressTypePutDto, AddressType>();
        }
    }
}