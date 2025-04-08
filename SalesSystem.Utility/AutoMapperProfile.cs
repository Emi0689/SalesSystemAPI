using AutoMapper;
using SalesSystem.DTO;
using SalesSystem.Model.Entities;
using System.Globalization;


namespace SalesSystem.Utility
{
    public class AutoMapperProfile: Profile
    {
        private const string cultureInfo = "en-US";

       public AutoMapperProfile() 
       {
            #region Rol
            CreateMap<Rol, RolDTO>().ReverseMap();
            #endregion

            #region Menu
            CreateMap<Menu, MenuDTO>().ReverseMap();
            #endregion

            #region User
            CreateMap<User, UserDTO>().
                ForMember(
                    dest => dest.RolDescription,
                    opt => opt.MapFrom(src => src.IdRolNavigation.Name)
                )
                .ForMember(
                  dest => dest.IsActive,
                  opt => opt.MapFrom(src => src.IsActive == true ? 1 : 0)
                );

            CreateMap<User, SessionDTO>().
                ForMember(
                    dest => dest.RolDescription,
                    opt => opt.MapFrom(src => src.IdRolNavigation.Name)
                );

            CreateMap<UserDTO, User>().
                ForMember(
                    dest => dest.IdRolNavigation,
                    opt => opt.Ignore()
                )
                .ForMember(
                  dest => dest.IsActive,
                  opt => opt.MapFrom(src => src.IsActive == 1 ? true : false)
                );
            #endregion

            #region Category
            CreateMap<Category, CategoryDTO>().ReverseMap();
            #endregion

            #region Product
            CreateMap<Product, ProductDTO>()
                .ForMember(
                    dest => dest.CategoryDescription,
                    opt => opt.MapFrom(src => src.IdCategoryNavigation.Name)
                ).ForMember(
                    dest => dest.Price,
                    opt => opt.MapFrom(src => Convert.ToString(src.Price.Value, new CultureInfo(cultureInfo)))
                ).ForMember(
                    dest => dest.IsActive,
                    opt => opt.MapFrom(src => src.IsActive == true ? 1 : 0)
                );

            CreateMap<ProductDTO, Product>()
                .ForMember(
                    dest => dest.IdCategoryNavigation,
                    opt => opt.Ignore()
                ).ForMember(
                    dest => dest.Price,
                    opt => opt.MapFrom(src => Convert.ToDecimal(src.Price, new CultureInfo(cultureInfo)))
                ).ForMember(
                    dest => dest.IsActive,
                    opt => opt.MapFrom(src => src.IsActive == 1 ? true : false)
                );
            #endregion

            #region Sale
            CreateMap<Sale, SaleDTO>()            
                .ForMember(
                    dest => dest.TotalText,
                    opt => opt.MapFrom(src => Convert.ToDecimal(src.Total, new CultureInfo(cultureInfo)))
                ).ForMember(
                    dest => dest.Timestamp,
                    opt => opt.MapFrom(src => src.Timestamp.Value.ToLocalTime().ToString("dd/MM/yyyy"))
                );

            CreateMap<SaleDTO, Sale>()
                .ForMember(
                    dest => dest.Total,
                    opt => opt.MapFrom(src => Convert.ToDecimal(src.TotalText, new CultureInfo(cultureInfo)))
                );

            #endregion

            #region Sale
            CreateMap<SaleDetails, SaleDetailsDTO>()
               .ForMember(
                    dest => dest.ProductDescription,
                    opt => opt.MapFrom(src => src.IdProductNavigation.Name)
                ).ForMember(
                    dest => dest.PriceText,
                    opt => opt.MapFrom(src => Convert.ToString(src.Price.Value, new CultureInfo(cultureInfo)))
                ).ForMember(
                    dest => dest.TotalText,
                    opt => opt.MapFrom(src => Convert.ToString(src.Total.Value, new CultureInfo(cultureInfo)))
                );

            CreateMap<SaleDetailsDTO, SaleDetails>()
                .ForMember(
                    dest => dest.Price,
                    opt => opt.MapFrom(src => Convert.ToDecimal(src.PriceText, new CultureInfo(cultureInfo)))
                ).ForMember(
                    dest => dest.Total,
                    opt => opt.MapFrom(src => Convert.ToDecimal(src.TotalText, new CultureInfo(cultureInfo)))
                );
            #endregion

            #region Report
            CreateMap<SaleDetails, ReportDTO>()
                .ForMember(
                    dest => dest.Timestamp,
                    opt => opt.MapFrom(src => src.IdSaleNavigation.Timestamp.Value.ToLocalTime().ToString("dd/MM/yyyy"))
                )
                .ForMember(
                    dest => dest.IdNumber,
                    opt => opt.MapFrom(src => src.IdSaleNavigation.Idnumber)
                ).ForMember(
                    dest => dest.PaymentType,
                    opt => opt.MapFrom(src => src.IdSaleNavigation.PaymentType)
                ).ForMember(
                    dest => dest.TotalSale,
                    opt => opt.MapFrom(src => Convert.ToString(src.IdSaleNavigation.Total.Value, new CultureInfo(cultureInfo)))
                ).ForMember(
                    dest => dest.Product,
                    opt => opt.MapFrom(src => Convert.ToString(src.IdProductNavigation.Name))
                ).ForMember(
                    dest => dest.Price,
                    opt => opt.MapFrom(src => Convert.ToString(src.Price.Value, new CultureInfo(cultureInfo)))
                ).ForMember(
                    dest => dest.Total,
                    opt => opt.MapFrom(src => Convert.ToString(src.Total.Value, new CultureInfo(cultureInfo)))
                );
            #endregion
        }
    }
}
