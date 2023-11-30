using AutoMapper;
using HomeMade.Core.Entities;
using HomeMade.Core.ViewModels;

namespace HomeMade.Infrastructure.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, UserModel>();
            CreateMap<UserModel, ApplicationUser>();
        }
    }
}
