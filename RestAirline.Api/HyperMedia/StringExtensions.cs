namespace RestAirline.Api.HyperMedia
{
    public static class StringExtensions
    {
        public static string FormatWith(this string @this, params object[] parameters)
        {
            return string.Format(@this, parameters);
        }

        public static bool IsNullOrEmpty(this string @this)
        {
            return string.IsNullOrEmpty(@this);
        }
    }
}