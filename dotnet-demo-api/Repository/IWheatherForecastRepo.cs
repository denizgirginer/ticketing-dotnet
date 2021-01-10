using API.Models;
using API.Repository.Abstract;

namespace API.Repository
{
    public interface IWeatherForecastRepo : IRepository<WeatherForecast, string>
    {

    }
}
