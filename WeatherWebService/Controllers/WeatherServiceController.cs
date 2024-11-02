using Microsoft.AspNetCore.Mvc;
using WeatherWebService.Repository;


namespace WeatherWebService.Controllers
{
    public class WeatherServiceController : ControllerBase
    {
      
       

        private readonly IWeatherServiceRepository _weatherService;
        private readonly IConfiguration _configuration;
        

        public WeatherServiceController(IWeatherServiceRepository weatherService,IConfiguration configuration)
        {
            _weatherService = weatherService;
            _configuration = configuration;
        }

        [HttpPost("uploadFile")]
        public async Task<IActionResult> GetWeatherByCities( IFormFile file)
        {
            try
            {
                string? outputFolder = _configuration.GetValue<string>("WebServiceOutputFolder");

                //Check file is uploaded or not
                if (file == null)
                    return BadRequest("No file uploaded.");

                if (string.IsNullOrWhiteSpace(outputFolder))
                    return BadRequest("Output folder value is not set.");

                //Check file format
                var contentType = file.ContentType;
                if (!contentType.Equals("text/plain", StringComparison.OrdinalIgnoreCase))
                {
                    return BadRequest("Uploaded file is not a text file.");
                }

                // Create output directory if it doesn't exist
                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }

                //read the file
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        var cityId = line.Trim().Split("=")[0].Trim(); //get City Id
                        var cityName = line.Trim().Split("=")[1].Trim(); //get City Name
                        var weatherData = await _weatherService.GetWeatherByCity(cityId);
                        var fileName = Path.Combine(outputFolder, $"{cityName.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd}.json"); //file Name
                        await System.IO.File.WriteAllTextAsync(fileName, weatherData);//generate the file 
                    }
                }

                return Ok("Weather data has been saved.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
