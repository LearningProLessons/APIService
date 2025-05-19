 

namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;



[ApiController]
[Route("[controller]")]
public sealed class WeatherForecastController : ControllerBase
{
    private static readonly List<WeatherForecast> _forecasts = new();
    private static int _nextId = 1;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<WeatherForecast>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        return Ok(_forecasts);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(WeatherForecast), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(int id)
    {
        var forecast = _forecasts.FirstOrDefault(x => x.Id == id);
        if (forecast is null)
            return NotFound();

        return Ok(forecast);
    }

    [HttpPost]
    [ProducesResponseType(typeof(WeatherForecast), StatusCodes.Status201Created)]
    public IActionResult Create([FromBody] CreateWeatherForecastDto dto)
    {
        var forecast = new WeatherForecast
        {
            Id = _nextId++,
            Date = dto.Date,
            TemperatureC = dto.TemperatureC,
            Summary = dto.Summary
        };

        _forecasts.Add(forecast);

        return CreatedAtAction(nameof(GetById), new { id = forecast.Id }, forecast);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Update(int id, [FromBody] UpdateWeatherForecastDto dto)
    {
        var forecast = _forecasts.FirstOrDefault(x => x.Id == id);
        if (forecast is null)
            return NotFound();

        forecast.Date = dto.Date;
        forecast.TemperatureC = dto.TemperatureC;
        forecast.Summary = dto.Summary;

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        var forecast = _forecasts.FirstOrDefault(x => x.Id == id);
        if (forecast is null)
            return NotFound();

        _forecasts.Remove(forecast);
        return NoContent();
    }
}


 

public sealed record WeatherForecast
{
    public int Id { get; init; }
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public string Summary { get; set; }
}

public sealed record CreateWeatherForecastDto
{
    public DateOnly Date { get; init; }
    public int TemperatureC { get; init; }
    public string Summary { get; init; }
}

public sealed record UpdateWeatherForecastDto
{
    public DateOnly Date { get; init; }
    public int TemperatureC { get; init; }
    public string Summary { get; init; }
}

