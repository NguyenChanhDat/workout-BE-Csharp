using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MembershipTierEnum
{
    Basic,
    Advance,
    High
}
