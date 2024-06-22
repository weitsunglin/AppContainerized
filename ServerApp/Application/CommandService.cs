using System;
using System.IO;
using System.Reflection;

namespace MyApp.Services
{
    //處理client socket 業務
    public static class CommandService
    {
        public static string ExecuteCommand1()
        {
            return "Command 1 received: Hello from server";
        }

        public static string ExecuteCommand2()
        {
            return "Command 2 received: : Hello from server ";
        }

        public static string ExecuteCommand3()
        {
            return "Command 3 received: : Hello from server");
        }

        public static string ExecuteUnknownCommand()
        {
            return "Unknown command: from server";
        }
    }
}