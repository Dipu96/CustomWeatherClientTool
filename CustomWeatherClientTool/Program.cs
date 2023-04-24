
using CustomWeatherClientTool.Models;
using System.Net.Http.Headers;
using JsonSerializer = System.Text.Json.JsonSerializer;

public class Program
{
    private static readonly string baseUrl = "https://api.open-meteo.com";
    private static readonly string WeatherApiUrl = "/v1/forecast";

    private static void Main(string[] args)
    {
        Console.WriteLine("Enter a city name");
        string city = Console.ReadLine().ToString();

        string text = File.ReadAllText(@".\Data\Cities.json");
        var getCityList = JsonSerializer.Deserialize<List<City>>(text);

        GetLatitude(city, getCityList, out decimal latitute, out decimal longitude);

        CityResponse cityWeatherReport = GetCityResponse(latitute, longitude);

        if (cityWeatherReport != null)
        {
            Console.Write($"Weather report of {city} is as follows: \n");
            Console.WriteLine($"Temperature: {cityWeatherReport.current_weather.temperature} \n" +
                $"Wind Speed: {cityWeatherReport.current_weather.windspeed} \n" +
                $"Wind Direction: {cityWeatherReport.current_weather.winddirection} \n" +
                $"Weather Code: {cityWeatherReport.current_weather.weathercode} \n" +
                $"Is Day: {cityWeatherReport.current_weather.is_day} \n" +
                $"Time: {cityWeatherReport.current_weather.time}");
        }
        else Console.WriteLine("Error in fetching City Weather Report");
    }

    private static void GetLatitude(string city, List<City> cityName, out decimal latitude, out decimal longitude)
    {
        decimal lat = 0;
        decimal lng = 0;

        foreach (var item in cityName)
        {
            if (city == item.city)
            {
                lat = Convert.ToDecimal(item.lat);
                lng = Convert.ToDecimal(item.lng);
                break;
            }
        }
        latitude = lat;
        longitude = lng;
    }

    public static CityResponse GetCityResponse(decimal latitute, decimal longitude)
    {
        using HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(baseUrl);
        var contentType = new MediaTypeWithQualityHeaderValue("application/json");
        client.DefaultRequestHeaders.Accept.Add(contentType);

        CityResponse cityResponse = new CityResponse();

        //Calling the API Endpoint
        var response = client.GetAsync($"{WeatherApiUrl}?latitude={latitute}&longitude={longitude}&current_weather=true");
        response.Wait();
        if (response.IsCompleted)
        {
            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                string stringData = result.Content.ReadAsStringAsync().Result;
                cityResponse = JsonSerializer.Deserialize<CityResponse>(stringData);
            }
            else { cityResponse = new CityResponse(); }
        }

        return cityResponse;

    }
}