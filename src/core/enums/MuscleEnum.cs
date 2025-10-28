using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MuscleEnum
{
    Quads,
    Hamstring,
    Calves,
    Glutes,
    Back,
    Chest,
    Shoulders,
    Triceps,
    Biceps,
    Traps,
}