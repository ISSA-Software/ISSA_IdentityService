﻿using ISSA.Core.Utils;
using ISSA.Core.ValueObject;

namespace ISSA.Core.Models.Common;
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
