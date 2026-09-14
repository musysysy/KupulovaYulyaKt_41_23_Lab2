using Microsoft.AspNetCore.Mvc;

namespace KupulovaYulyaKt_41_23.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        _logger.LogInformation("Вызван метод Get у WeatherForecastController");

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    // POST-метод из ЛР2: добавляет в выдачу ещё одну "погоду" с описанием из запроса
    [HttpPost(Name = "AddWeatherForecast")]
    public IEnumerable<WeatherForecast> Post([FromQuery] string summary)
    {
        _logger.LogInformation("Вызван метод Post у WeatherForecastController, summary = {Summary}", summary);

        var forecasts = Get().ToList();
        forecasts.Add(new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = summary
        });

        return forecasts;
    }
}
