using AutoMapper;
using ISSA_IdentityService.Contract.Repository.Entity;
using ISSA_IdentityService.Core.Models;

namespace ISSA_IdentityService.Mapper
{
    public class AdminMapperProfile : Profile
    {
        public AdminMapperProfile()
        {
            CreateMap<AdminModel, Admin>().ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();
        }
    }
}
