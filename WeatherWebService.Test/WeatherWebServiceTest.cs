using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherWebService.Controllers;
using WeatherWebService.Repository;

namespace WeatherWebService.Test
{
    public class WeatherWebServiceTest
    {
        WeatherServiceController _controller;
        WeatherServiceRepository _service;
        public WeatherWebServiceTest() 
        {
            HttpClient httpClient = new HttpClient();
            IConfiguration configuration;
            _controller = new WeatherServiceController();

            _service = new WeatherServiceRepository(httpClient,configuration);


        }
    }
}
