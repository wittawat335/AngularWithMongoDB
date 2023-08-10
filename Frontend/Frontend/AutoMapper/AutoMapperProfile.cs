using AutoMapper;
using Frontend.DTOs;
using Frontend.Models.ViewModel.Login;

namespace Frontend.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<SessionDTO, LoginResponse>().ReverseMap();
        }
    }
}
