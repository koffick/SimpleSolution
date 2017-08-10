using System;
using System.ComponentModel;

namespace SimpleSolutionKassa
{
    public static class Converter
    {
        public static Nullable<T> ToNullable<T>(string source) where T : struct
        {
            Nullable<T> result = new Nullable<T>();

            if (!string.IsNullOrEmpty(source) && !string.IsNullOrWhiteSpace(source))
                {
                    TypeConverter conv = TypeDescriptor.GetConverter(typeof(T));
                    result = (T)conv.ConvertFrom(source);
            
            }
            return result;
        }

        public static T To<T>(string source) where T : struct
        {
            T result = new T();

            if (!string.IsNullOrEmpty(source) && !string.IsNullOrWhiteSpace(source))
            {
                TypeConverter conv = TypeDescriptor.GetConverter(typeof(T));
                result = (T)conv.ConvertFrom(source);

            }
            return result;
        }

        public static object To(Type t, string source)
        {
            object result = Activator.CreateInstance(t);

            if (!string.IsNullOrEmpty(source) && !string.IsNullOrWhiteSpace(source))
            {
                TypeConverter conv = TypeDescriptor.GetConverter(t);
                result = conv.ConvertFrom(source);

            }
            return result;
        }
    }
}
