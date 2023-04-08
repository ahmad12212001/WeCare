using Microsoft.AspNetCore.Mvc;
using WeCare.Application.WeatherForecasts.Queries.GetWeatherForecasts;

namespace WeCare.WebUI.Controllers;
public class WeatherForecastController : ApiControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        return await Mediator.Send(new GetWeatherForecastsQuery());
    }
}
