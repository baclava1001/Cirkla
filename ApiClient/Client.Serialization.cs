using System.Text.Json.Serialization;
using System.Text.Json;

namespace Cirkla.ApiClient;

public partial class Client
{
    static partial void UpdateJsonSerializerSettings(System.Text.Json.JsonSerializerOptions settings)
    {
        //settings.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        //settings.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        settings.ReferenceHandler = ReferenceHandler.Preserve;
    }
}