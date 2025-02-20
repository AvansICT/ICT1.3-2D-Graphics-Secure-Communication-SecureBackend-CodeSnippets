using System.ComponentModel.DataAnnotations;

namespace ProjectMap.WebApi.Models;

public class WeatherForecast
{
    public Guid Id { get; set; }

    public DateOnly Date { get; set; }

    [Range(-75, 75)]
    public int TemperatureC { get; set; }

    [Required]
    public string? Summary { get; set; }
}

