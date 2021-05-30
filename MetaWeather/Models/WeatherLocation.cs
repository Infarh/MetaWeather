using System.Text.Json.Serialization;
using MetaWeather.Service;

namespace MetaWeather.Models
{
    public class WeatherLocation
    {
        [JsonPropertyName("woeid")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("location_type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LocationType Type { get; set; }

        [JsonPropertyName("latt_long")]
        [JsonConverter(typeof(JsonCoordinateConverter))]
        public (double Latitude, double Longitude) Location { get; set; }

        [JsonPropertyName("distance")]
        public int Distance { get; set; }

        public override string ToString() => $"{Title}[{Id}]({Type}):{Location} ({Distance})";
    }
}