using Newtonsoft.Json;

namespace ConfigServerHost.Core.Repository
{
    public static class JsonExtension
    {
        public static string ConvertToJson(this object parameter)
        {
            var result = JsonConvert.SerializeObject(parameter, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
            });

            return result;
        }
    }
}
