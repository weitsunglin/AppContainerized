using System;

namespace MyApp.Modules
{
    public class ModuleC : IModule
    {
        public void Initialize()
        {
            Console.WriteLine("ModuleC initialized.");
        }

        public void SomeMethod()
        {
            Console.WriteLine("ModuleC method called.");
        }
    }
}
