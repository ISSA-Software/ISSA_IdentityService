using Grpc.Core;
using ISSA_IdentityService.Core.Protos.Student;
using static ISSA_IdentityService.Core.Protos.Student.StudentService;

namespace ISSA_IdentityService.Services
{
    public class StudentRPCService : StudentServiceBase
    {
        public override Task<CreateStudentResponse> Create(CreateStudentCommand request, ServerCallContext context)
        {
            return base.Create(request, context);
        }

        public override Task<DeleteStudentResponse> Delete(DeleteStudentCommand request, ServerCallContext context)
        {
            return base.Delete(request, context);
        }

        public override Task<GetStudentResponse> GetById(GetQuery request, ServerCallContext context)
        {
            return base.GetById(request, context);
        }

        public override Task<GetStudentsPagiResponse> Get(GetPagiQuery request, ServerCallContext context)
        {
            return base.Get(request, context);
        }

        public override Task<UpdateStudentResponse> Update(UpdateStudentCommand request, ServerCallContext context)
        {
            return base.Update(request, context);
        }
    }
}
