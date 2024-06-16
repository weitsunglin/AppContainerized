using System;
using System.Threading.Tasks;

namespace ClientExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("Enter Name: ");
            string? name = Console.ReadLine();
            Console.Write("Enter Age: ");
            string? ageInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(ageInput) || !int.TryParse(ageInput, out int age))
            {
                Console.WriteLine("Invalid input. Name and age are required.");
                return;
            }

            await HttpClientExample.PerformInsertRequest(name, age);

            Console.WriteLine("Performing SELECT request...");
            await HttpClientExample.PerformSelectRequest();

            Console.WriteLine("Enter command (1, 2, or 3): ");
            string? commandInput = Console.ReadLine();

            if (commandInput != "1" && commandInput != "2" && commandInput != "3")
            {
                Console.WriteLine("Invalid command. Please enter 1, 2, or 3.");
                return;
            }

            Console.WriteLine("Performing socket communication...");
            await SocketClientExample.PerformSocketCommunication(commandInput);
        }
    }
}
