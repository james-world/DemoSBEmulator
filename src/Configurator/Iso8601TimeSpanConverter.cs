using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;

public class Iso8601TimeSpanConverter : JsonConverter<TimeSpan>
{
    public override TimeSpan Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        var durationString = reader.GetString();
        if (string.IsNullOrEmpty(durationString))
        {
            return TimeSpan.Zero;
        }

        return XmlConvert.ToTimeSpan(durationString);
    }

    public override void Write(
        Utf8JsonWriter writer,
        TimeSpan value,
        JsonSerializerOptions options)
    {
        var durationString = XmlConvert.ToString(value);
        writer.WriteStringValue(durationString);
    }
}
