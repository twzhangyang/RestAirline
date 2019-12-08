using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace RestAirline.Shared.Extensions
{
    public static class JsonExtensions
    {
        public static TResponse DeSerializeTo<TResponse>(this string value)
        {
            if (typeof(TResponse) == typeof(string))
            {
                return (TResponse)(object)value;
            }

            return JsonConvert.DeserializeObject<TResponse>(value);
        }

        public static string Serialize<TRequest>(this TRequest request)
        {
            return JsonConvert.SerializeObject(request);
        }

        public static string SerializeToCamelCase<TRequest>(this TRequest request)
        {
            return JsonConvert.SerializeObject(request, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }

        public static string SerializeToCamelCaseWithIndented<TRequest>(this TRequest request)
        {
            return JsonConvert.SerializeObject(request, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            });
        }
    }
}