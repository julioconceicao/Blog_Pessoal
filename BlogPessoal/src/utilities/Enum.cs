using System.Text.Json.Serialization;

namespace BlogPessoal.src.utilities
{
   [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserType
    {
        USER,
        ADMIN
    }
}
