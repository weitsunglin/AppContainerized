using System;
using System.Threading.Tasks;

namespace ClientExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Performing SELECT request...");
            await HttpClientExample.PerformSelectRequest();

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
        }
    }
}
