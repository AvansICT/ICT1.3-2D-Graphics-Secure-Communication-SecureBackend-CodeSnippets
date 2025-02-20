using Microsoft.AspNetCore.Mvc;
using ProjectMap.WebApi.Models;
using ProjectMap.WebApi.Repositories;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjectMap.WebApi.Controllers;

[ApiController]
[Route("WeatherForecasts")]
public class WeatherForecastController : ControllerBase
{
    private readonly WeatherForecastRepository _weatherForecastRepository;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(WeatherForecastRepository weatherForecastRepository, ILogger<WeatherForecastController> logger)
    {
        _weatherForecastRepository = weatherForecastRepository;
        _logger = logger;
    }

    [HttpGet(Name = "ReadWeatherForecasts")]
    public async Task<ActionResult<IEnumerable<WeatherForecast>>> Get()
    {
        var weatherForecasts = await _weatherForecastRepository.ReadAsync();
        return Ok(weatherForecasts);
    }

    [HttpGet("{weatherForecastId}", Name = "ReadWeatherForecast")]
    public async Task<ActionResult<WeatherForecast>> Get(Guid weatherForecastId)
    {
        var weatherForeCast = await _weatherForecastRepository.ReadAsync(weatherForecastId);
        if (weatherForeCast == null)
            return NotFound();

        return Ok(weatherForeCast);
    }

    [HttpPost(Name = "CreateWeatherForecast")]
    public async Task<ActionResult> Add(WeatherForecast weatherForecast)
    {
        weatherForecast.Id = Guid.NewGuid();

        var createdWeatherForecast = await _weatherForecastRepository.InsertAsync(weatherForecast);
        return Created();
    }

    [HttpPut("{weatherForecastId}", Name = "UpdateWeatherForecast")]
    public async Task<ActionResult> Update(Guid weatherForecastId, WeatherForecast newWeatherForeCast)
    {
        var existingWeatherForecast = await _weatherForecastRepository.ReadAsync(weatherForecastId);

        if (existingWeatherForecast == null)
            return NotFound();

        await _weatherForecastRepository.UpdateAsync(newWeatherForeCast);

        return Ok(newWeatherForeCast);
    }

    [HttpDelete("{weatherForecastId}", Name = "DeleteWeatherForecastByDate")]
    public async Task<IActionResult> Update(Guid weatherForecastId)
    {
        var existingWeatherForecast = await _weatherForecastRepository.ReadAsync(weatherForecastId);

        if (existingWeatherForecast == null)
            return NotFound();

        await _weatherForecastRepository.DeleteAsync(weatherForecastId);

        return Ok();
    }

}
