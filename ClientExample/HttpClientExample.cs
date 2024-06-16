using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClientExample
{
    public class HttpClientExample
    {
        public static async Task PerformHttpRequest()
        {
            // API endpoint
            string url = "http://localhost:5000/api/HttpRequestHandler/execute-query";

            // SQL query
            string query = "SELECT * FROM YourTable";

            // JSON payload
            string jsonPayload = $"{{\"query\": \"{query}\"}}";

            // Create HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Set up the request content
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                try
                {
                    // Send POST request
                    HttpResponseMessage response = await client.PostAsync(url, content);

                    // Read response content
                    string responseContent = await response.Content.ReadAsStringAsync();

                    // Display the response
                    Console.WriteLine("Response Status Code: " + response.StatusCode);
                    Console.WriteLine("Response Content: " + responseContent);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error occurred during HTTP request: " + ex.Message);
                }
            }
        }
    }
}
