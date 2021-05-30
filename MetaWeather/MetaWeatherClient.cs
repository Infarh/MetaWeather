using System;
using System.Globalization;
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

        public async Task<WeatherLocation[]> GetLocation(string Name, CancellationToken Cancel = default)
        {
            return await _Client
               .GetFromJsonAsync<WeatherLocation[]>($"/api/location/search/?query={Name}", /*__JsonOptions,*/ Cancel)
               .ConfigureAwait(false);
        }

        public async Task<WeatherLocation[]> GetLocation((double Latitude, double Longitude) Location, CancellationToken Cancel = default)
        {
            return await _Client
               .GetFromJsonAsync<WeatherLocation[]>($"/api/location/search/?lattlong={Location.Latitude.ToString(CultureInfo.InvariantCulture)},{Location.Longitude.ToString(CultureInfo.InvariantCulture)}", Cancel)
               .ConfigureAwait(false);
        }

        public async Task<LocationInfo> GetInfo(int WoeId, CancellationToken Cancel = default)
        {
            return await _Client.GetFromJsonAsync<LocationInfo>($"/api/location/{WoeId}", Cancel).ConfigureAwait(false);
        }

        public Task<LocationInfo> GetInfo(WeatherLocation Location, CancellationToken Cancel = default) =>
            GetInfo(Location.Id, Cancel);

        public async Task<WeatherInfo[]> GetWeather(int WoeId, DateTime Time, CancellationToken Cancel = default)
        {
            return await _Client
               .GetFromJsonAsync<WeatherInfo[]>($"/api/location/{WoeId}/{Time:yyyy}/{Time:MM}/{Time:dd}/", Cancel)
               .ConfigureAwait(false);
        }
    }
}
