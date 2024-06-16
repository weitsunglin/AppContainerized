using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientExample
{
    public class SocketClientExample
    {
        public static async Task PerformSocketCommunication(string command)
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
                        byte[] data = Encoding.UTF8.GetBytes(command);
                        await stream.WriteAsync(data, 0, data.Length);
                        Console.WriteLine($"Sent to server: {command}");

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