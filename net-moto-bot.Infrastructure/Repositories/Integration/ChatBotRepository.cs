using net_moto_bot.Domain.Interfaces.Integration;
using System.Text.Json;

namespace net_moto_bot.Infrastructure.Repositories.Integration;

public class ChatBotRepository(
    HttpClient _httpClient) : IChatBotRepository
{
    public async Task<string> SendUserQueryAsync(string userQuery)
    {
        Dictionary<string, object?> dict = [];

        dict.Add("human_query", userQuery);
        dict.Add("additionalProp1", new { });

        string content = JsonSerializer.Serialize(dict);
        using HttpClient _httpClient = new();
        _httpClient.BaseAddress = new Uri("http://134.122.114.162:8000");
        using HttpRequestMessage requestMessage = new(HttpMethod.Post, $"/api/v1/user-query-html")
        {
            Content = new StringContent(content, new System.Net.Http.Headers.MediaTypeHeaderValue("application/json")),
        };
        using HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage);
        string data = await responseMessage.Content.ReadAsStringAsync();
        return data;
    }
}
