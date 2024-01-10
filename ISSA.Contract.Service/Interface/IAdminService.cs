using ISSA.Contract.Repository.Entity;
using ISSA.Contract.Service.BaseServiceInterface;
using ISSA.Core.Models;
using ISSA.Core.QueryObject;

namespace ISSA.Contract.Service.Interface
{
    public interface IAdminService : 
        ICreateAble<AdminModel, string>,
        IGetAble<Admin, string, AdminQuery>,
        IUpdateAble<AdminModel, string>,
        IDeleteAble<string>
    {

    }
}
