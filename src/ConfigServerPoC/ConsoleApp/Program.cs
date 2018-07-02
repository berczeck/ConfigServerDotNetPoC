using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            QuartzConfiguration.Init();

            string value = "";
            do
            {
                ApplicationConfig.RegisterConfig("API_DEMO", "DEV");
                var config = ApplicationConfig.Configuration;
                var section = config.GetSection("application:dataBaseConnection");

                Console.WriteLine(section["pricing"]);
                Console.WriteLine(section["invoicing"]);
                Console.WriteLine(config.GetSection("title").Value);

                Console.WriteLine("Desea continuar?");
                value = Console.ReadLine();
            } while (value != "EXIT");

            Console.ReadLine();
        }
    }
}
