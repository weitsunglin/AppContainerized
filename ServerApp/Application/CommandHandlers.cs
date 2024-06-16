using System;
using System.IO;
using System.Reflection;

namespace MyApp.Services
{
    public static class CommandHandlers
    {
        public static string HandleCommand1()
        {
            return "Command 1 received: Hello from server";
        }

        public static string HandleCommand2()
        {
            return "Command 2 received: Server time is " + DateTime.Now.ToString();
        }

        public static string HandleCommand3()
        {
            return "Command 3 received: Execution path is " + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        public static string HandleUnknownCommand()
        {
            return "Unknown command";
        }
    }
}
