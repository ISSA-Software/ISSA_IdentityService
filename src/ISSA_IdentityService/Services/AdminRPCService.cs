using Grpc.Core;
using ISSA_IdentityService.Protos.Admin;
using static ISSA_IdentityService.Protos.Admin.AdminService;

namespace ISSA_IdentityService.Services
{
    public class AdminRPCService : AdminServiceBase
    {
        public override Task<GetResponse> Get(GetQuery request, ServerCallContext context)
        {
            return base.Get(request, context);
        }

        public override Task<GetPagiResponse> GetPagi(GetPagiQuery request, ServerCallContext context)
        {
            return base.GetPagi(request, context);
        }

        public override Task<CreateAdminResponse> Create(CreateAdminCommand request, ServerCallContext context)
        {
            return base.Create(request, context);
        }

        public override Task<UpdateAdminResponse> Update(UpdateAdminCommand request, ServerCallContext context)
        {
            return base.Update(request, context);
        }

        public override Task<DeleteAdminResponse> Delete(DeleteAdminCommand request, ServerCallContext context)
        {
            return base.Delete(request, context);
        }
    }
}
