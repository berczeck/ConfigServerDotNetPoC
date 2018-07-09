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

                var dataBaseSection = config.GetSection("application:dataBaseConnection");
                Console.WriteLine(dataBaseSection["pricing"]);
                Console.WriteLine(dataBaseSection["invoicing"]);

                var ftpSection = config.GetSection("application:ftp");
                Console.WriteLine(ftpSection["server"]);
                Console.WriteLine(ftpSection["path"]);


                Console.WriteLine("Desea continuar?");
                value = Console.ReadLine();
            } while (value != "EXIT");

            Console.ReadLine();
        }
    }
}
