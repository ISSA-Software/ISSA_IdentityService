using AutoMapper;
using ISSA.Contract.Repository.Entity;
using ISSA.Core.Models;

namespace ISSA.Mapper
{
    public class AdminMapperProfile : Profile
    {
        public AdminMapperProfile()
        {
            CreateMap<AdminModel, Admin>().ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();
        }
    }
}
