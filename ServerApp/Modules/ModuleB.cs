using System;

namespace MyApp.Modules
{
    public class ModuleB : IModule
    {
        public void Initialize()
        {
            Console.WriteLine("ModuleB initialized.");
        }

        public void SomeMethod()
        {
            Console.WriteLine("ModuleB method called.");
        }
    }
}
