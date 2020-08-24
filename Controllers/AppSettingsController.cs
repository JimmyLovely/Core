using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

using NetCore.Models;

namespace NetCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppSettingsController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IOptions<AppSetting> _appSetting;
        private readonly WebService _webService;

        public AppSettingsController(
            IConfiguration configuration,
            IOptions<AppSetting> appSetting,
            IOptions<WebService> webService)
        {
            _configuration = configuration;
            _appSetting = appSetting;
            _webService = webService.Value;
        }

        [HttpGet]
        [Route("Sample")]
        public IActionResult Sample()
        {
            var URLValue1 = _configuration.GetSection("WebService").GetSection("URL").Value;
            var URLValue2 = _configuration.GetValue<string>("WebService:URL");
            var URLValue3 = _configuration["WebService:URL"];

            Console.WriteLine(URLValue1);
            Console.WriteLine(URLValue2);
            Console.WriteLine(URLValue3);

            var Logging1 = _configuration.GetValue<string>("Logging:LogLevel:Microsoft.Hosting.Lifetime");
            var Logging2 = _configuration["Logging:LogLevel:Microsoft.Hosting.Lifetime"];
            Console.WriteLine(Logging1);
            Console.WriteLine(Logging2);

            var Level1 = _configuration.GetValue<string>("Level:Level1:Level2:Level3");
            var Level2 = _configuration["Level:Level1:Level2:Level3"];
            Console.WriteLine(Logging1);
            Console.WriteLine(Level2);

            return Ok();
        }

        [HttpGet]
        [Route("sample-with-di")]
        public IActionResult SampleWithDI()
        {
            Console.WriteLine(_appSetting.Value.logging.logLevel.Default);
            Console.WriteLine(_webService.URL);
            return Ok();
        }
    }
}