using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MetaWeather.Service
{
    internal class JsonCoordinateConverter : JsonConverter<(double Latitude, double Longitude)>
    {
        public override (double Latitude, double Longitude) Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
            reader.GetString() is not { Length: >= 3 } str
            || str.Split(',') is not { Length: 2 } components
            || !double.TryParse(components[0], NumberStyles.Any, CultureInfo.InvariantCulture, out var lat)
            || !double.TryParse(components[1], NumberStyles.Any, CultureInfo.InvariantCulture, out var lon)
                ? (double.NaN, double.NaN)
                : (lat, lon);

        public override void Write(Utf8JsonWriter writer, (double Latitude, double Longitude) value, JsonSerializerOptions options) => writer
           .WriteStringValue($"{value.Latitude.ToString(CultureInfo.InvariantCulture)},{value.Longitude.ToString(CultureInfo.InvariantCulture)}");
    }
}