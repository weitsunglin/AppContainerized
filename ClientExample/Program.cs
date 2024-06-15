using System;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // 进行HTTP请求
            await PerformHttpRequest();

            // 进行Socket通信
            await PerformSocketCommunication();
        }

        static async Task PerformHttpRequest()
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

        static async Task PerformSocketCommunication()
        {
            string server = "localhost";
            int port = 5001;  // 连接到主机的端口 5001

            try
            {
                using (TcpClient client = new TcpClient(server, port))
                {
                    Console.WriteLine("Connected to socket server.");

                    using (NetworkStream stream = client.GetStream())
                    {
                        // 发送消息到服务器
                        string message = "Hello from socket client";
                        byte[] data = Encoding.UTF8.GetBytes(message);
                        await stream.WriteAsync(data, 0, data.Length);
                        Console.WriteLine($"Sent to server: {message}");

                        // 接收来自服务器的消息
                        byte[] buffer = new byte[1024];
                        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                        if (bytesRead > 0)
                        {
                            string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                            Console.WriteLine($"Received from server: {response}");
                        }
                        else
                        {
                            Console.WriteLine("No data received from server.");
                        }
                        client.Close();
                        Console.WriteLine("Disconnected from socket server.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during socket communication: {ex.Message}");
            }
        }
    }
}
