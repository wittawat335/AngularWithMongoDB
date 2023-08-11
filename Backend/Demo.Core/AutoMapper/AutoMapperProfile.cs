using AutoMapper;
using Demo.Domain.DTOs.Category;
using Demo.Domain.DTOs.Menu;
using Demo.Domain.DTOs.Product;
using Demo.Domain.Models.Collections;
using MongoDB.Bson;

namespace Demo.Core.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Menu, MenuDTO>().ForMember(x => x.Id, opt => opt.MapFrom(origin => origin.Id.ToString()))
                 .ForMember(x => x.IsActive, opt => opt.MapFrom(origin => origin.IsActive == true ? 1 : 0));

            //Input
            CreateMap<MenuDTO, Menu>().ForMember(x => x.Id, opt => opt.MapFrom(origin => new ObjectId(origin.Id)))
                 .ForMember(x => x.IsActive, opt => opt.MapFrom(origin => origin.IsActive == 1 ? true : false));

            CreateMap<MenuInput, Menu>().ForMember(x => x.IsActive, opt => opt.MapFrom(origin => origin.IsActive == 1 ? true : false));

            #region Products
            CreateMap<Products, ProductDTO>() //Output 
                .ForMember(x => x.IsActive, opt => opt.MapFrom(origin => origin.IsActive == true ? 1 : 0))
                .ForMember(x => x.Id, opt => opt.MapFrom(origin => origin.Id.ToString()));

            CreateMap<ProductDTO, Products>() //Input
                .ForMember(x => x.Id, opt => opt.MapFrom(origin => new ObjectId(origin.Id)))
                .ForMember(x => x.CreateDate, opt => opt.MapFrom(origin => DateTime.Now))
                .ForMember(x => x.IsActive, opt => opt.MapFrom(origin => origin.IsActive == "A" ? true : false));

            CreateMap<ProductInput, Products>() //New
                .ForMember(x => x.CreateDate, opt => opt.MapFrom(origin => DateTime.Now));
            CreateMap<ProductDTO, ProductInput>() //New
              .ForMember(x => x.CreateDate, opt => opt.MapFrom(origin => DateTime.Now))
              .ForMember(x => x.IsActive, opt => opt.MapFrom(origin => origin.IsActive == "A" ? true : false));
            #endregion

            CreateMap<Category, CategoryDTO>() //Output 
              .ForMember(x => x.Id, opt => opt.MapFrom(origin => origin.Id.ToString()));

            CreateMap<CategoryDTO, Category>() //Output 
             .ForMember(x => x.Id, opt => opt.MapFrom(origin => new ObjectId(origin.Id)))
             .ForMember(x => x.CreateDate, opt => opt.MapFrom(origin => DateTime.Now));
        }
    }
}
