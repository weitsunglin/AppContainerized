using System;

namespace MyApp.Modules
{
    public class ModuleA : IModule
    {
        public void Initialize()
        {
            Console.WriteLine("ModuleA initialized.");
        }

        public void SomeMethod()
        {
            Console.WriteLine("ModuleA method called.");
        }
    }
}
