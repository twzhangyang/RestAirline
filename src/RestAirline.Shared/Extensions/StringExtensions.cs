using System;

namespace RestAirline.Shared.Extensions
{
    public static class StringExtensions
    {
        public static bool EqualCaseInsensitive(this string source, string dest)
        {
            if (source.IsNull())
            {
                return false;
            }
            
            return source.Equals(dest, StringComparison.InvariantCultureIgnoreCase);
        }
        
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static bool IsNotNullOrEmpty(this string s)
        {
            return !s.IsNullOrEmpty();
        }

        public static T ToEnum<T>(this string s) where T : struct
        {
            return Enum.Parse<T>(s, true);
        }

        public static T? TryToEnum<T>(this string s) where T : struct
        {
            if (s.IsNullOrEmpty())
            {
                return null;
            }

            return Enum.Parse<T>(s, true);
        }

        public static string FormatWith(this string s, params object[] args)
        {
            return string.Format(s, args);
        }
    }
}