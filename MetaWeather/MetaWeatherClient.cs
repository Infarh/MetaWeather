using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using MetaWeather.Models;

namespace MetaWeather
{
    public class MetaWeatherClient
    {
        private readonly HttpClient _Client;

        public MetaWeatherClient(HttpClient Client) => _Client = Client;

        //private static readonly JsonSerializerOptions __JsonOptions = new()
        //{
        //    Converters =
        //    {
        //        new JsonStringEnumConverter(),
        //        new JsonCoordinateConverter()
        //    }
        //};

        public async Task<WeatherLocation[]> GetLocationByName(string Name, CancellationToken Cancel = default)
        {
            return await _Client
               .GetFromJsonAsync<WeatherLocation[]>($"/api/location/search/?query={Name}", /*__JsonOptions,*/ Cancel)
               .ConfigureAwait(false);
        }
    }
}
