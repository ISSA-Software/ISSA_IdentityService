using ISSA_IdentityService.Contract.Repository.Entity;
using ISSA_IdentityService.Contract.Service.BaseServiceInterface;
using ISSA_IdentityService.Core.Models;
using ISSA_IdentityService.Core.QueryObject;

namespace ISSA_IdentityService.Contract.Service.Interface
{
    public interface IStudentService :
        ICreateAble<StudentModel, string>,
        IGetAble<Student, string, StudentQuery>,
        IUpdateAble<StudentModel, string>,
        IDeleteAble<string>
    {
    }
}
