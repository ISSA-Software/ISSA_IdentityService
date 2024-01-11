using AutoMapper;
using ISSA_IdentityService.Contract.Repository.Entity;
using ISSA_IdentityService.Core.Models;

namespace ISSA_IdentityService.Mapper
{
    public class MentorMapperProfile : Profile
    {
        public MentorMapperProfile()
        {
            CreateMap<MentorModel, Mentor>().ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();
        }
    }
}
