using ISSA.Contract.Repository.Entity;
using ISSA.Contract.Service.BaseServiceInterface;
using ISSA.Core.Models;
using ISSA.Core.QueryObject;

namespace ISSA.Contract.Service.Interface
{
    public interface IMentorService : 
        ICreateAble<MentorModel, string>,
        IGetAble<Mentor, string, MentorQuery>,
        IUpdateAble<MentorModel, string>,
        IDeleteAble<string>
    {

    }
}
