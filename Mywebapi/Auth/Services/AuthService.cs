using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Mywebapi.Auth.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly string _loginUrl;

        public AuthService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _loginUrl = configuration["ApiSettings:LoginUrl"]
                        ?? throw new ArgumentNullException("Login URL is missing in appsettings");
        }

        public async Task<string?> LoginAsync(string username, string password)
        {
            var loginData = new
            {
                username = username,
                password = password
            };

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(loginData),
                Encoding.UTF8,
                "application/json"
            );

            // Gửi yêu cầu đăng nhập
            var response = await _httpClient.PostAsync(_loginUrl, jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Login failed: {response.StatusCode}");
                return null;
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Response Body: " + responseBody);

            try
            {
                using JsonDocument doc = JsonDocument.Parse(responseBody);
                if (doc.RootElement.TryGetProperty("access_token", out JsonElement tokenElement))
                {
                    string accessToken = tokenElement.GetString() ?? string.Empty;
                    Console.WriteLine("Access Token: " + accessToken);
                    return accessToken;
                }
                else
                {
                    Console.WriteLine("Error: 'access_token' not found in response.");
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine("JSON Parse Error: " + ex.Message);
            }

            return null;
        }
    }
}
