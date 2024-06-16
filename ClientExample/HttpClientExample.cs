using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClientExample
{
    public class HttpClientExample
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task PerformSelectRequest()
        {
            string url = "http://localhost:5000/api/HttpRequestHandler/execute-query";
            string query = "SELECT * FROM YourTable";
            string jsonPayload = $"{{\"query\": \"{query}\"}}";

            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PostAsync(url, content);
                string responseContent = await response.Content.ReadAsStringAsync();

                Console.WriteLine("SELECT Response Status Code: " + response.StatusCode);
                Console.WriteLine("SELECT Response Content: " + responseContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred during SELECT request: " + ex.Message);
            }
        }

        public static async Task PerformInsertRequest(string name, int age)
        {
            string url = "http://localhost:5000/api/HttpRequestHandler/execute-query";
            string query = $"INSERT INTO YourTable (Name, Age) VALUES ('{name}', {age})";
            string jsonPayload = $"{{\"query\": \"{query}\"}}";

            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PostAsync(url, content);
                string responseContent = await response.Content.ReadAsStringAsync();

                Console.WriteLine("INSERT Response Status Code: " + response.StatusCode);
                Console.WriteLine("INSERT Response Content: " + responseContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred during INSERT request: " + ex.Message);
            }
        }
    }
}
