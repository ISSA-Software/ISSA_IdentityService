using AutoMapper;
using Grpc.Core;
using ISSA_IdentityService.Core.Models;
using ISSA_IdentityService.Core.Protos.Admin;
using ISSA_IdentityService.Core.QueryObject;
using static ISSA_IdentityService.Core.Protos.Admin.AdminService;

namespace ISSA_IdentityService.Services
{
    public class AdminRPCService(Contract.Service.Interface.IAdminService service, IMapper mapper, ILogger<AdminRPCService> logger ) : AdminServiceBase
    {
        public override async Task<GetAdminResponse> GetById(GetQuery request, ServerCallContext context)
        {
            return await base.GetById(request, context);
            //try
            //{
            //    var admin = await service.GetByIdAsync(request.Id);

            //    var response = new GetAdminResponse();
            //    if (admin == null)
            //    {
            //        response.StatusCode = (int)StatusCode.NotFound;
            //        response.Message = "Admin not found";
            //        return response;
            //    }

            //    response.Admin = mapper.Map<Admin>(admin);
            //    response.Admin.ApplicationUser = mapper.Map<Core.Protos.ApplicationUser>(admin.ApplicationUser);
            //    response.StatusCode = (int)StatusCode.OK;
            //    response.Message = "Success";
            //    return response;
            //}
            //catch (Exception ex)
            //{
            //    logger.LogError(ex.Message);
            //    var response = new GetAdminResponse
            //    {
            //        StatusCode = (int)StatusCode.Internal,
            //        Message = ex.Message
            //    };
            //    return response;
            //}

        }

        public override async Task<GetAdminsPagiResponse> Get(GetPagiQuery request, ServerCallContext context)
        {
            return await base.Get(request, context);
            //try
            //{
            //    var admins = await service.GetPaginatedAsync(mapper.Map<AdminQuery>(request));
            //    var response = new GetAdminsPagiResponse
            //    {
            //        Data = mapper.Map<AdminPagi>(admins),
            //        StatusCode = (int)StatusCode.OK,
            //        Message = "Success"
            //    };
            //    return response;
            //}
            //catch (Exception ex)
            //{
            //    var response = new GetAdminsPagiResponse
            //    {
            //        StatusCode = (int)StatusCode.Internal,
            //        Message = ex.Message
            //    };
            //    return response;
            //}
        }

        public override async Task<CreateAdminResponse> Create(CreateAdminCommand request, ServerCallContext context)
        {
            return await base.Create(request, context);
            //try
            //{
            //    var admin = mapper.Map<AdminModel>(request);
            //    var result = await service.CreateAsync(admin);
            //    var response = new CreateAdminResponse
            //    {
            //        Id = result,
            //        StatusCode = (int)StatusCode.OK,
            //        Message = "Success"
            //    };
            //    return response;
            //}
            //catch (Exception ex)
            //{
            //    var response = new CreateAdminResponse
            //    {
            //        StatusCode = (int)StatusCode.Internal,
            //        Message = ex.Message
            //    };
            //    return response;
            //}
        }

        public override async Task<UpdateAdminResponse> Update(UpdateAdminCommand request, ServerCallContext context)
        {
            return await base.Update(request, context);
            //try
            //{
            //    var admin = mapper.Map<AdminModel>(request);
            //    var result = await service.UpdateAsync(request.Id, admin);
            //    var response = new UpdateAdminResponse
            //    {
            //        AffectedRows = result,
            //        StatusCode = (int)StatusCode.OK,
            //        Message = "Success"
            //    };
            //    return response;
            //}
            //catch (Exception ex)
            //{
            //    var response = new UpdateAdminResponse
            //    {
            //        StatusCode = (int)StatusCode.Internal,
            //        Message = ex.Message
            //    };
            //    return response;
            //}
        }

        public override async Task<DeleteAdminResponse> Delete(DeleteAdminCommand request, ServerCallContext context)
        {
            return await base.Delete(request, context);
           //try
           // {
           //     var result = await service.DeleteAsync(request.Id);
           //     var response = new DeleteAdminResponse
           //     {
           //         AffectedRows = result,
           //         StatusCode = (int)StatusCode.OK,
           //         Message = "Success"
           //     };
           //     return response;
           // }
           // catch (Exception ex)
           // {
           //     var response = new DeleteAdminResponse
           //     {
           //         StatusCode = (int)StatusCode.Internal,
           //         Message = ex.Message
           //     };
           //     return response;
           // }
        }
    }
}
