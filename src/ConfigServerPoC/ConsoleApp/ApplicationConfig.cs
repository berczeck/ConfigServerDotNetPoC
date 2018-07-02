using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ConsoleApp
{
    public static class ApplicationConfig
    {
        private static HttpClient client = new HttpClient();

        public static IConfiguration Configuration { get; set; }

        public static void RegisterConfig(string applicationName, string environment)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.GetAsync($"http://localhost:8627/v1/{applicationName}/{environment}").Result;

            var jsonResponse = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<ServiceOperationResult<List<ConfigurationFileResponse>>>(jsonResponse);

            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(GetContentRoot())
                //.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                //.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            foreach (var item in result.Value)
            {
                var path = Path.Combine(GetContentRoot(), item.Name);
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(item.Content);
                }
                builder = builder.AddJsonFile(item.Name, optional: false, reloadOnChange: true);
            }

            Configuration = builder.Build();

            //var root = builder.Build();
            //var reloadToken = root.GetReloadToken();
            //reloadToken.RegisterChangeCallback(callback =>
            //{
            //    Console.WriteLine("Config changed");
            //}, null);

            //ChangeToken.OnChange(() => root.GetReloadToken(), () => Console.WriteLine("Config changed token"));
        }
        public static string GetContentRoot()
        {
            var basePath = (string)AppDomain.CurrentDomain.GetData("APP_CONTEXT_BASE_DIRECTORY") ??
               AppDomain.CurrentDomain.BaseDirectory;
            return Path.GetFullPath(basePath);
        }
    }
}
