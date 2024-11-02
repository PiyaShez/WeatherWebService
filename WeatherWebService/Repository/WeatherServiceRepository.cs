namespace WeatherWebService.Repository
{
    public class WeatherServiceRepository : IWeatherServiceRepository
    {
        public HttpClient _httpClient ;
        public IConfiguration _configuration ;
        public WeatherServiceRepository(HttpClient httpClient,IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> GetWeatherByCity(string cityId)
        {
            try
            {
                string? apiKey = _configuration.GetValue<string>("WebApiKey");
                string? endPointURL = _configuration.GetValue<string>("WebAPIEndPoint");
                if (endPointURL == null)
                    throw new Exception("Please provide a webapi end point URL");

                if (apiKey == null)
                    throw new Exception("Please provide API Key");

                var response = await _httpClient.GetAsync(endPointURL+$"id={cityId}&appid={apiKey}");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
