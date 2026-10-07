using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;
internal class Program
{
    static async Task Main(string[] args)
    {
        string apiKey = "<Your Api  Key Here>";
        string apiUrl = "https://api.openai.com/v1/chat/completions";
        using HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        var requestBody = new
        {
            model = "gpt-5-mini",
            messages = new[] {
        new {
          role = "user",
          content = "What is ASP.NET Core?"
        }
      }
        };
        string json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync(apiUrl, content);
        string result = await response.Content.ReadAsStringAsync();
        JsonDocument document = JsonDocument.Parse(result);
        string? answer = document.RootElement
      .GetProperty("choices")[0].GetProperty("message")
      .GetProperty("content").GetString();
        Console.WriteLine(answer);
        Console.ReadLine();
    }
}