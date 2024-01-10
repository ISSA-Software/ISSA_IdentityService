using AutoMapper;
using ISSA.Contract.Repository.Entity;
using ISSA.Core.Models;

namespace ISSA.Mapper
{
    public class MentorMapperProfile : Profile
    {
        public MentorMapperProfile()
        {
            CreateMap<MentorModel, Mentor>().ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();
        }
    }
}
