using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace MyApp
{
    public class SocketService : ISocketService
    {
        private TcpListener _listener;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public async Task StartAsync()
        {
            _listener = new TcpListener(IPAddress.Any, 5001);
            _listener.Start();
            Console.WriteLine("Socket server started on port 5001");

            string executionPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Console.WriteLine($"Execution Path: {executionPath}");

            try
            {
                while (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    var client = await _listener.AcceptTcpClientAsync();
                    Console.WriteLine("Client connected.");
                    _ = HandleClientAsync(client);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Socket server stopped.");
            }
            finally
            {
                _listener.Stop();
                _listener = null;
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            var buffer = new byte[1024];
            var stream = client.GetStream();

            try
            {
                int bytesRead;
                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    var message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"Received: {message}");

                    // Respond to client
                    var response = Encoding.UTF8.GetBytes("Hello from server");
                    await stream.WriteAsync(response, 0, response.Length);
                    Console.WriteLine("Response sent.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                client.Close();
                Console.WriteLine("Client disconnected.");
            }
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}
