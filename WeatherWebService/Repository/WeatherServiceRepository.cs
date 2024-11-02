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
                
                //Check apiKey and endpoint URL is null or not
                if (string.IsNullOrWhiteSpace(endPointURL))
                    throw new Exception("Please provide a webapi end point URL");

                if (string.IsNullOrWhiteSpace(apiKey))
                    throw new Exception("Please provide API Key");
                
                //call the endpoint url
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
