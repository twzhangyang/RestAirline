using System;

namespace RestAirline.Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static T Mutate<T>(this T @this, Action<T> action)
        {
            action?.Invoke(@this);

            return @this;
        }
    }
}