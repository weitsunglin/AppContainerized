using System;
using System.Threading.Tasks;

namespace ClientExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter command (insert, select, socket, exit): ");
                string? command = Console.ReadLine();

                switch (command?.ToLower())
                {
                    case "insert":
                        Console.Write("Enter Name: ");
                        string? name = Console.ReadLine();
                        Console.Write("Enter Age: ");
                        string? ageInput = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(ageInput) || !int.TryParse(ageInput, out int age))
                        {
                            Console.WriteLine("Invalid input. Name and age are required.");
                        }
                        else
                        {
                            await HttpClientService.PerformInsertRequest(name, age);
                        }
                        break;

                    case "select":
                        await HttpClientService.PerformSelectRequest();
                        break;

                    case "socket":
                        Console.WriteLine("Enter socket command (1, 2, or 3): ");
                        string? socketCommand = Console.ReadLine();

                        if (socketCommand != "1" && socketCommand != "2" && socketCommand != "3")
                        {
                            Console.WriteLine("Invalid command. Please enter 1, 2, or 3.");
                        }
                        else
                        {
                            await TcpClientService.PerformSocketCommunication(socketCommand);
                        }
                        break;

                    case "exit":
                        return;

                    default:
                        Console.WriteLine("Invalid command. Please enter insert, select, socket, or exit.");
                        break;
                }
            }
        }
    }
}
