using System;

namespace RestAirline.Domain.Shared
{
    public class Maybe<T> where T : class
    {
        private readonly T _value;

        public T Value
        {
            get
            {
                if (_value == null)
                {
                    throw new ArgumentNullException("value is null");
                }

                return _value;
            }
        }

        public Maybe(T value)
        {
            _value = value;
        }

    }

    public static class Maybe
    {
        public static Maybe<T> Some<T>(T value) where T : class
        {
            return new Maybe<T>(value);
        }

        public static Maybe<T> None<T>() where T : class
        {
            return new Maybe<T>(null);
        }

        public static bool HasValue<T>(this Maybe<T> @this) where T : class
        {
            try
            {
                if (@this.Value == null)
                {
                    return false;
                }
            }
            catch (ArgumentNullException e)
            {

            }
            return true;
        }
    }
}