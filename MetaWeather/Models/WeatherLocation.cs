using System.Text.Json.Serialization;

namespace MetaWeather.Models
{
    public class WeatherLocation
        : Location
    {
        [JsonPropertyName("distance")]
        public int Distance { get; set; }

        public override string ToString() => $"{Title}[{Id}]({Type}):{Coordinates} ({Distance})";
    }
}