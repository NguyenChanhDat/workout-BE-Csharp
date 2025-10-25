using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RoleEnum
{
    Admin,
    User,
    Guest
}

