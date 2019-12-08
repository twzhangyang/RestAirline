using System;

namespace RestAirline.Shared.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static T ResolveService<T>(this IServiceProvider provider)
            where T : class
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            return (T)provider.GetService(typeof(T));
        }
    }
}