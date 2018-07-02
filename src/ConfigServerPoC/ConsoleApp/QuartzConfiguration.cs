using Newtonsoft.Json;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ConsoleApp
{
    static class QuartzConfiguration
    {
        private static IScheduler scheduler;


        public static void Init()
        {
            var props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
            var factory = new StdSchedulerFactory(props);
            scheduler = factory.GetScheduler().Result;

            scheduler.Start().Wait();

            IJobDetail job = JobBuilder.Create<ConfigJob>()
                 .WithIdentity($"job", "grupp")
                 .Build();

            ITrigger trigger = TriggerBuilder.Create()
                        .WithIdentity($"trigger", "grupp")
                        .StartNow()
                        .WithCronSchedule("0 0/1 0-23 * * ?", cron => { cron.InTimeZone(TimeZoneInfo.Utc); })
                        .Build();

            // Tell quartz to schedule the job using our trigger
            scheduler.ScheduleJob(job, trigger).Wait();
        }
    }

    public class ConfigJob : IJob
    {
        private static HttpClient client = new HttpClient();

        public Task Execute(IJobExecutionContext context)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.GetAsync($"http://localhost:63820/v1/demo/dev").Result;

            var jsonResponse = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<List<Response>>(jsonResponse);

            foreach (var item in result)
            {
                var path = Path.Combine(GetContentRoot(), item.FileName);
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(item.Content);
                }
            }

            return Task.FromResult(0);
        }

        public static string GetContentRoot()
        {
            var basePath = (string)AppDomain.CurrentDomain.GetData("APP_CONTEXT_BASE_DIRECTORY") ??
               AppDomain.CurrentDomain.BaseDirectory;
            return Path.GetFullPath(basePath);
        }
    }

    public class Response
    {
        public string FileName { get; set; }
        public string Content { get; set; }
    }
}
