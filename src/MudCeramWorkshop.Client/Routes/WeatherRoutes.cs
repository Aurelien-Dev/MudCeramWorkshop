namespace MudCeramWorkshop.Client.MinimalApi;

public static class WeatherRoutes
{
    public static IEndpointConventionBuilder MapPageRoute(this IEndpointRouteBuilder endpoints)
    {
        var apiGroup = endpoints.MapGroup("/api");

        var summaries = new[]
                {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        apiGroup.MapGet("weatherforecast", () =>
        {
            var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
                .ToArray();
            return forecast;
        })
        .WithName("GetWeatherForecast")
        .RequireAuthorization()
        .WithOpenApi();

        return apiGroup;
    }
}

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
