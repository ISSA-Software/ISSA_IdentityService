using Grpc.Core;
using ISSA_IdentityService.Core.Protos.Mentor;
using static ISSA_IdentityService.Core.Protos.Mentor.MentorService;

namespace ISSA_IdentityService.Services
{
    public class MentorRPCService : MentorServiceBase
    {
        public override Task<CreateMentorResponse> Create(CreateMentorCommand request, ServerCallContext context)
        {
            return base.Create(request, context);
        }

        public override Task<DeleteMentorResponse> Delete(DeleteMentorCommand request, ServerCallContext context)
        {
            return base.Delete(request, context);
        }

        public override Task<GetMentorResponse> GetById(GetQuery request, ServerCallContext context)
        {
            return base.GetById(request, context);
        }

        public override Task<GetMentorsPagiResponse> Get(GetPagiQuery request, ServerCallContext context)
        {
            return base.Get(request, context);
        }

        public override Task<UpdateMentorResponse> Update(UpdateMentorCommand request, ServerCallContext context)
        {
            return base.Update(request, context);
        }
    }
}