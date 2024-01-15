using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using ISSA_IdentityService.Protos.Admin;
using static ISSA_IdentityService.Protos.Admin.AdminService;

namespace ISSA_IdentityService.Services
{
    public class AdminRPCService : AdminServiceBase
    {
        public override Task<GetResponse> GetById(GetQuery request, ServerCallContext context)
        {
            return Task.FromResult(new GetResponse()
            {
                Admin = new()
                {
                    ApplicationUser = new()
                    {
                        Id = request.Id,
                        Username = "test",
                        Email = "",
                        PhoneNumber = "",
                      
                    },
                    CreatedTime = DateTime.UtcNow.ToTimestamp(),
                    IsDeleted = false,
                    Id = request.Id,
                    ApplicationUserId = request.Id,
                }

            });
        }

      
    }
}
