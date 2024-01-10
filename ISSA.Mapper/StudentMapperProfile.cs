using AutoMapper;
using ISSA.Contract.Repository.Entity;
using ISSA.Core.Models;

namespace ISSA.Mapper
{
    public class StudentMapperProfile : Profile
    {
        public StudentMapperProfile()
        {
            CreateMap<StudentModel, Student>().ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();
        }
    }
}
