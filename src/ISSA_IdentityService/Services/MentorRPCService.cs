using AutoMapper;
using Grpc.Core;
using ISSA_IdentityService.Core.Models;
using ISSA_IdentityService.Core.QueryObject;
using ISSA_IdentityService.Core.Utils;
using ISSA_IdentityService.Protos.Mentor;
using static ISSA_IdentityService.Protos.Mentor.MentorService;

namespace ISSA_IdentityService.Services
{
    public class MentorRPCService(Contract.Service.Interface.IMentorService service, ILogger<AdminRPCService> logger) : MentorServiceBase
    {
        public override async Task<CreateMentorResponse> Create(CreateMentorCommand request, ServerCallContext context)
        {
            try
            {
                var response = new CreateMentorResponse();
                var mentor = ObjHelper.TryConvertTo<MentorModel>(request);
                if (mentor == null)
                {
                    response.StatusCode = (int)StatusCode.InvalidArgument;
                    response.Message = "Error converting mentor";
                    return response;
                }
                var result = await service.CreateAsync(mentor);
                if (result == null)
                {
                    response.StatusCode = (int)StatusCode.Internal;
                    response.Message = "Error creating mentor";
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
                var response = new CreateMentorResponse
                {
                    StatusCode = (int)StatusCode.Internal,
                    Message = ex.Message
                };
                return response;
            }
        }

        public override async Task<DeleteMentorResponse> Delete(DeleteMentorCommand request, ServerCallContext context)
        {
            //return base.Delete(request, context);
            try
            {
                var response = new DeleteMentorResponse();
                var result = await service.DeleteAsync(request.Id);
                if (result == 0)
                {
                    response.StatusCode = (int)StatusCode.Internal;
                    response.Message = "Error deleting mentor";
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
                var response = new DeleteMentorResponse
                {
                    StatusCode = (int)StatusCode.Internal,
                    Message = ex.Message
                };
                return response;
            }
        }

        public override async Task<GetMentorResponse> GetById(GetQuery request, ServerCallContext context)
        {
            try
            {
                var response = new GetMentorResponse();
                var result = await service.GetByIdAsync(request.Id);
                if (result == null)
                {
                    response.StatusCode = (int)StatusCode.NotFound;
                    response.Message = "Mentor not found";
                    return response;
                }
                response.StatusCode = (int)StatusCode.OK;
                response.Message = "Success";
                response.Mentor = result;
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                var response = new GetMentorResponse
                {
                    StatusCode = (int)StatusCode.Internal,
                    Message = ex.Message
                };
                return response;
            }
        }

        public override async Task<GetMentorsPagiResponse> Get(GetPagiQuery request, ServerCallContext context)
        {
            try
            {
                var response = new GetMentorsPagiResponse();
                var query = ObjHelper.TryConvertTo<MentorQuery>(request);
                if (query == null)
                {
                    response.StatusCode = (int)StatusCode.InvalidArgument;
                    response.Message = "Error converting query";
                    return response;
                }
                var mentors = await service.GetPaginatedAsync(query);
                response.Data = mentors;
                response.StatusCode = (int)StatusCode.OK;
                response.Message = "Success";
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                var response = new GetMentorsPagiResponse
                {
                    StatusCode = (int)StatusCode.Internal,
                    Message = ex.Message
                };
                return response;
            }
        }

        public override async Task<UpdateMentorResponse> Update(UpdateMentorCommand request, ServerCallContext context)
        {
            try
            {
                var response = new UpdateMentorResponse();
                var mentor = ObjHelper.TryConvertTo<MentorModel>(request);
                if (mentor == null)
                {
                    response.StatusCode = (int)StatusCode.InvalidArgument;
                    response.Message = "Error converting mentor";
                    return response;
                }
                var result = await service.UpdateAsync(request.Id, mentor);
                if (result == 0)
                {
                    response.StatusCode = (int)StatusCode.Internal;
                    response.Message = "Error updating mentor";
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
                var response = new UpdateMentorResponse
                {
                    StatusCode = (int)StatusCode.Internal,
                    Message = ex.Message
                };
                return response;
            }
        }
    }
}