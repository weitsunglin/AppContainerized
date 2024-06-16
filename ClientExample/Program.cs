using System;
using System.Threading.Tasks;

namespace ClientExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // 进行HTTP请求
            await HttpClientExample.PerformHttpRequest();

            // 进行Socket通信
            await SocketClientExample.PerformSocketCommunication();
        }
    }
}
