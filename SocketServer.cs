using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MySocketHttpApp
{
    public class SocketServer
    {
        private TcpListener _listener;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public async Task StartAsync()
        {
            _listener = new TcpListener(IPAddress.Any, 5000);
            _listener.Start();
            Console.WriteLine("Socket server started on port 5000");

            try
            {
                while (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    var client = await _listener.AcceptTcpClientAsync();
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
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            var buffer = new byte[1024];
            var stream = client.GetStream();

            int bytesRead;
            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
            {
                var message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received from socket: {message}");

                var response = Encoding.UTF8.GetBytes("Hello from socket server");
                await stream.WriteAsync(response, 0, response.Length);
            }

            client.Close();
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}
