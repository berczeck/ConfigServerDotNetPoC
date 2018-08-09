using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConfigServerHost.Web.Controllers
{
    [Produces("application/json")]
    [Route("v1")]
    public class ConfigurationFileController : Controller
    {
        [HttpGet("{application}/{environment}/{version?}")]
        public List<Response> Get(string application, string environment, string version = "")
        {

            var fileList = new List<Response>();
            var path = Path.Combine(GetContentRoot(), "Repo") ;
            foreach (var item in Directory.EnumerateFiles(path))
            {
                fileList.Add(new Controllers.Response
                {
                    FileName = Path.GetFileName(item),
                    Content = System.IO.File.ReadAllText(item)
                });
            }

            return fileList;
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