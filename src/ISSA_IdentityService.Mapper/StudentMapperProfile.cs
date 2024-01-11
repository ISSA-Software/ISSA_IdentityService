using AutoMapper;
using ISSA_IdentityService.Contract.Repository.Entity;
using ISSA_IdentityService.Core.Models;

namespace ISSA_IdentityService.Mapper
{
    public class StudentMapperProfile : Profile
    {
        public StudentMapperProfile()
        {
            CreateMap<StudentModel, Student>().ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();
        }
    }
}
