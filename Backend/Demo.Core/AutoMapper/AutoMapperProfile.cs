using AutoMapper;
using Demo.Domain.DTOs.Category;
using Demo.Domain.DTOs.Menu;
using Demo.Domain.DTOs.Product;
using Demo.Domain.Models.Collections;
using MongoDB.Bson;
using System.Globalization;

namespace Demo.Core.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Input
            CreateMap<MenuDTO, Menu>()
                .ForMember(x => x.Id, opt => opt.MapFrom(origin => new ObjectId(origin.Id)))
                .ForMember(x => x.IsActive, opt => opt.MapFrom(origin => origin.IsActive == "A" ? true : false));

            CreateMap<MenuInput, Menu>().ForMember(x => x.IsActive, opt => opt.MapFrom(origin => origin.IsActive == 1 ? true : false));
            //==========================================================================================================================================
            CreateMap<ProductDTO, Products>() //Update
              .ForMember(x => x.Id, opt => opt.MapFrom(origin => new ObjectId(origin.Id)))
              .ForMember(x => x.CreateDate, opt => opt.MapFrom(origin => DateTime.Now))
              .ForMember(x => x.IsActive, opt => opt.MapFrom(origin => origin.IsActive == "A" ? true : false));

            CreateMap<ProductInput, Products>() //New
              .ForMember(x => x.CreateDate, opt => opt.MapFrom(origin => DateTime.Now));

            CreateMap<ProductDTO, ProductInput>() //New
              .ForMember(x => x.CreateDate, opt => opt.MapFrom(origin => DateTime.Now))
              .ForMember(x => x.IsActive, opt => opt.MapFrom(origin => origin.IsActive == "A" ? true : false));

            #endregion
            //==========================================================================================================================================
            #region Output
            CreateMap<Menu, MenuDTO>()
                .ForMember(x => x.Id, opt => opt.MapFrom(origin => origin.Id.ToString()))
                .ForMember(x => x.IsActive, opt => opt.MapFrom(origin => origin.IsActive == true ? "A" : "I"));
            //==========================================================================================================================================
            CreateMap<Products, ProductDTO>() //Output 
               .ForMember(x => x.Price, opt => opt.MapFrom(origin => string.Format("{0:N}\n", origin.Price)))
               .ForMember(x => x.IsActive, opt => opt.MapFrom(origin => origin.IsActive == true ? "A" : "I"))
               .ForMember(x => x.Id, opt => opt.MapFrom(origin => origin.Id.ToString()));
            //==========================================================================================================================================

            CreateMap<Category, CategoryDTO>() //Output 
              .ForMember(x => x.Id, opt => opt.MapFrom(origin => origin.Id.ToString()));

            CreateMap<CategoryDTO, Category>() //Output 
             .ForMember(x => x.Id, opt => opt.MapFrom(origin => new ObjectId(origin.Id)))
             .ForMember(x => x.CreateDate, opt => opt.MapFrom(origin => DateTime.Now));
            //==========================================================================================================================================
            #endregion




        }
    }
}
