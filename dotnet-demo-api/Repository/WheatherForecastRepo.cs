using API.Models;
using API.Repository.Abstract;
using API.Utility;
using Microsoft.Extensions.Options;

namespace API.Repository
{
    public class WeatherForecastRepo : MongoDbRepositoryBase<WeatherForecast>, IWeatherForecastRepo
    {
        public WeatherForecastRepo(IOptions<MongoDbSettings> options) : base(options)
        {
        }
    }
}
