using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Security;
using System.Threading.Tasks;

namespace StudentApiConsoleClient
{
    class Program
    {
        // ==========================
        // Configuration
        // ==========================

        // Base URL of the secured Student API
        private const string BaseUrl = "http://localhost:5215/";

        // Test credentials that already exist in the API
        private const string Email = "ali@gmail.com";
        private const string Password = "Ali123";

        static async Task Main(string[] args)
        {
            Console.WriteLine("=== Student API Console Client (JWT) ===");
            Console.WriteLine();

            // Create an HttpClient configured for local HTTPS development
            using var http = CreateHttpClientForLocalDev(BaseUrl);

            // Step 1: Login and retrieve JWT
            var token = await LoginAndGetTokenAsync(http, Email, Password);

            if (string.IsNullOrWhiteSpace(token))
            {
                Console.WriteLine("Login failed.");
                return;
            }

            Console.WriteLine("Login succeeded.");
            Console.WriteLine($"Token (first 30 chars): {token[..30]}...");
            Console.WriteLine();

            // Step 2: Call secured endpoint WITHOUT token
            Console.WriteLine("Calling GET /api/Students/All WITHOUT token...");
            await CallGetAllStudentsAsync(http, null);
            Console.WriteLine();

            // Step 3: Call secured endpoint WITH token
            Console.WriteLine("Calling GET /api/Students/All WITH token...");
            await CallGetAllStudentsAsync(http, token);
            Console.WriteLine();
        }

        // ==========================
        // Helper Methods
        // ==========================

        static HttpClient CreateHttpClientForLocalDev(string baseUrl)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    (message, certificate, chain, sslErrors) =>
                        sslErrors == SslPolicyErrors.None ||
                        sslErrors == SslPolicyErrors.RemoteCertificateChainErrors
            };

            return new HttpClient(handler)
            {
                BaseAddress = new Uri(baseUrl)
            };
        }

        static async Task<string> LoginAndGetTokenAsync(HttpClient http, string email, string password)
        {
            var request = new LoginRequest
            {
                Email = email,
                Password = password
            };

            var response = await http.PostAsJsonAsync("/api/Auth/login", request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Invalid credentials.");
                return "";
            }

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Login failed: {response.StatusCode}");
                return "";
            }

            var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
            return tokenResponse?.Token ?? "";
        }

        static async Task CallGetAllStudentsAsync(HttpClient http, string? token)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, "api/Students/All");

            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await http.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("401 Unauthorized");
                return;
            }

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Request failed: {response.StatusCode}");
                return;
            }

            var students = await response.Content.ReadFromJsonAsync<List<StudentDto>>();
            if (students == null || students.Count == 0)
            {
                Console.WriteLine("No students found.");
                return;
            }
            Console.WriteLine($"{students.Count} students returned:");
            foreach (var s in students)
            {
                Console.WriteLine($"- {s.Name} (Age: {s.Age}, Grade: {s.Grade})");
            }
        }
    }

    // ==========================
    // DTOs
    // ==========================

    class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    class TokenResponse
    {
        public string Token { get; set; } = string.Empty;
    }

    class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public int Grade { get; set; }
    }
}