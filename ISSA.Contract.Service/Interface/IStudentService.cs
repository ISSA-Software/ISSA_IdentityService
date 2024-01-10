using ISSA.Contract.Repository.Entity;
using ISSA.Contract.Service.BaseServiceInterface;
using ISSA.Core.Models;
using ISSA.Core.QueryObject;

namespace ISSA.Contract.Service.Interface
{
    public interface IStudentService :
        ICreateAble<StudentModel, string>,
        IGetAble<Student, string, StudentQuery>,
        IUpdateAble<StudentModel, string>,
        IDeleteAble<string>
    {

    }
}
