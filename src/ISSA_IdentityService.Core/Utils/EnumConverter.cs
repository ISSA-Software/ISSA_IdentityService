﻿using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ISSA_IdentityService.Core.Utils;
public class EnumConverter<T> : JsonConverter<T> where T : System.Enum
{
    public override T? ReadJson(JsonReader reader, Type objectType, T? existingValue, bool hasExistingValue, JsonSerializer serializer)
        => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, T? value, JsonSerializer serializer)
    {
        var nameAndValue = new {
            Name = value?.ToString("g"),
            Value = value?.ToString("d"),
            DisplayName = CoreHelper.ToSentenceCase(value?.ToString("g") ?? string.Empty)
        };
        var semiJson = JObject.FromObject(nameAndValue);
        semiJson.WriteTo(writer);
    }
}
