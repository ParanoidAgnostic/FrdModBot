using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RedditWrapper.Helpers
{
    public static class StringValueAttributeHelpers
    {
        public static Dictionary<string, T> StringValues<T>() where T : struct
        {
            var type = typeof(T);
            if (!type.IsEnum)
                throw new ArgumentException(String.Format("Type is not an enum", type.Name));

            return (from field in type.GetFields(BindingFlags.Public | BindingFlags.Static)
                    where field.IsLiteral
                    select (T)field.GetValue(null)).ToDictionary(v => (v as Enum).GetStringValue(), v => v);
        }
    }
}
