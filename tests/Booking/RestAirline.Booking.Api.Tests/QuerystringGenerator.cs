using System.Reflection;
using System.Text;
using RestAirline.Shared.Extensions;

namespace RestAirline.Booking.Api.Tests
{
    public static class QuerystringGenerator
    {
        public static string ToQueryString<TRequest>(TRequest request)
        {
            if (request.IsNull())
            {
                return string.Empty;
            }
            
            var type = request.GetType();

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var sb = new StringBuilder("?");
            foreach (var propertyInfo in properties)
            {
                var name = propertyInfo.Name;
                var value = propertyInfo.GetValue(request);
                if (value.IsNotNull() && value.ToString().IsNotNullOrEmpty())
                {
                    sb.Append($"{name}={value}&");
                }
            }

            return sb.ToString();
        }

        public static string ToQueryStringWithoutSymbols<TRequest>(TRequest request)
        {
            var query = ToQueryString(request);
            return query.Substring(1, query.Length - 2);
        }
    }
}