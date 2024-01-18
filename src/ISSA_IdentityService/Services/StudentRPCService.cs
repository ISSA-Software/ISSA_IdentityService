using AutoMapper;
using Grpc.Core;
using ISSA_IdentityService.Core.Models;
using ISSA_IdentityService.Core.QueryObject;
using ISSA_IdentityService.Core.Utils;
using ISSA_IdentityService.Protos.Student;
using static ISSA_IdentityService.Protos.Student.StudentService;

namespace ISSA_IdentityService.Services
{
    public class StudentRPCService(Contract.Service.Interface.IStudentService service, ILogger<AdminRPCService> logger) : StudentServiceBase
    {
        public override async Task<CreateStudentResponse> Create(CreateStudentCommand request, ServerCallContext context)
        {
            try
            {
                var response = new CreateStudentResponse();
                var student = ObjHelper.TryConvertTo<StudentModel>(request);
                if (student == null)
                {
                    response.StatusCode = (int)StatusCode.InvalidArgument;
                    response.Message = "Error converting student";
                    return response;
                }
                var result = await service.CreateAsync(student);
                if (result == null)
                {
                    response.StatusCode = (int)StatusCode.Internal;
                    response.Message = "Error creating student";
                    return response;
                }
                response.StatusCode = (int)StatusCode.OK;
                response.Message = "Success";
                response.Id = result;
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                var response = new CreateStudentResponse
                {
                    StatusCode = (int)StatusCode.Internal,
                    Message = ex.Message
                };
                return response;
            }
        }

        public override async Task<DeleteStudentResponse> Delete(DeleteStudentCommand request, ServerCallContext context)
        {
            try
            {
                var response = new DeleteStudentResponse();
                var result = await service.DeleteAsync(request.Id);
                if (result == 0)
                {
                    response.StatusCode = (int)StatusCode.Internal;
                    response.Message = "Error deleting student";
                    return response;
                }
                response.StatusCode = (int)StatusCode.OK;
                response.Message = "Success";
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                var response = new DeleteStudentResponse
                {
                    StatusCode = (int)StatusCode.Internal,
                    Message = ex.Message
                };
                return response;
            }
        }

        public override async Task<GetStudentResponse> GetById(GetQuery request, ServerCallContext context)
        {
            try
            {
                var response = new GetStudentResponse();
                var result = await service.GetByIdAsync(request.Id);
                if (result == null)
                {
                    response.StatusCode = (int)StatusCode.NotFound;
                    response.Message = "Student not found";
                    return response;
                }
                response.StatusCode = (int)StatusCode.OK;
                response.Message = "Success";
                response.Student = result;
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                var response = new GetStudentResponse
                {
                    StatusCode = (int)StatusCode.Internal,
                    Message = ex.Message
                };
                return response;
            }
        }

        public override async Task<GetStudentsPagiResponse> Get(GetPagiQuery request, ServerCallContext context)
        {
            try
            {
                var response = new GetStudentsPagiResponse();
                var query = ObjHelper.TryConvertTo<StudentQuery>(request);
                if (query == null)
                {
                    response.StatusCode = (int)StatusCode.InvalidArgument;
                    response.Message = "Error converting query";
                    return response;
                }
                var students = await service.GetPaginatedAsync(query);
                response.Data = students;
                response.StatusCode = (int)StatusCode.OK;
                response.Message = "Success";
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                var response = new GetStudentsPagiResponse
                {
                    StatusCode = (int)StatusCode.Internal,
                    Message = ex.Message
                };
                return response;
            }
        }

        public override async Task<UpdateStudentResponse> Update(UpdateStudentCommand request, ServerCallContext context)
        {
           try
            {
                var response = new UpdateStudentResponse();
                var student = ObjHelper.TryConvertTo<StudentModel>(request);
                if (student == null)
                {
                    response.StatusCode = (int)StatusCode.InvalidArgument;
                    response.Message = "Error converting student";
                    return response;
                }
                var result = await service.UpdateAsync(request.Id, student);
                if (result == 0)
                {
                    response.StatusCode = (int)StatusCode.NotFound;
                    response.Message = "Student not found";
                    return response;
                }
                response.StatusCode = (int)StatusCode.OK;
                response.Message = "Success";
                response.AffectedRows = result;
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                var response = new UpdateStudentResponse
                {
                    StatusCode = (int)StatusCode.Internal,
                    Message = ex.Message
                };
                return response;
            }
        }
    }
}
