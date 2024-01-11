using ISSA_IdentityService.Contract.Repository.Entity;
using ISSA_IdentityService.Contract.Service.BaseServiceInterface;
using ISSA_IdentityService.Core.Models;
using ISSA_IdentityService.Core.QueryObject;

namespace ISSA_IdentityService.Contract.Service.Interface
{
    public interface IAdminService : 
        ICreateAble<AdminModel, string>,
        IGetAble<Admin, string, AdminQuery>,
        IUpdateAble<AdminModel, string>,
        IDeleteAble<string>
    {

    }
}
