using Grpc.Core;
using ISSA_IdentityService.Protos.Admin;
using ISSA_IdentityService.Core.QueryObject;
using static ISSA_IdentityService.Protos.Admin.AdminService;
using ISSA_IdentityService.Core.Utils;
using ISSA_IdentityService.Core.Models;

namespace ISSA_IdentityService.Services
{
    public class AdminRPCService(Contract.Service.Interface.IAdminService service, ILogger<AdminRPCService> logger) : AdminServiceBase
    {
        public override async Task<GetAdminResponse> GetById(GetQuery request, ServerCallContext context)
        {
            try
            {
                var admin = await service.GetByIdAsync(request.Id);
                var response = new GetAdminResponse();
                if (admin == null)
                {
                    response.StatusCode = (int)StatusCode.NotFound;
                    response.Message = "Admin not found";
                    return response;
                }
                response.Admin = admin;
                response.StatusCode = (int)StatusCode.OK;
                response.Message = "Success";
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                var response = new GetAdminResponse
                {
                    StatusCode = (int)StatusCode.Internal,
                    Message = ex.Message
                };
                return response;
            }
        }

        public override async Task<GetAdminsPagiResponse> Get(GetPagiQuery request, ServerCallContext context)
        {
            try
            {
                var response = new GetAdminsPagiResponse();
                var query = ObjHelper.TryConvertTo<AdminQuery>(request);
                if (query == null)
                {
                    response.StatusCode = (int)StatusCode.InvalidArgument;
                    response.Message = "Error converting query";
                    return response;
                }
                var admins = await service.GetPaginatedAsync(query);
                response.Data = admins;
                response.StatusCode = (int)StatusCode.OK;
                response.Message = "Success";
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                var response = new GetAdminsPagiResponse
                {
                    StatusCode = (int)StatusCode.Internal,
                    Message = ex.Message
                };
                return response;
            }
        }

        public override async Task<CreateAdminResponse> Create(CreateAdminCommand request, ServerCallContext context)
        {
            try
            {
                var response = new CreateAdminResponse();
                var admin = ObjHelper.TryConvertTo<AdminModel>(request);
                if (admin == null)
                {
                    response.StatusCode = (int)StatusCode.InvalidArgument;
                    response.Message = "Error converting admin";
                    return response;
                }
                var result = await service.CreateAsync(admin);
                response.Id = result;
                response.StatusCode = (int)StatusCode.OK;
                response.Message = "Success";
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                var response = new CreateAdminResponse
                {
                    StatusCode = (int)StatusCode.Internal,
                    Message = ex.Message
                };
                return response;
            }
        }

        public override async Task<UpdateAdminResponse> Update(UpdateAdminCommand request, ServerCallContext context)
        {
            try
            {
                var response = new UpdateAdminResponse();
                var admin = ObjHelper.TryConvertTo<AdminModel>(request);
                if (admin == null)
                {
                    response.StatusCode = (int)StatusCode.InvalidArgument;
                    response.Message = "Error converting admin";
                    return response;
                }
                var result = await service.UpdateAsync(request.Id, admin);
                response.AffectedRows = result;
                response.StatusCode = (int)StatusCode.OK;
                response.Message = "Success";
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                var response = new UpdateAdminResponse
                {
                    StatusCode = (int)StatusCode.Internal,
                    Message = ex.Message
                };
                return response;
            }
        }

        public override async Task<DeleteAdminResponse> Delete(DeleteAdminCommand request, ServerCallContext context)
        {
            try
            {
                var response = new DeleteAdminResponse();
                var result = await service.DeleteAsync(request.Id);
                if(result == 0)
                {
                    response.StatusCode = (int)StatusCode.NotFound;
                    response.Message = "Admin not found";
                    return response;
                }
                response.AffectedRows = result;
                response.StatusCode = (int)StatusCode.OK;
                response.Message = "Success";
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                var response = new DeleteAdminResponse
                {
                    StatusCode = (int)StatusCode.Internal,
                    Message = ex.Message
                };
                return response;
            }
        }
    }
}
