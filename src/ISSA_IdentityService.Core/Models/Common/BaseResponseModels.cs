using ISSA_IdentityService.Core.Utils;
using ISSA_IdentityService.Core.ValueObject;

namespace ISSA_IdentityService.Core.Models.Common;
public class BaseResponseModel<T>
{
    public T? Data { get; set; }
    public object? AdditionalData { get; set; }
    public Dictionary<string, EnumObject[]> EnumData { get; set; } = CoreHelper.GetAllEnums();
    public string? Message { get; set; }
    public int StatusCode { get; set; }

    public BaseResponseModel(int statusCode, T? data, object? additionalData = null)
    {
        StatusCode = statusCode;
        Data = data;
        AdditionalData = additionalData;
    }

    public BaseResponseModel(int statusCode, string? message)
    {
        StatusCode = statusCode;
        Message = message;
    }
}
