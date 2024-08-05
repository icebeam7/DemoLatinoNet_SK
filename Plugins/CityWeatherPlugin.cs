using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace DemoLatinoNet_SK.Plugins
{
    // Native function: native code with custom logic
    public class CityWeatherPlugin
    {
        HttpClient client = new HttpClient();

        [KernelFunction]
        [Description("Describes the current weather of a city in JSON format")]
        public async Task<string> GetWeather(
            [Description("The name of the city ")] string city)
        {
            var key = "52585853b68fa6c2decd041061a86019";
            var query = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={key}";

            var result = await client.GetAsync(query);

            if (result.IsSuccessStatusCode)
                return await result.Content.ReadAsStringAsync();

            return "Error, the weather of the city is not available at this moment";
        }
    }
}
