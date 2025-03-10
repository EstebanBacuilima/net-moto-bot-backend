
using net_moto_bot.Domain.Interfaces.Integration;
using net_moto_bot.Domain.Models;
using System.Text.Json;

namespace net_moto_bot.Infrastructure.Repositories.Integration;

public class EmailRepository(
    HttpClient _httpClient
) : IEmailRepository
{
    public async Task<string> SendEmailAsync(EmailModel email)
    {
        Dictionary<string, object?> dict = [];

        dict.Add("to", email.To);
        dict.Add("subject", email.Subject);
        dict.Add("message", email.Message);
        dict.Add("additionalProp1", new { });

        string content = JsonSerializer.Serialize(dict);
        using HttpClient _httpClient = new();

        _httpClient.BaseAddress = new Uri("http://134.122.114.162:8000");
        using HttpRequestMessage requestMessage = new(HttpMethod.Post, $"/api/v1/send-email")

        {
            Content = new StringContent(content, new System.Net.Http.Headers.MediaTypeHeaderValue("application/json")),
        };
        using HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage);
        string data = await responseMessage.Content.ReadAsStringAsync();
        return data;    
    }
}