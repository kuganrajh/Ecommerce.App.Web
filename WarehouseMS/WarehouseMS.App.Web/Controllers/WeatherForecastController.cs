using Microsoft.AspNetCore.Mvc;
using WarehouseMS.App.Infrastructure.Interface;
using WarehouseMS.App.Service.grpc;

namespace WarehouseMS.App.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IPaymentChecker paymentChecker;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IPaymentChecker paymentChecker)
        {
            _logger = logger;
            this.paymentChecker = paymentChecker;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public  async Task<IEnumerable<WeatherForecast>> GetAsync()
        {
            var result =  await paymentChecker.IsOrderPaidAsync("1","1", CancellationToken.None);

            if(result)
            {
                _logger.LogInformation("Order is paid.");
            }
            else
            {
                _logger.LogInformation("Order is not paid.");
            }
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
