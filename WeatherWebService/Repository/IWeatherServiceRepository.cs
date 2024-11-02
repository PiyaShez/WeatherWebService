namespace WeatherWebService.Repository
{
    public interface IWeatherServiceRepository
    {
        public Task<string> GetWeatherByCity(string cityId);
    }
}
