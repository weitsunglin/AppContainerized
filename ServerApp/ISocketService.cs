using System.Threading.Tasks;

namespace MyApp
{
    public interface ISocketService
    {
        Task StartAsync();
        void Stop();
    }
}
