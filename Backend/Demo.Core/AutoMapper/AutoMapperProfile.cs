using AutoMapper;
using Demo.Domain.DTOs.Product;
using Demo.Domain.Models.Collections;
using MongoDB.Bson;

namespace Demo.Core.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Products
            CreateMap<Products, ProductDTO>() //Output 
                .ForMember(x => x.Id, opt => opt.MapFrom(origin => origin.Id.ToString()));
            CreateMap<ProductDTO, Products>() //Input
                .ForMember(x => x.Id, opt => opt.MapFrom(origin => new ObjectId(origin.Id)))
                .ForMember(x => x.CreateDate, opt => opt.MapFrom(origin => DateTime.Now));
            CreateMap<ProductInput, Products>() //Input
                .ForMember(x => x.CreateDate, opt => opt.MapFrom(origin => DateTime.Now));
            #endregion

        }
    }
}
